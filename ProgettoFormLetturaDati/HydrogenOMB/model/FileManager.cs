using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;

namespace HydrogenOMB {
    public class FileManager {
        private List<string> _fields;
        private string _fileName, _path, _templateFile;
        private const string Estensione = "xlsx";
        private char _separator;
        private int Contatore { get; set; }

        private ExcelPackage _app;
        private ExcelWorksheet _ws;

        public FileManager(string path, string templFile, char separator, string[] campi) {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            _fields = new List<string>(campi);

            Separator = separator;
            Path = path;
            TemplateFile = templFile;

            Contatore = 2;//2 perchè parte da 1 ma a 1 ci stanno le intestazione (delta tempo, ora, angolo..)
        }

        /*properties*/
        public string FileName {
            get => _fileName;
            private set => PublicData.InsertIfObjValid(ref _fileName, value, "FileName");
        }
        public string Path {
            get => _path;
            private set => PublicData.InsertIfObjValid(ref _path, value, "Path");
        }
        public string TemplateFile {
            get => _templateFile;
            private set => PublicData.InsertIfObjValid(ref _templateFile, value, "TemplateFile");
        }
        public char Separator {
            get => _separator;
            private set => PublicData.InsertIfObjValid(ref _separator, value, "Char Separator");
        }
        public List<string> Fields {
            get => _fields;
        }
        private ExcelPackage ExcelFile {
            get => _app;
            set => PublicData.InsertIfObjValid(ref _app, value, "Excel Application");
        }
        private ExcelWorksheet Ws {
            get => _ws;
            set => PublicData.InsertIfObjValid(ref _ws, value, "Excel Worksheet");
        }
        /*end properties*/

        public void StartNewFile() {
            DateTime tmp = DateTime.Now;
            FileName = $"{tmp.Day}-{tmp.Month}-{tmp.Year}_{tmp.Hour}-{tmp.Minute}-{tmp.Second}";


            File.Copy($@"{Path}/{TemplateFile}.{Estensione}", $@"{Path}/{FileName}.{Estensione}"); //il 'vecchio' è il template di base quindi lo si sovrascrive


            ExcelFile = new ExcelPackage($@"{Path}/{FileName}.{Estensione}");

            //**RIEMPIMENTO DATI INFORMAZIONI VALVOLA**//
            ChangeWorkSheet(0);//dati valvola
            Ws.Cells[1, 2].Value = PublicData.InfoValve.NomeValvola;
            Ws.Cells[2, 2].Value = PublicData.InfoValve.ModelloValvola;


            for (byte j = 0; j < 2; j++) {
                ChangeWorkSheet((uint)j+1);//perchè i fogli partono da 1
                for (int i = 0; i < Fields.Count; i++) { // aggiunta intestazione: trimmer, angolo, ...
                    Ws.Cells[1, i + 1].Value = Fields[i].ToUpper();
                }
            }
        }

        public void Write(List<string> newLine) {
            byte cnt = (byte)newLine.Count;

            for (int i = 0; i < cnt; i++) {
                if (i < 2) { //only first 2 columns are string
                    Ws.Cells[Contatore, i + 1].Style.Numberformat.Format = "@"; ;//string format only with time
                    Ws.Cells[Contatore, i + 1].Value = newLine[i];
                } else {
                    Ws.Cells[Contatore, i + 1].Value = int.Parse(newLine[i]);//sennò non se li salva come intero e non li legge nel grafico
                }
            }

            Contatore++;
        }
        public void Close() {
            SaveFile();
            ExcelFile.Dispose();
        }

        public void ChangeWorkSheet(uint index) {
            Ws = ExcelFile.Workbook.Worksheets[(int)index];
            Contatore = 2;
        }
        public void SaveFile() {
            ExcelFile.SaveAs($@"{Path}/{FileName}.{Estensione}");
        }
    }
}