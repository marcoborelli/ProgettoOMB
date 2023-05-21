﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HydrogenOMB {
    public class DataManager {
        private Form1 _associatedForm;
        private char _separator;
        public byte NumeroParametri { get; private set; }

        public DataManager(Form1 form, char separator, byte numParametri) {
            AssociatedForm = form;
            Separator = separator;
            NumeroParametri = numParametri;
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

        public void PrintOnForm(int index, string row) {
            string[] campi = row.Split(Separator); //3 perchè: 1=ora 2=delta 3=angolo
            if (campi.Length != 3 + NumeroParametri) {
                campi = new string[3 + NumeroParametri];
                for (byte i = 0; i < campi.Length; i++) {
                    campi[i] = "-";
                }
            }
            AssociatedForm.PrintRow(index, campi);
        }

        public void StartMeasurement() {
            AssociatedForm.StartMeasure();
        }
        public void StopMeasurement(string mess) {
            AssociatedForm.StopMeasure(mess);
        }
    }
}
