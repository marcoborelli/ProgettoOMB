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
            get {
                return _associatedForm;
            }
            private set {
                if (value != null) {
                    _associatedForm = value;
                } else {
                    throw new Exception("You must insert a valid Form");
                }
            }
        }
        public char Separator {
            get {
                return _separator;
            }
            private set {
                if ($"{value}" != "" && value != ' ') {
                    _separator = value;
                } else {
                    throw new Exception("Invalid Char Separator");
                }
            }
        }
        /*fine properties*/

        public void PrintOnForm(int index, List<string> row) {
            AssociatedForm.PrintRow(index, row);
        }

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
