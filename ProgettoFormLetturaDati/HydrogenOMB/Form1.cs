﻿using System;
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

        bool first = true;
        DateTime tempo = DateTime.Now, tempoOld = DateTime.Now, oraInizio;
        TimeSpan deltaTempo, tempoDaStart;

        SerialPort portaSeriale = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);

        private void Form1_Load(object sender, EventArgs e) {
            portaSeriale.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived); /*set the event handler*/
            timer1.Enabled = false;
            DataGridViewColumn delta = new DataGridViewTextBoxColumn();
            delta.HeaderText = "DELTA";
            dataGridView1.Columns.Add(delta);

            DataGridViewColumn timer = new DataGridViewTextBoxColumn();
            timer.HeaderText = "TIMER";
            dataGridView1.Columns.Add(timer);

            DataGridViewColumn tr1 = new DataGridViewTextBoxColumn();
            tr1.HeaderText = "TRIMMER 1";
            dataGridView1.Columns.Add(tr1);

            DataGridViewColumn tr2 = new DataGridViewTextBoxColumn();
            tr2.HeaderText = "TRIMMER 2";
            dataGridView1.Columns.Add(tr2);

            this.dataGridView1.Rows.Insert(0, "ciao", "ciao1", "ciao2", "ciao3");
        }

        private void timer1_Tick(object sender, EventArgs e) {
            deltaTempo = DateTime.Now - oraInizio;
            timerLab.Text = $"{deltaTempo.Minutes}:{deltaTempo.Seconds}:{deltaTempo.Milliseconds}";
        }

        private void stopBut_Click(object sender, EventArgs e) {/*termina*/
            portaSeriale.Close();
        }

        private void startBut_Click(object sender, EventArgs e) { /*inizia*/
            //timer1.Enabled = true;
            oraInizio = tempo;
            portaSeriale.Open();
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e) {
            //System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            tempo = DateTime.Now;
            tempoDaStart = tempo - oraInizio;
            string ricevuto = portaSeriale.ReadLine();
            string[] fields = ricevuto.Split(';');

            if (InvokeRequired) { // after we've done all the processing, 
                this.Invoke(new MethodInvoker(delegate {
                    if (first) {
                        this.dataGridView1.Rows.Insert(0, "", $"{tempoDaStart.Minutes}:{tempoDaStart.Seconds}:{tempoDaStart.Milliseconds}", fields[0], fields[1]);
                    } else {
                        deltaTempo = tempo - tempoOld;
                        this.dataGridView1.Rows.Insert(0, $"{deltaTempo.Minutes}:{deltaTempo.Seconds}:{deltaTempo.Milliseconds}", $"{tempoDaStart.Minutes}:{tempoDaStart.Seconds}:{tempoDaStart.Milliseconds}", fields[0], fields[1]);
                    }
                    tempoOld = tempo;
                    first = false;
                }));
                return;
            }


        }
    }
}
