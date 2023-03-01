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
        private bool _first;

        public SerialPortReader(string ComPorta, DataManager dManager, FileManager fManager) {
            _port = new SerialPort(ComPorta, 9600, Parity.None, 8, StopBits.One);
            Port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived); /*set the event handler*/
            DManager = dManager;
            FManager = fManager;
            First = true;
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
        /*fine properties*/

        public void Start() {
            Port.Open(); /* Begin communications*/
            FManager.StartNewFile();
        }
        public void Stop() {
            Port.Close();
            FManager.Close();
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e) {
            //Console.WriteLine("Incoming line " + _port.ReadLine());

            Now = DateTime.Now;
            string final;
            string[] fields = Port.ReadLine().Split(';');

            if (fields.Length != 2) {
                fields = new string[] { "-", "-" };
            }

            string HourMinSecMilTime = $"{Now.Hour}:{Now.Minute}:{Now.Second}:{Now.Millisecond}";
            if (First) {/*because first time i have no delta-time */
                final = $";{HourMinSecMilTime};{fields[0]};{fields[1]}";
                First = false;
            } else {
                DeltaTime = Now - OldTime;
                final = $"{DeltaTime.Minutes}:{DeltaTime.Seconds}:{DeltaTime.Milliseconds};{HourMinSecMilTime};{fields[0]};{fields[1]}";
            }

            DManager.PrintOnForm(0, final);
            FManager.Write(First, final);

            OldTime = Now;
        }
    }
}
