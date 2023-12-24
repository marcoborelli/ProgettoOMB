using System;
using System.Collections.Generic;

namespace HydrogenOMB {
    public class DataManager : IDataManager {
        private Form1 _associatedForm;
        private ExcelManager _excManager;
        private char _separator;


        public DataManager(Form1 form, ExcelManager excManager, char separator) {
            AssociatedForm = form;
            ExcManager = excManager;
            Separator = separator;
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

        public char Separator {
            get => _separator;
            private set => PublicData.InsertIfObjValid(ref _separator, value, "Char Separator");
        }
        /*fine properties*/


        public void OnStart() {
            AssociatedForm.StartMeasure("Inizio misurazione");
        }

        public void OnEndOpen() {
            AssociatedForm.EndOpen("Apertura valvola terminata, inzio chiusura...");
        }

        public void OnStop() {
            AssociatedForm.StopMeasure("Misurazione terminata con successo");
            StartNewExcelFile();
        }

        public void OnForcedStop() {
            AssociatedForm.StopMeasure("Misurazione fermata");
            StartNewExcelFile();
        }

        public void OnEndArrayOpen() {
            ExcManager.ChangeWorkSheet((uint)eWorksheet.CloseValveData); //metto sul foglio di chiusura
            ExcManager.SaveFile(); //salvataggio backup(?)
        }

        public void OnEndArrayClose() {
            ExcManager.Close(); //chiudo e salvo il file di excel
            AssociatedForm.StoptWritingExcel("File excel creato correttamente!\n");
        }

        public void OnData(string row, DateTime oldTime) { //va implementato oggetto ad hoc
            OMBRecord rec = new OMBRecord(row, Separator, oldTime);

            if (rec != null) { //se e' null e' perche' i gradi hanno superato il max
                ExcManager.Write(rec); //per stampare su file excel
            }
        }


        private void StartNewExcelFile() {
            ExcManager.StartNewFile();
            AssociatedForm.StartWritingExcel("Creazione file excel...");
            ExcManager.ChangeWorkSheet((uint)eWorksheet.OpenValveData);
        }
    }
}
