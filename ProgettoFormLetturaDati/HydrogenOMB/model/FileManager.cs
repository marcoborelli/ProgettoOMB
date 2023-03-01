using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace HydrogenOMB {
    public class FileManager {
        private string _fileName, _path;
        private byte _times;
        private char _separator;

        private Excel.Application _app;
        private Excel.Workbook _wb;
        private Excel.Worksheet _ws;
        private Excel.Range _range;
        object misValue = System.Reflection.Missing.Value;

        public FileManager(char separator, string path) {
            Separator = separator;
            Path = path;
        }

        /*properties*/
        public string FileName {
            get {
                return _fileName;
            }
            set {
                if (!string.IsNullOrEmpty(value)) {
                    _fileName = value;
                } else {
                    throw new Exception("Invalid FileName");
                }
            }
        }
        public string Path {
            get {
                return _path;
            }
            set {
                if (!string.IsNullOrEmpty(value)) {
                    _path = value;
                } else {
                    throw new Exception("Invalid Path");
                }
            }
        }
        public char Separator {
            get {
                return _separator;
            }
            set {
                if ($"{value}" != "" && value != ' ') {
                    _separator = value;
                } else {
                    throw new Exception("Invalid Char Separer");
                }
            }
        }
        private byte Times {
            get {
                return _times;
            }
            set {
                _times = value;
            }
        }
        private Excel.Application App {
            get {
                return _app;
            }
            set {
                if (value != null) {
                    _app = value;
                } else {
                    throw new Exception("Application not valid");
                }
            }
        }
        private Excel.Workbook Wb {
            get {
                return _wb;
            }
            set {
                if (value != null) {
                    _wb = value;
                } else {
                    throw new Exception("WorkBook not valid");
                }
            }
        }
        private Excel.Worksheet Ws {
            get {
                return _ws;
            }
            set {
                if (value != null) {
                    _ws = value;
                } else {
                    throw new Exception("WorkBook not valid");
                }
            }
        }
        private Excel.Range Rng {
            get {
                return _range;
            }
            set {
                if (value != null) {
                    _range = value;
                } else {
                    throw new Exception("WorkBook not valid");
                }
            }
        }
        /*end properties*/

        public void StartNewFile() {
            FileName = $"{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}_{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}";

            App = new Excel.Application();/*starts excel app*/
            App.DisplayAlerts = false;/*it allows to not require everytime confirm to rewrite file*/
            App.SheetsInNewWorkbook = 1; /*there is only 1 sheet*/

            Wb = App.Workbooks.Add(misValue);

            Ws = Wb.Worksheets[1]; /*firts (and unique) sheet*/
            Ws.Name = "Size";
        }
        public void Write(bool first, string newLine) {
            if (!String.IsNullOrWhiteSpace(newLine)) {
                try {
                    string[] val = newLine.Split(';');
                    if (!first) {
                        AddLine();
                    }

                    for (int i = 0; i < val.Length; i++) {
                        Ws.Cells[1, i + 1] = val[i];
                    }
                } catch {
                    throw new Exception("Not valid string");
                }

                if (Times == 5) { /*every 5 times I auto-salve file*/
                    Wb.SaveAs($@"{Path}\{FileName}.xlsx");
                    Times = 0;
                }
                Times++;
            }
        }
        public void Close() {
            Wb.SaveAs($@"{AppDomain.CurrentDomain.BaseDirectory}File\{FileName}.xlsx");

            Wb.Close();
            App.Quit();
        }

        private void AddLine() {
            Rng = Ws.Rows[1];
            Rng.Insert();
        }
    }
}
