using System;
using System.Runtime.InteropServices;

namespace HydrogenOMB {
    public static class PublicData {
        public static class InfoValve {
            public static string NomeValvola;
            public static string ModelloValvola;
        }

        public static string ConfigFileName = "settings.conf";
        public static string OutpDirectory = "File";
        public static string TemplateFileName = "base";



        public static void InsertIfObjValid<T>(ref T campo, T val, string perErrore) {
            if (typeof(T) == typeof(String) && String.IsNullOrWhiteSpace(val.ToString()) ||
                typeof(T) == typeof(Char) && val.ToString().Trim() == "") {
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
