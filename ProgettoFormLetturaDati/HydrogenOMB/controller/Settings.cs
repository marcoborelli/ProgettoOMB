﻿using System;
using System.IO;

namespace HydrogenOMB {
    public class Settings {
        private static Settings _instance;

        public string PortNameOnWin { get; private set; }
        public string PortNameOnLinux { get; private set; }
        public uint PortBaud { get; private set; }
        public ushort MaxDegrees { get; private set; }
        public bool OpenInExplorer { get; private set; }
        public string BackendURL { get; private set; }


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
                PortNameOnLinux = elements[1];
                PortBaud = uint.Parse(elements[2]);
                MaxDegrees = ushort.Parse(elements[3]);
                OpenInExplorer = bool.Parse(elements[4]);
                BackendURL = elements[5];
            }
        }

        public void WriteSettings(string portNameWin, string portNameLinux, uint portBaud, ushort maxDeg, bool openInExplorer, string backendURL) {
            using (StreamWriter sw = new StreamWriter(PublicData.Instance.ConfigFileName)) {
                sw.Write($"{portNameWin};{portNameLinux};{portBaud};{maxDeg};{openInExplorer};{backendURL}");
            }

            ReadSettings(); //cosi' si aggiornano anche le variabili globali nel codice (senno' si aggiornerebbe solo il file)
        }


        private void RecreateConfFile() { // ricreo il file delle configurazioni con dei valori di default
            using (StreamWriter sw = new StreamWriter(PublicData.Instance.ConfigFileName)) {
                sw.Write($"COM3;/dev/ttyACM0;9600;100;true;http://84.33.120.138:9999/");
            }
        }
    }
}
