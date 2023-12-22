using System;
using System.Runtime.InteropServices;

namespace HydrogenOMB {
    public class PublicData {
        public struct StructInfoValve {
            public string NomeValvola;
            public string ModelloValvola;
        }


        private static PublicData _instance;

        public string _configFileName, _outpDirectory, _templateFileName;
        public StructInfoValve InfoValve;


        public static PublicData Instance {
            get => _instance;
            set {
                if (value != null && Instance == null) {
                    _instance = value;
                }
            }
        }

        public string ConfigFileName { //"read only"
            get => _configFileName;
            private set => InsertIfObjValid(ref _configFileName, value, "Configuration filename");
        }

        public string OutputDirectory {
            get => _outpDirectory;
            private set => InsertIfObjValid(ref _outpDirectory, value, "Output directory");
        }

        public string TemplateFileName {
            get => _templateFileName;
            private set => InsertIfObjValid(ref _templateFileName, value, "Template filename");
        }


        private PublicData() { //Singleton Pattern
        }

        public static void Init() {
            Instance = new PublicData();

            Instance.ConfigFileName = "settings.conf";
            Instance.OutputDirectory = "File";
            Instance.TemplateFileName = "base";
            Instance.InfoValve = new StructInfoValve();
        }


        public static void InsertIfObjValid<T>(ref T campo, T val, string perErrore) {
            if (typeof(T) == typeof(String) && String.IsNullOrWhiteSpace(val.ToString()) || typeof(T) == typeof(Char) && val.ToString().Trim() == "") {
                val = default(T); //se e' stringa ed e' fatta da spazi vuoti la metto a null (così poi c'e' un solo controllo uguale per tutti i tipi), se e' char e non e' un separatore valido idem
            }

            if (val != null) {
                campo = val;
            } else {
                throw new Exception($"Invalid \"{perErrore}\"");
            }
        }

        public static bool IsWindows() {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }
    }
}