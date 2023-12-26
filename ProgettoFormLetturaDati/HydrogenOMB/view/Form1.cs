using System;
using System.Windows.Forms;
using System.Drawing;


namespace HydrogenOMB {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        SerialPortReader serialReader;
        IDataManager dataMan;

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
            dataMan = new DataManager(this);

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

        public string[] GetValveFields() {
            return new string[] { textBoxNameValve.Text, textBoxModelValve.Text };
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            serialReader.StopPort(); //in questo modo, se si e' su linux (ma anche Windows) si killa il thread in ascolto sulla seriale
        }
    }
}
