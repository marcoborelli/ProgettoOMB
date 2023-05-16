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
        private DateTime _startTime, _now, _oldTime;
        private TimeSpan _deltaTime;
        private char _separator;
        private byte _numeroParametri, _gradiMax;
        private int _gradiAttuali;
        private bool First, Started;
        private sbyte DeltaGradi;

        public SerialPortReader(string ComPorta, char separator, byte numParametri, byte gradiMassimi, DataManager dManager, FileManager fManager) {
            _port = new SerialPort(ComPorta, 9600, Parity.None, 8, StopBits.One);
            Separator = separator;
            Port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived); /*set the event handler*/
            DManager = dManager;
            FManager = fManager;
            First = true;
            Started = false;
            NumeroParametri = numParametri;
            GradiMax = gradiMassimi;
            GradiAttuali = 0;
            DeltaGradi = 1;
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
                    throw new Exception("Inserire un ViewManager valido");
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
                    throw new Exception("Errore col FileManager");
                }
            }
        }
        public DateTime StartTime {
            get {
                return _startTime;
            }
            private set {
                _startTime = value;
            }
        }
        private DateTime Now {
            get {
                return _now;
            }
            set {
                _now = value;
            }
        }
        private DateTime OldTime {
            get {
                return _oldTime;
            }
            set {
                _oldTime = value;
            }
        }
        private TimeSpan DeltaTime {
            get {
                return _deltaTime;
            }
            set {
                _deltaTime = value;
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
                    throw new Exception("Invalid Char Separer");
                }
            }
        }
        public byte NumeroParametri {
            get {
                return _numeroParametri;
            }
            private set {
                _numeroParametri = value;
            }
        }
        public byte GradiMax {
            get {
                return _gradiMax;
            }
            private set {
                _gradiMax = value;
            }
        }
        public int GradiAttuali {
            get {
                return _gradiAttuali;
            }
            private set {
                _gradiAttuali = value;
            }
        }
        /*fine properties*/

        public void Start() {
            Port.Open(); /* Begin communications*/
        }
        public void Stop() {
            Port.Close();
            FManager.Close();
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e) {
            //Console.WriteLine("Incoming line " + _port.ReadLine());
            string tmp = Port.ReadLine();

            if (tmp.ToUpper() == "START\r") {
                FManager.StartNewFile();
                Started = true;
                return;
            } else if (tmp.ToUpper() == "ENDOPEN\r") {
                DeltaGradi = (sbyte)-DeltaGradi;
                Console.WriteLine($"{DeltaGradi}");
                return;
            } else if (tmp.ToUpper() == "STOP\r" || tmp.ToUpper() == "FSTOP\r") {
                this.Stop();
                return;
            }

            if (!Started)
                return;

            GradiAttuali += DeltaGradi;
            if ((GradiAttuali + DeltaGradi) >= GradiMax || (GradiAttuali + DeltaGradi) < 0) {
                return;
            }

            string[] fields = tmp.Split(Separator);
            ControlloNumeriCampi(ref fields);

            Now = DateTime.Now;
            string HourMinSecMilTime = $"{Now.Hour}:{Now.Minute}:{Now.Second}:{Now.Millisecond}";

            string parametri = $"{Separator}{(GradiAttuali - DeltaGradi)}{Separator}";//inizializzo questa stringa già con l'angolo (non lo conto come parametro perche' e' una cosa interna)
            parametri += AggiuntaParametriInPiu(fields);

            string final;
            if (First) {//because first time i have no delta-time 
                final = $"{Separator}{HourMinSecMilTime}{parametri}";
                First = false;
            } else {
                DeltaTime = Now - OldTime;
                final = $"{DeltaTime.Minutes}:{DeltaTime.Seconds}:{DeltaTime.Milliseconds}{Separator}{HourMinSecMilTime}{parametri}";
            }

            DManager.PrintOnForm(0, final);
            FManager.Write(First, final);

            OldTime = Now;
        }

        private string AggiuntaParametriInPiu(string[] field) {
            string p = "";
            for (byte i = 0; i < NumeroParametri; i++) {
                p += $"{field[i]}{Separator}";
            }
            p = p.Substring(0, p.Length - 1);//per togliere il ';' finale
            return p;
        }
        private void ControlloNumeriCampi(ref string[] field) {
            if (field.Length != NumeroParametri) {
                field = new string[NumeroParametri];
                for (byte i = 0; i < NumeroParametri; i++) {
                    field[i] = "-";
                }
            }
        }
    }
}
