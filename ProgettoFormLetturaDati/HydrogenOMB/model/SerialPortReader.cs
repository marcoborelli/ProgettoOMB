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
        public byte GradiMax { get; private set; }
        public int GradiAttuali { get; private set; }
        public sbyte DeltaGradi { get; set; }


        public SerialPortReader(string ComPorta, char separator, byte numParametri, byte gradiMassimi, DataManager dManager, FileManager fManager) {
            _port = new SerialPort(ComPorta, 9600, Parity.None, 8, StopBits.One);
            Port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived); /*set the event handler*/

            DManager = dManager;
            FManager = fManager;

            Separator = separator;
            NumeroParametri = numParametri;
            GradiMax = gradiMassimi;

            First = true;
            Started = false;
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
                DeltaGradi = (sbyte)-DeltaGradi;
                return;
            } else if (tmp.ToUpper() == "STOP\r" ) {
                this.Stop("Misurazione terminata con successo");
                return;
            } else if (tmp.ToUpper() == "FSTOP\r") {
                this.Stop("Misurazione fermata");
                return;
            }

            if (!Started)
                return;

            GradiAttuali += DeltaGradi;//dato che c'è limite imponibile via software su angoli da ricevere devo comunque aumentare perchè sennò non aumenta più
            if ((GradiAttuali - DeltaGradi) >= GradiMax || (GradiAttuali - DeltaGradi) < 0) {
                return;
            }

            string[] fields = tmp.Split(Separator);//in caso ci siano più campi
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

            DManager.PrintOnForm(0, final);//per stampare sulla form
            FManager.Write(First, final);//per stampare su file excel

            OldTime = Now;
        }

        private string AggiuntaParametriInPiu(string[] field) {//restituisce la stringa finale con i parametri in fila separati da un separatore
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
