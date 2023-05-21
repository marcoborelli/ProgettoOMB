﻿using System;
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
        private List<string> _datiValvola;
        private string _fileName, _path, _templateFile;
        private char _separator;
        private int Contatore { get; set; }
        private byte Times { get; set; }

        private Excel.Application _app;
        private Excel.Workbook _wb;
        private Excel.Worksheet _ws;
        private Excel.Range _range;
        //object misValue = System.Reflection.Missing.Value;

        public FileManager( string path,  string templFile, char separator, string[] campi, string[] datiValvola) {
            _fields = new List<string>(campi);
            _datiValvola = new List<string>(datiValvola);

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
        public List<string> DatiValvola {
            get {
                return _datiValvola;
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

            File.Copy($@"{Path}\{TemplateFile}.xlsx", $@"{Path}\{FileName}.xlsx");//il 'vecchio' è il template di base quindi lo si sovrascrive

            App = new Excel.Application(); //starts excel app
            App.DisplayAlerts = false; //it allows to not require everytime confirm to rewrite file
            Wb = (Excel.Workbook)(App.Workbooks.Add($@"{Path}\{TemplateFile}.xlsx"));
            
            Ws = Wb.Worksheets[1];
            for (int i = 0; i < DatiValvola.Count; i++) { //aggiunta nome e modello della valvola
                Ws.Cells[i + 1, 2] = DatiValvola[i];
            }


            Ws = Wb.Worksheets[2];
            for (int i = 0; i < Fields.Count; i++) { // aggiunta intestazione: trimmer, angolo, ...
                Ws.Cells[1, i + 1] = Fields[i].ToUpper();
            }
        }
        public void Write(bool first, string newLine) {
            if (String.IsNullOrWhiteSpace(newLine)) {
                return;
            }

            try {
                string[] val = newLine.Split(Separator);

                for (int i = 0; i < val.Length; i++) {
                    if (i < 2) { //only first 2 columns are string
                        Ws.Cells[Contatore, i + 1].NumberFormat = "@";//string format only with time
                        Ws.Cells[Contatore, i + 1] = val[i];
                    } else {
                        Ws.Cells[Contatore, i + 1] = int.Parse(val[i]);
                    }
                }
                Contatore++;
            } catch {
                throw new Exception("Not valid string");
            }

            if (Times == 5) { /*every 5 times I auto-salve file*/
                Wb.SaveAs($@"{Path}\{FileName}.xlsx");
                Times = 0;
            }
            Times++;
        }
        public void Close() {
            Wb.SaveAs($@"{Path}\{FileName}.xlsx");

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
    }
}
