﻿using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;


namespace HydrogenOMB {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        string[] campi = new string[] { "delta", "time", "angle", "pair" };
        const char separ = ';';

        SerialPortReader serialReader;
        IDataManager dataMan;
        ExcelManager excMan;

        private void Form1_Load(object sender, EventArgs e) {
            PublicData.Init();
            Settings.Init();
            InizializzaOggetti();
            StartSerialPort();
        }

        private void buttonSettings_Click(object sender, EventArgs e) { //settings
            SettingsForm sForm = new SettingsForm(); //form delle impostazioni
            sForm.Show();
        }

        }

        public void StartMeasure(string mess) {
            if (InvokeRequired) {
                this.Invoke(new MethodInvoker(delegate {
                    textBoxModelValve.Enabled = textBoxNameValve.Enabled = false;
                    StampaSuRich(Color.Black, DateTime.Now, mess);
                }));
                return;
            }

        public void StopMeasure(string message) {
            if (InvokeRequired) { //se non metto questa parte non funziona. DA CHIEDERE
                this.Invoke(new MethodInvoker(delegate {
                    StampaSuRich(Color.Black, DateTime.Now, message);
                }));
                return;
            }
        }
        public void EndOpen(string message) {
            if (InvokeRequired) {
                this.Invoke(new MethodInvoker(delegate {
                    StampaSuRich(Color.Black, DateTime.Now, message);
                }));
                return;
            }
        }
        public void StartWritingExcel(string message) {
            if (InvokeRequired) {
                this.Invoke(new MethodInvoker(delegate {
                    StampaSuRich(Color.Black, DateTime.Now, message);
                }));
                return;
            }
        }
        public void StoptWritingExcel(string message) {
            if (InvokeRequired) {
                this.Invoke(new MethodInvoker(delegate {
                    StampaSuRich(Color.Green, DateTime.Now, message);

                    RipristinaCampi();//per prepararsi a rifare un'altra misurazione
                    StartSerialPort();

                    if (Settings.Instance.OpenInExplorer) {
                        string fileMan = PublicData.IsWindows() ? "explorer.exe" : "xdg-open";
                        Process.Start(fileMan, $"{AppDomain.CurrentDomain.BaseDirectory}{PublicData.Instance.OutputDirectory}");
                    }
                }));
                return;
            }
        }

        private void textBoxNameValvue_TextChanged(object sender, EventArgs e) {
            PublicData.Instance.InfoValve.NomeValvola = textBoxNameValve.Text;
        }

        private void textBoxModelValvue_TextChanged(object sender, EventArgs e) {
            PublicData.Instance.InfoValve.ModelloValvola = textBoxModelValve.Text;
        }


        private void StampaSuRich(Color col, DateTime ora, string mess) {
            richTextBoxAvvisi.SelectionColor = col;
            richTextBoxAvvisi.AppendText($"{ora}: {mess}\n");
        }
        private void InizializzaOggetti() {
            excMan = new ExcelManager($"{AppDomain.CurrentDomain.BaseDirectory}{PublicData.Instance.OutputDirectory}", PublicData.Instance.TemplateFileName, campi);
            dataMan = new DataManager(this, excMan, separ);

            string portName = PublicData.IsWindows() ? Settings.Instance.PortNameOnWin : Settings.Instance.PortNameOnLinux;
            serialReader = new SerialPortReader(portName, Settings.Instance.PortBaud, dataMan);
        }
        private void RipristinaCampi() {
            textBoxModelValve.Enabled = textBoxNameValve.Enabled = true;
            textBoxModelValve.Text = textBoxNameValve.Text = "";
            textBoxNameValve.Focus();
        }

        public void StartSerialPort() { //E' concettualmente sbagliato che la porta seriale venga aperta dalla form
            serialReader.StartPort();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            serialReader.StopPort(); //in questo modo, se si e' su linux (ma anche Windows) si killa il thread in ascolto sulla seriale
        }
    }
}
