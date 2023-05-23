using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace HydrogenOMB {
    public class FileManager {
        private List<string> _fields;
        private string _fileName, _path, _templateFile;
        private const string Estensione = "xslx";
        private char _separator;
        private int Contatore { get; set; }
        private byte Times { get; set; }

        private Excel.Application _app;
        private Excel.Workbook _wb;
        private Excel.Worksheet _ws;
        private Excel.Range _range;
        //object misValue = System.Reflection.Missing.Value;

        public FileManager(string path, string templFile, char separator, string[] campi) {
            _fields = new List<string>(campi);

            Separator = separator;
            Path = path;
            TemplateFile = templFile;

            Contatore = 2;//2 perchè parte da 1 ma a 1 ci stanno le intestazione (delta tempo, ora, angolo..)
        }

        /*properties*/
        public string FileName {
            get {
                return _fileName;
            }
            private set {
                InserisciSeStringaValida(ref _fileName, value, "FileName");
            }
        }
        public string Path {
            get {
                return _path;
            }
            private set {
                InserisciSeStringaValida(ref _path, value, "Path");
            }
        }
        public string TemplateFile {
            get {
                return _templateFile;
            }
            private set {
                InserisciSeStringaValida(ref _templateFile, value, "TemplateFile");
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
        public List<string> Fields {
            get {
                return _fields;
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
                    throw new Exception("WorkSheet not valid");
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
            DateTime tmp = DateTime.Now;
            FileName = $"{tmp.Day}-{tmp.Month}-{tmp.Year}_{tmp.Hour}-{tmp.Minute}-{tmp.Second}";

            File.Copy($@"{Path}\{TemplateFile}.{Estensione}", $@"{Path}\{FileName}.{Estensione}");//il 'vecchio' è il template di base quindi lo si sovrascrive

            App = new Excel.Application(); //starts excel app
            App.DisplayAlerts = false; //it allows to not require everytime confirm to rewrite file
            Wb = (Excel.Workbook)(App.Workbooks.Add($@"{Path}\{TemplateFile}.{Estensione}"));

            for (byte i = 0; i < 4; i++) {
                ChangeWorkSheet((uint)i+1);//per far si che il grafico non si aggiorni ogni volta (lo riattiviamo a fine misurazione)
                Ws.EnableCalculation = false;
            }

            ChangeWorkSheet(1);//dati valvola
            Ws.Cells[1, 2] = DatiValvola.NomeValvola;
            Ws.Cells[2, 2] = DatiValvola.ModelloValvola;

            for (byte j = 0; j < 2; j++) {
                ChangeWorkSheet((uint)j+2);//perchè i fogli partono da 2
                for (int i = 0; i < Fields.Count; i++) { // aggiunta intestazione: trimmer, angolo, ...
                    Ws.Cells[1, i + 1] = Fields[i].ToUpper();
                }
            }
            ChangeWorkSheet(2); //seleziono la scheda delle misurazioni in apertura
        }
        public void Write(List<string> newLine) {
            byte cnt = (byte)newLine.Count;
            for (int i = 0; i < cnt; i++) {
                if (i < 2) { //only first 2 columns are string
                    Ws.Cells[Contatore, i + 1].NumberFormat = "@";//string format only with time
                    Ws.Cells[Contatore, i + 1] = newLine[i];
                } else {
                    Ws.Cells[Contatore, i + 1] = int.Parse(newLine[i]);//sennò non se li salva come intero e non li legge nel grafico
                }
            }
            Contatore++;

            if (Times == 10) { /*every 10 times I auto-salve file*/
                Wb.SaveAs($@"{Path}\{FileName}.{Estensione}");
                Times = 0;
            }
            Times++;
        }
        public void Close() {
            for (byte i = 0; i < 4; i++) {//riattiviamo il ricalcolo automatico solo alla fine della misurazione, così da non rallentare in fase di misurazione
                ChangeWorkSheet((uint)i + 1);
                Ws.EnableCalculation = true;
            }
            Wb.SaveAs($@"{Path}\{FileName}.{Estensione}");

            Wb.Close();
            App.Quit();
        }

        private void InserisciSeStringaValida(ref string campo, string val, string perErrore) {
            if (!String.IsNullOrWhiteSpace(val)) {
                campo = val;
            } else {
                throw new Exception($"Invalid \"{perErrore}\"");
            }
        }

        public void ChangeWorkSheet(uint index) {
            Ws = Wb.Worksheets[index];
            Contatore = 2;
        }
    }
}