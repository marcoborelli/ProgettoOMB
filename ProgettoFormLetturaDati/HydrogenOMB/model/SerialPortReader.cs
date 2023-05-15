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
        private bool _first, _started, _endOpen;
        private char _separator;
        private byte _numeroParametri, _gradiMax;
        private int _gradiAttuali;

        public SerialPortReader(string ComPorta, char separator, byte numParametri,byte gradiMassimi, DataManager dManager, FileManager fManager) {
            _port = new SerialPort(ComPorta, 9600, Parity.None, 8, StopBits.One);
            Separator = separator;
            Port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived); /*set the event handler*/
            DManager = dManager;
            FManager = fManager;
            First = true;
            Started = false;
            EndOpen = false;
            NumeroParametri = numParametri;
            GradiMax = gradiMassimi;
            GradiAttuali = 0;
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
        public DateTime Now {
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
        private bool First {
            get {
                return _first;
            }
            set {
                _first = value;
            }
        }
        private bool Started {
            get {
                return _started;
            }
            set {
                _started = value;
            }
        }
        private bool EndOpen {
            get {
                return _endOpen;
            }
            set {
                _endOpen = value;
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

            if (tmp.ToUpper() == "START") {
                FManager.StartNewFile();
                Started = true;
                return;
            } else if (tmp.ToUpper() == "ENDOPEN") {
                EndOpen = true;
                return;
            } else if (tmp.ToUpper() == "END") {
                this.Stop();
                return;
            }

            if (!Started || GradiAttuali >= GradiMax || GradiAttuali < 0) {
                return;
            }

            Now = DateTime.Now;
            string final;
            string[] fields = tmp.Split(Separator);

            if (fields.Length != NumeroParametri) {
                fields = new string[NumeroParametri];
                for (byte i = 0; i < NumeroParametri; i++) {
                    fields[i] = "-";
                }
            }

            string HourMinSecMilTime = $"{Now.Hour}:{Now.Minute}:{Now.Second}:{Now.Millisecond}";
            string parametri = $"{Separator}";
            for (byte i = 0; i< NumeroParametri; i++) {
                parametri += $"{fields[i]}{Separator}";
            }
            parametri = parametri.Substring(0, parametri.Length - 1);

            if (First) {/*because first time i have no delta-time */
                final = $"{Separator}{HourMinSecMilTime}{parametri}";
                First = false;
            } else {
                DeltaTime = Now - OldTime;
                final = $"{DeltaTime.Minutes}:{DeltaTime.Seconds}:{DeltaTime.Milliseconds}{Separator}{HourMinSecMilTime}{parametri}";
            }

            DManager.PrintOnForm(0, final);
            FManager.Write(First, final);

            OldTime = Now;

            if (!EndOpen) {
                GradiAttuali++;
            } else {
                GradiAttuali--;
            }
        }
    }
}
