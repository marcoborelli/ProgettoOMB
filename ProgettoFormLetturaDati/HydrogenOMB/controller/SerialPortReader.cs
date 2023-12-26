using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace HydrogenOMB {
    public class SerialPortReader {
        private EnhancedSerialPort _port;
        private IDataManager _dManager;
        private bool Started { get; set; }
        private DateTime OldTime { get; set; }


        public SerialPortReader(string ComPorta, uint VelocitaPorta, IDataManager dManager) {
            _port = new EnhancedSerialPort(ComPorta, (int)VelocitaPorta, Parity.None, 8, StopBits.One);
            Port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived); /*set the event handler*/

            DManager = dManager;
        }


        /*properties*/
        public EnhancedSerialPort Port {
            get => _port;
        }

        public IDataManager DManager {
            get => _dManager;
            private set => PublicData.InsertIfObjValid(ref _dManager, value, "DataManager");
        }
        /*fine properties*/


        public void StartPort() {
            Started = false;
            OldTime = DateTime.MinValue;

            Port.Open(); //Begin communications
        }

        public void StopPort() {
            Port.Close(); //chiudo la porta
        }


        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e) {
            //Console.WriteLine("Incoming line " + _port.ReadLine());
            string tmp = Port.ReadLine().ToUpper();

            switch (tmp) {
                case "START\r":
                    DManager.OnStart();
                    return;
                case "ENDOPEN\r":
                    DManager.OnEndOpen();
                    return;
                case "STOP\r":
                    DManager.OnStop();
                    Started = true;
                    return;
                case "FSTOP\r":
                    DManager.OnForcedStop();
                    Started = true;
                    return;
                case "ENDARROPEN\r":
                    DManager.OnEndArrayOpen();
                    return;
                case "ENDARRCLOSE\r":
                    DManager.OnEndArrayClose();
                    return;
                default:
                    break;
            }

            if (!Started)
                return;

            DManager.OnData(tmp, OldTime);
            OldTime = DateTime.Now;
        }
    }
}