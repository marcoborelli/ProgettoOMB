using System;
using System.Diagnostics;
using System.Drawing;

namespace HydrogenOMB {
    public class DataManager : IDataManager {
        private Form1 _associatedForm;
        private ExcelManager _excManager;
        private readonly char Separator; //separatore del record ricevuto sulla porta seriale
        string[] campi = new string[] { "delta", "time", "angle", "pair" };


        public DataManager(Form1 form) {
            AssociatedForm = form;

            Separator = ';';
            ExcManager = new ExcelManager($"{AppDomain.CurrentDomain.BaseDirectory}{PublicData.Instance.OutputDirectory}", PublicData.Instance.TemplateFileName, campi);
        }


        /*properties*/
        public Form1 AssociatedForm {
            get => _associatedForm;
            set => PublicData.InsertIfObjValid(ref _associatedForm, value, "Form");
        }

        public ExcelManager ExcManager {
            get => _excManager;
            private set => PublicData.InsertIfObjValid(ref _excManager, value, "FileManager");
        }
        /*fine properties*/


        public void OnStart() {
            AssociatedForm.SetStateOfValveDataInput(false);
            string[] valveFields = AssociatedForm.GetValveFields();
            PublicData.Instance.InfoValve.NomeValvola = valveFields[0];
            PublicData.Instance.InfoValve.ModelloValvola = valveFields[1];

            AssociatedForm.PrintOn(Color.Black, DateTime.Now, "Inizio misurazione");
        }

        public void OnEndOpen() {
            AssociatedForm.PrintOn(Color.Black, DateTime.Now, "Apertura valvola terminata, inzio chiusura...");
        }

        public void OnStop() {
            AssociatedForm.PrintOn(Color.Black, DateTime.Now, "Misurazione terminata con successo");
            StartNewExcelFile();
        }

        public void OnForcedStop() {
            AssociatedForm.PrintOn(Color.Black, DateTime.Now, "Misurazione fermata");
            StartNewExcelFile();
        }

        public void OnEndArrayOpen() {
            ExcManager.ChangeWorkSheet((uint)eWorksheet.CloseValveData); //metto sul foglio di chiusura
            ExcManager.SaveFile(); //salvataggio backup(?)
        }

        public void OnEndArrayClose() {
            ExcManager.Close(); //chiudo e salvo il file di excel

            AssociatedForm.PrintOn(Color.Green, DateTime.Now, "File excel creato correttamente!\n");
            AssociatedForm.SetStateOfValveDataInput(true);
            AssociatedForm.ResetValveFields();
            AssociatedForm.StartSerialPort();

            if (Settings.Instance.OpenInExplorer) {
                string fileMan = PublicData.IsWindows() ? "explorer.exe" : "xdg-open";
                Process.Start(fileMan, $"{AppDomain.CurrentDomain.BaseDirectory}{PublicData.Instance.OutputDirectory}");
            }
        }

        public void OnData(string row, DateTime oldTime) {
            OMBRecord rec = new OMBRecord(row, Separator, oldTime);

            if (rec != null) { //se e' null e' perche' i gradi hanno superato il max
                ExcManager.Write(rec); //per stampare su file excel
            }
        }


        private void StartNewExcelFile() {
            ExcManager.StartNewFile();
            AssociatedForm.PrintOn(Color.Black, DateTime.Now, "Creazione file excel...");
            ExcManager.ChangeWorkSheet((uint)eWorksheet.OpenValveData);
        }
    }
}
