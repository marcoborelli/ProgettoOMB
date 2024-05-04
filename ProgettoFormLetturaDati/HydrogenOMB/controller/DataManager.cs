using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HydrogenOMB {
    public class DataManager : IDataManager {
        private Form1 _associatedForm;
        private ExcelManager _excManager;
        private SerialPortReader _sPortReader;

        private readonly char Separator; //separatore del record ricevuto sulla porta seriale
        string[] campi = new string[] { "angle", "pair" };
        private List <OMBRecord> TestData { get; set; }


        public DataManager(Form1 form) {
            AssociatedForm = form;
            AssociatedForm.FormClosing += new FormClosingEventHandler(Form_FormClosing);

            Separator = ';';
            ExcManager = new ExcelManager($"{AppDomain.CurrentDomain.BaseDirectory}{PublicData.Instance.OutputDirectory}", PublicData.Instance.TemplateFileName, campi);

            string portName = PublicData.IsWindows() ? Settings.Instance.PortNameOnWin : Settings.Instance.PortNameOnLinux;
            SPortReader = new SerialPortReader(portName, Settings.Instance.PortBaud, this);
            SPortReader.StartPort();

            TestData = new List<OMBRecord>();

            LoadComboBoxItems();
        }

        public async Task LoadComboBoxItems() {
            AssociatedForm.PrintOn(Color.Black, "È in corso la connessione al servizio web...");
            string[] res = await ApiRequester.Instance.GetAllInstances();

            if (res == null) {
                //TODO: aggiungere file di testo a parte in cui vengono salvati gli ultimi id e caricare quello
                AssociatedForm.PrintOn(Color.Red, "Non è stato possibile connetrsi al db. Inserire l'id manualmente");
                AssociatedForm.SetComboWriteableState(true);
            } else if (res.Length == 0) {
                AssociatedForm.PrintOn(Color.Red, "Non sono presenti seriali. Inserire prima modelli di valvola");
            } else {
                AssociatedForm.PrintOn(Color.Black, "Id di tutte le istanze caricati correttamente");
                AssociatedForm.SetComboWriteableState(false); //nel caso in cui prima fosse stato cambiato ora viene ripristinato
                AssociatedForm.SetItemsCombo(res);
            }
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

        public SerialPortReader SPortReader {
            get => _sPortReader;
            set => PublicData.InsertIfObjValid(ref _sPortReader, value, "SerialPort Reader");
        }
        /*fine properties*/


        public void OnStart() {
            AssociatedForm.SetStateOfValveDataInput(false);
            PublicData.Instance.ValveSerialNumber = AssociatedForm.GetValveId();

            AssociatedForm.PrintOn(Color.Black, "Inizio misurazione");
        }

        public void OnEndOpen() {
            AssociatedForm.PrintOn(Color.Black, "Apertura valvola terminata, inzio chiusura...");
        }

        public void OnStop() {
            AssociatedForm.PrintOn(Color.Black, "Misurazione terminata con successo");
            StartNewExcelFile();
        }

        public void OnForcedStop() {
            AssociatedForm.PrintOn(Color.Black, "Misurazione fermata");
            StartNewExcelFile();
        }

        public void OnEndArrayOpen() {
            ExcManager.ChangeWorkSheet((uint)eWorksheet.CloseValveData); //metto sul foglio di chiusura
            ExcManager.SaveFile(); //salvataggio backup(?)
        }

        public void OnEndArrayClose() {
            ExcManager.Close(); //chiudo e salvo il file di excel

            AssociatedForm.PrintOn(Color.Green, "File excel creato correttamente!\n");
            AssociatedForm.SetStateOfValveDataInput(true);
            AssociatedForm.ResetValveIdField();

            SPortReader.ResetTestField();

            if (Settings.Instance.OpenInExplorer) {
                string fileMan = PublicData.IsWindows() ? "explorer.exe" : "xdg-open";
                Process.Start(fileMan, $"{AppDomain.CurrentDomain.BaseDirectory}{PublicData.Instance.OutputDirectory}");
            }

            Test t = new Test(PublicData.Instance.ValveSerialNumber, TestData);
            ApiRequester.Instance.AddNewTest(t);
            TestData.Clear();
        }

        public void OnData(string row, bool isOpening) {
            OMBRecord rec = new OMBRecord(row, isOpening, Separator);

            if (rec != null) { //se e' null e' perche' i gradi hanno superato il max
                ExcManager.Write(rec); //per stampare su file excel
                TestData.Add(rec);
            }
        }


        private void StartNewExcelFile() {
            ExcManager.StartNewFile();
            AssociatedForm.PrintOn(Color.Black, "Creazione file excel...");
            ExcManager.ChangeWorkSheet((uint)eWorksheet.OpenValveData);
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e) {
            SPortReader.StopPort(); //in questo modo, se si e' su linux (ma anche Windows) si killa il thread in ascolto sulla seriale
        }
    }
}
