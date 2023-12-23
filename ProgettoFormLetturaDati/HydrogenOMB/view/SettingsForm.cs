using System;
using System.Windows.Forms;
using System.IO;

namespace HydrogenOMB {
    public partial class SettingsForm : Form {
        bool modified = false;

        string[] velocita = new string[] { "9600", "115200" };
        string[] linuxPort = new string[] { "/dev/ttyACM0", "/dev/ttyACM1" };
        const string testoLabel = "MAX GRADI: ";

        const byte min = 90; //perche' l'angolo min e' 90 [90-120]
        const byte step = 5; //5 perche' aumenta di 5 in 5

        public SettingsForm() {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e) {
            comboBoxPorta.DropDownStyle = comboBoxVelocita.DropDownStyle = ComboBoxStyle.DropDownList;

            if (PublicData.IsWindows()) {
                for (byte i = 1; i < 7; i++) {
                    comboBoxPorta.Items.Add($"COM{i}");
                }
            } else {
                comboBoxPorta.Items.AddRange(linuxPort);
            }

            for (byte i = 0; i < velocita.Length; i++) {
                comboBoxVelocita.Items.Add($"{velocita[i]}");
            }

            InizializzaValori();

            trackBarGradi_Scroll(sender, e);//perche' senno' non si aggiorna
            modified = false; //la prima volta resetto perche' senno' conterebbe come modifica
        }

        private void trackBarGradi_Scroll(object sender, EventArgs e) {
            label2.Text = $"{testoLabel}{(trackBarGradi.Value * step) + min}";
            SettaModificato();
        }

        private void checkOpenExplorer_CheckedChanged(object sender, EventArgs e) {
            SettaModificato();
        }

        private void comboBoxPorta_SelectedIndexChanged(object sender, EventArgs e) {
            SettaModificato();
        }

        private void comboBoxVelocita_SelectedIndexChanged(object sender, EventArgs e) {
            SettaModificato();
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e) {
            //e.Cancel = true;
            if (modified) {
                DialogResult result = MessageBox.Show("Sono state modificate delle impostazioni, desideri salvare?", "CONFERMA", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) {
                    if (PublicData.IsWindows()) //perche' se sono su Win la porta di linux non la devo vedere e viceversa
                        Settings.Instance.WriteSettings(comboBoxPorta.Text, Settings.Instance.PortNameOnLinux, uint.Parse(comboBoxVelocita.Text), (ushort)(trackBarGradi.Value * step + min), checkOpenExplorer.Checked);
                    else
                        Settings.Instance.WriteSettings(Settings.Instance.PortNameOnWin, comboBoxPorta.Text, uint.Parse(comboBoxVelocita.Text), (ushort)(trackBarGradi.Value * step + min), checkOpenExplorer.Checked);
                }
            }
        }


        private void InizializzaValori() {
            comboBoxPorta.Text = PublicData.IsWindows() ? Settings.Instance.PortNameOnWin : Settings.Instance.PortNameOnLinux;
            comboBoxVelocita.Text = $"{Settings.Instance.PortBaud}";
            trackBarGradi.Value = (Settings.Instance.MaxDegrees - min) / step;
            checkOpenExplorer.Checked = Settings.Instance.OpenInExplorer;
        }

        private void SettaModificato() {
            modified = true;
        }
    }
}
