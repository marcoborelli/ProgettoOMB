using System;
using System.Collections.Generic;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
using System.IO.Ports;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;


namespace HydrogenOMB {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        DateTime oraInizio;

        //SerialPort portaSeriale = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
        SerialPortReader serialReader;
        DataManager dataMan;

        private void Form1_Load(object sender, EventArgs e) {
            timer1.Stop();
            stopBut.Enabled = false;
            timer1.Enabled = false;

            dataGridView1.Columns.Add("delta", "DELTA");
            dataGridView1.Columns.Add("timer","TIME");
            dataGridView1.Columns.Add("tr1", "TRIMMER 1");
            dataGridView1.Columns.Add("tr2", "TRIMMER 2");

            dataMan = new DataManager(this);
            serialReader = new SerialPortReader("COM3", dataMan);
    }

        private void timer1_Tick(object sender, EventArgs e) {
            TimeSpan deltaTempo = DateTime.Now - oraInizio;
            timerLab.Text = $"{deltaTempo.Minutes}:{deltaTempo.Seconds}:{deltaTempo.Milliseconds}";
        }

        private void stopBut_Click(object sender, EventArgs e) {/*termina*/
            timer1.Stop();
            serialReader.Stop();

            stopBut.Enabled = false;
            startBut.Enabled = true;
        }

        private void startBut_Click(object sender, EventArgs e) { /*inizia*/
            dataGridView1.Rows.Clear();
            oraInizio = DateTime.Now;

            timer1.Start();
            serialReader.Start();

            stopBut.Enabled = true;
            startBut.Enabled = false;
        }

        public void PrintRow(int rowIndex, string[] fields) {
            if (InvokeRequired) { // after we've done all the processing, 
                this.Invoke(new MethodInvoker(delegate {
                    dataGridView1.Rows.Insert(rowIndex, fields);
                }));
                return;
            }
        }

    }
}
