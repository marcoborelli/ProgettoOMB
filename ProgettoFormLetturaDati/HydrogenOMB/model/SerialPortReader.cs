using Microsoft.Office.Interop.Access.Dao;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HydrogenOMB {
    public class SerialPortReader {
        private SerialPort _port;
        private FileManager _fManager;
        private DataManager _dManager;
        private char _separator;
        private bool First, Started;
        private DateTime Now { get; set; }
        private DateTime OldTime { get; set; }
        private TimeSpan DeltaTime { get; set; }
        public byte NumeroParametri { get; private set; }
        //public byte GradiMax { get; private set; }
        //public int GradiAttuali { get; private set; }
        //public sbyte DeltaGradi { get; set; }


        public SerialPortReader(string ComPorta, char separator, byte numParametri, byte gradiMassimi, DataManager dManager, FileManager fManager) {
            _port = new SerialPort(ComPorta, 9600, Parity.None, 8, StopBits.One);
            Port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived); /*set the event handler*/

            DManager = dManager;
            FManager = fManager;

            Separator = separator;
            NumeroParametri = numParametri;
            //GradiMax = gradiMassimi;

            First = true;
            Started = false;
            //GradiAttuali = 0;
            //DeltaGradi = 1;
        }

        /*properties*/
        public SerialPort Port {
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
        public void Stop(string m) {
            Port.Close();//chiudo la porta
            FManager.Close();//chiudo e salvo il file di excel
            DManager.StopMeasurement(m);//agisco sulla form (fermo timer ed esce messageBox)
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e) {
            //Console.WriteLine("Incoming line " + _port.ReadLine());
            string tmp = Port.ReadLine();

            if (tmp.ToUpper() == "START\r") {
                FManager.StartNewFile();
                DManager.StartMeasurement();
                Started = true;
                return;
            } else if (tmp.ToUpper() == "ENDOPEN\r") {
                return;
            } else if (tmp.ToUpper() == "STOP\r") {
                this.Stop("Misurazione terminata con successo");
                return;
            } else if (tmp.ToUpper() == "FSTOP\r") {
                this.Stop("Misurazione fermata");
                return;
            }

            if (!Started)
                return;

            /*if ((GradiAttuali - DeltaGradi) >= GradiMax || (GradiAttuali - DeltaGradi) < 0) {
                return;
            }*/

            List<string> fields = new List<string>(tmp.Split(Separator));//in caso ci siano più campi
            if (fields.Count != NumeroParametri) {
                CampiDefault(ref fields);
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

            DManager.PrintOnForm(0, fields);//per stampare sulla form
            FManager.Write(First, fields);//per stampare su file excel

            OldTime = Now;
        }

        private void CampiDefault(ref List<string> field) {
            field = new List<string>();
            for (byte i = 0; i < NumeroParametri; i++) {
                field[i] = "-";
            }
        }
    }
}
