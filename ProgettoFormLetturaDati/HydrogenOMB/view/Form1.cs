using System;
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

        public void SetStateOfValveDataInput(bool enabled) {
            if (InvokeRequired) {
                this.Invoke(new MethodInvoker(delegate {
                    textBoxModelValve.Enabled = textBoxNameValve.Enabled = enabled;
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


        public void PrintOn(Color col, DateTime ora, string mess) {
            if (InvokeRequired) {
                this.Invoke(new MethodInvoker(delegate {
                    richTextBoxAvvisi.SelectionColor = col;
                    richTextBoxAvvisi.AppendText($"{ora}: {mess}\n");
                }));
                return;
            }
        }

        private void InizializzaOggetti() {
            excMan = new ExcelManager($"{AppDomain.CurrentDomain.BaseDirectory}{PublicData.Instance.OutputDirectory}", PublicData.Instance.TemplateFileName, campi);
            dataMan = new DataManager(this, excMan, separ);

            string portName = PublicData.IsWindows() ? Settings.Instance.PortNameOnWin : Settings.Instance.PortNameOnLinux;
            serialReader = new SerialPortReader(portName, Settings.Instance.PortBaud, dataMan);
        }

        public void ResetValveFields() {
            if (InvokeRequired) {
                this.Invoke(new MethodInvoker(delegate {
                    textBoxModelValve.Text = textBoxNameValve.Text = "";
                    textBoxNameValve.Focus();
                }));
                return;
            }
        }

        public void StartSerialPort() { //E' concettualmente sbagliato che la porta seriale venga aperta dalla form
            serialReader.StartPort();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            serialReader.StopPort(); //in questo modo, se si e' su linux (ma anche Windows) si killa il thread in ascolto sulla seriale
        }
    }
}
