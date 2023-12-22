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

            Started = false;
            OldTime = DateTime.MinValue;
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
            Port.Open(); /* Begin communications*/
        }

        public void StopPort() {
            Port.Close();//chiudo la porta
        }

        private void Stop() {
            //perche' su windows non vengono generati altri thread. Le funzioni del fileManager e le altre sono eseguite dal thread principale, che richiedera' tra le varie cose di aprire una nuova istanza sulla stessa porta. Serve quindi che sia libera
            if (PublicData.IsWindows()) {
                this.StopPort();
            }
            DManager.OnEndArrayClose();

            //perche' su linux le operazioni di Excel... sono eseguite dal thread generato ad hoc per gli eventi da seriale. E' quindi obbligatorio che prima le altre attivita' siano completate e poi il thread sia killato
            if (!PublicData.IsWindows()) {
                this.StopPort();
            }
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
                    this.Stop();
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