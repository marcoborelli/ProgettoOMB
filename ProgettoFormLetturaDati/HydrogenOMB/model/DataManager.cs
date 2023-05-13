using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HydrogenOMB {
    public class DataManager {
        private Form1 _associatedForm;
        public DataManager(Form1 form) {
            AssociatedForm = form;
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
                    throw new Exception("Inserire una form valida da associare");
                }
            }
        }
        /*fine properties*/

        public void PrintOnForm(int index, string row) {
            string[] campi = row.Split(';');
            if (campi.Length != 4) {
                campi = new string[] { "-", "-", "-", "-" };
            }
            AssociatedForm.PrintRow(index, campi);
        }

        public void StartMeasurement() {

        }
    }
}
