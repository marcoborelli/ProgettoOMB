using System;
using System.Collections.Generic;

namespace HydrogenOMB {
    public class DataManager {
        private Form1 _associatedForm;
        private char _separator;

        public DataManager(Form1 form, char separator) {
            AssociatedForm = form;
            Separator = separator;
        }

        /*properties*/
        public Form1 AssociatedForm {
            get => _associatedForm;
            set => PublicData.InsertIfObjValid(ref _associatedForm, value, "Form");
        }
        public char Separator {
            get => _separator;
            private set => PublicData.InsertIfObjValid(ref _separator, value, "Char Separator");
        }
        /*fine properties*/

        public void StartMeasurement(string m) {
            AssociatedForm.StartMeasure(m);
        }
        public void StopMeasurement(string mess) {
            AssociatedForm.StopMeasure(mess);
        }
        public void EndOpening(string messaggio) {
            AssociatedForm.EndOpen(messaggio);
        }
        public void StartExcelWriting(string messaggino) {
            AssociatedForm.StartWritingExcel(messaggino);
        }
        public void StopExcelWriting(string messaggetto) {
            AssociatedForm.StoptWritingExcel(messaggetto);
        }
    }
}
