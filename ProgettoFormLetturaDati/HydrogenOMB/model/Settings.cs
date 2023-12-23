using System;
using System.IO;

namespace HydrogenOMB {
    public class Settings {
        private static Settings _instance;

        public string PortNameOnWin { get; private set; }
        public uint PortBaud { get; private set; }
        public ushort MaxDegrees { get; private set; }
        public bool OpenInExplorer { get; private set; }


        private Settings() { //Singleton Pattern
        }


        public static Settings Instance {
            get => _instance;
            set {
                if (value != null && Instance == null) {
                    _instance = value;
                }
            }
        }


        public static void Init() {
            Instance = new Settings();
            Instance.ReadSettings();
        }


        private void ReadSettings() {
            if (!File.Exists(PublicData.Instance.ConfigFileName)) {
                RecreateConfFile();
            }

            using (StreamReader sr = new StreamReader(PublicData.Instance.ConfigFileName)) {
                string[] elements = sr.ReadLine().Split(';');

                PortNameOnWin = elements[0];
                PortBaud = uint.Parse(elements[1]);
                MaxDegrees = ushort.Parse(elements[2]);
                OpenInExplorer = bool.Parse(elements[3]);
            }
        }

        public void WriteSettings(string portNameWin, uint portBaud, ushort maxDeg, bool openInExplorer) {
            using (StreamWriter sw = new StreamWriter(PublicData.Instance.ConfigFileName)) {
                sw.Write($"{portNameWin};{portBaud};{maxDeg};{openInExplorer}");
            }

            ReadSettings(); //cosi' si aggiornano anche le variabili globali nel codice (senno' si aggiornerebbe solo il file)
        }


        private void RecreateConfFile() { // ricreo il file delle configurazioni con dei valori di default
            using (StreamWriter sw = new StreamWriter(PublicData.Instance.ConfigFileName)) {
                sw.Write($"COM3;9600;100;true");
            }
        }
    }
}
