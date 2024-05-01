using System;
using System.IO;
using OfficeOpenXml;

namespace HydrogenOMB {
    public enum eWorksheet {
        ValveData,
        OpenValveData,
        CloseValveData,
        Graph,
        Count
    }

    public class ExcelManager {
        private string [] fields;
        private string _fileName, _path, _templateFile;
        private const string Estensione = "xlsx";
        private int IndexRiga { get; set; }

        private ExcelPackage _app;
        private ExcelWorksheet _ws;


        public ExcelManager(string path, string templFile, string[] campi) {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            fields = campi;

            Path = path;
            TemplateFile = templFile;
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
            IndexRiga = 2; //2 perche' parte da 1; ma a 1 ci stanno le intestazione (delta tempo, ora, angolo..)
            DateTime tmp = DateTime.Now;
            FileName = $"{tmp.Day}-{tmp.Month}-{tmp.Year}_{tmp.Hour}-{tmp.Minute}-{tmp.Second}";


            File.Copy($@"{Path}/{TemplateFile}.{Estensione}", $@"{Path}/{FileName}.{Estensione}"); //il "vecchio" e' il template di base quindi lo si sovrascrive


            ExcelFile = new ExcelPackage($@"{Path}/{FileName}.{Estensione}");

            //**RIEMPIMENTO DATI INFORMAZIONI VALVOLA**//
            ChangeWorkSheet((uint)eWorksheet.ValveData);
            Ws.Cells[1, 2].Value = PublicData.Instance.InfoValve.NomeValvola;
            Ws.Cells[2, 2].Value = PublicData.Instance.InfoValve.ModelloValvola;


            for (uint j = (uint)(eWorksheet.OpenValveData); j <= (uint)(eWorksheet.CloseValveData); j++) {
                ChangeWorkSheet(j);
                for (int i = 0; i < fields.Length; i++) { //aggiunta intestazione: trimmer, angolo, ...
                    Ws.Cells[1, i + 1].Value = fields[i].ToUpper();
                }
            }
        }

        public void Write(OMBRecord record) {
            Ws.Cells[IndexRiga, 1].Value = record.Angle;
            Ws.Cells[IndexRiga, 2].Value = record.Pair;

            IndexRiga++;
        }

        public void Close() {
            SaveFile();
            ExcelFile.Dispose();
        }

        public void ChangeWorkSheet(uint index) {
            Ws = ExcelFile.Workbook.Worksheets[(int)index];
            IndexRiga = 2;
        }

        public void SaveFile() {
            ExcelFile.SaveAs($@"{Path}/{FileName}.{Estensione}");
        }
    }
}