using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydrogenOMB {
    public class SerialPortReader {
        private SerialPort _port;
        private ViewManager _vManager;

        public SerialPortReader(string ComPorta, ViewManager vManager) {
            _port = new SerialPort(ComPorta, 9600, Parity.None, 8, StopBits.One);
            _port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived); /*set the event handler*/
            VManager = vManager;
        }

        /*properties*/
        public SerialPort Port {
            get {
                return _port;
            }
        }
        public ViewManager VManager {
            get {
                return _vManager;
            }
            set {
                if (value != null) {
                    _vManager = value;
                } else {
                    throw new Exception("Inserire un ViewManager valido");
                }
            }
        }
        /*fine properties*/

        public void Start() {
            _port.Open(); /* Begin communications*/
        }
        public void Stop() {
            _port.Close();
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e) {
            //Get and Show a received line (all characters up to a serial New Line character)
            //Console.WriteLine("Incoming line " + _port.ReadLine());
            VManager.PrintOnForm(_port.ReadLine());
        }
    }
}
