using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public void StartMeasurement() {
            AssociatedForm.StartMeasure();
        }
        public void StopMeasurement(string mess) {
            AssociatedForm.StopMeasure(mess);
        }
        public void EndOpening() {
            AssociatedForm.EndOpen();
        }
    }
}
