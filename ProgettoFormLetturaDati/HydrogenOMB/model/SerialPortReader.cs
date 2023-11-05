using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace HydrogenOMB {
    public class SerialPortReader {
        private EnhancedSerialPort _port;
        private FileManager _fManager;
        private DataManager _dManager;
        private char _separator;
        private bool First { get; set; }
        private bool Started { get; set; }
        private DateTime Now { get; set; }
        private DateTime OldTime { get; set; }
        private TimeSpan DeltaTime { get; set; }

        public byte NumeroParametri { get; private set; }
        public byte GradiMax { get; private set; }


        public SerialPortReader(string ComPorta,int VelocitaPorta, char separator, byte numParametri, byte gradiMassimi, DataManager dManager, FileManager fManager) {
            _port = new EnhancedSerialPort(ComPorta, VelocitaPorta, Parity.None, 8, StopBits.One);
            Port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived); /*set the event handler*/

            DManager = dManager;
            FManager = fManager;
            Separator = separator;
            NumeroParametri = numParametri;
            GradiMax = gradiMassimi;

            First = true;
            Started = false;
        }

        /*properties*/
        public EnhancedSerialPort Port {
            get {
                return _port;
            }
        }
        public DataManager DManager {
            get {
                return _dManager;
            }
            set {
                if (value != null) {
                    _dManager = value;
                } else {
                    throw new Exception("You must insert a valid DataManager");
                }
            }
        }
        public FileManager FManager {
            get {
                return _fManager;
            }
            set {
                if (value != null) {
                    _fManager = value;
                } else {
                    throw new Exception("You must insert a valid FileManager");
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

        public void Start() {
            Port.Open(); /* Begin communications*/
        }
        public void Stop() {
            Port.Close();//chiudo la porta
            FManager.Close();//chiudo e salvo il file di excel
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e) {
            //Console.WriteLine("Incoming line " + _port.ReadLine());
            string tmp = Port.ReadLine().ToUpper();

            switch (tmp) {
                case "START\r":
                    DManager.StartMeasurement("Inizio misurazione");
                    return;
                case "ENDOPEN\r":
                    DManager.EndOpening("Apertura valvola terminata, inzio chiusura...");
                    return;
                case "STOP\r":
                    DManager.StopMeasurement("Misurazione terminata con successo");
                    InizializzaExcel();//inizio già a prepararmi per ricevere i dati
                    FManager.ChangeWorkSheet(2);
                    return;
                case "FSTOP\r":
                    DManager.StopMeasurement("Misurazione fermata");
                    InizializzaExcel();
                    FManager.ChangeWorkSheet(2);
                    return;
                case "ENDARROPEN\r":
                    FManager.ChangeWorkSheet(3);//metto sul foglio di chiusura
                    FManager.SaveFile();//salvataggio backup(?)
                    return;
                case "ENDARRCLOSE\r":
                    this.Stop();
                    DManager.StopExcelWriting("File excel creato correttamente!\n");
                    return;
                default:
                    break;
            }

            if (!Started)
                return;

            List<string> fields = new List<string>(tmp.Split(Separator));//in caso ci siano più campi
            if (fields.Count != NumeroParametri) {
                CampiDefault(ref fields);
            }

            if (int.Parse(fields[0]) > GradiMax) {//nel caso in cui i gradi (presenti all'indice 0 per il momento) siano maggiori del limite imposto via software
                return;
            }

            Now = DateTime.Now;
            fields.Insert(0, $"{Now.Hour}:{Now.Minute}:{Now.Second}:{Now.Millisecond}");

            if (!First) {
                DeltaTime = Now - OldTime;
                fields.Insert(0, $"{DeltaTime.Minutes}:{DeltaTime.Seconds}:{DeltaTime.Milliseconds}");
            } else {//because first time i have no delta-time 
                fields.Insert(0, $"");
            }
            First = false;

            //DManager.PrintOnForm(0, fields);//per stampare sulla form
            FManager.Write(fields);//per stampare su file excel

            OldTime = Now;
        }

        private void CampiDefault(ref List<string> field) {
            field = new List<string>();
            for (byte i = 0; i < NumeroParametri; i++) {
                field[i] = "-";
            }
        }
        private void InizializzaExcel() {
            FManager.StartNewFile();
            Started = true;
            DManager.StartExcelWriting("Creazione file excel...");
        }
    }
}