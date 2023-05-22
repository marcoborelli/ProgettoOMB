using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace HydrogenOMB {
    public partial class Settings : Form {
        string configurationFileName, directoryName;
        bool first = true, modified = false;
        const string testoLabel = "MAX GRADI: ";
        public Settings(string configurationFileNamee, string directoryNamee) {
            InitializeComponent();
            ConfigurationFileName = configurationFileNamee;
            DirectoryName = directoryNamee;
        }
        private void Settings_Load(object sender, EventArgs e) {
            comboBoxPorta.DropDownStyle = ComboBoxStyle.DropDownList;
            for (byte i = 1; i < 7; i++) {
                comboBoxPorta.Items.Add($"COM{i}");
            }

            InizializzaValori();

            trackBarGradi_Scroll(sender, e);//perchè sennò non si aggiorna
            first = false;
        }

        /*properties*/
        public string ConfigurationFileName {
            get {
                return configurationFileName;
            }
            set {
                InserisciSeStringaValida(ref configurationFileName, value, "ConfigurationFileName");
            }
        }
        public string DirectoryName {
            get {
                return directoryName;
            }
            set {
                InserisciSeStringaValida(ref directoryName, value, "DirectoryName");
            }
        }
        /*fine properties*/

        private void trackBarGradi_Scroll(object sender, EventArgs e) {
            label2.Text = $"{testoLabel}{(trackBarGradi.Value * 5) + 90}";//+90 perchè il min è 90, *5 perchè aumenta di 5 in 5
            SettaModificato();
        }

        private void checkOpenExplorer_CheckedChanged(object sender, EventArgs e) {
            SettaModificato();
        }

        private void comboBoxPorta_SelectedIndexChanged(object sender, EventArgs e) {
            SettaModificato();
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
            if (modified) {
                var p = new FileStream(ConfigurationFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                p.Seek(0, SeekOrigin.Begin);
                using (BinaryWriter writer = new BinaryWriter(p)) {
                    writer.Write(comboBoxPorta.Text);
                    writer.Write((trackBarGradi.Value * 5) + 90);
                    writer.Write((bool)checkOpenExplorer.Checked);
                }
                p.Close();
            }
            modified = false;
            this.Visible = false;
        }

        private void InizializzaValori() {
            var p = new FileStream(ConfigurationFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            using (BinaryReader reader = new BinaryReader(p)) {
                comboBoxPorta.Text = reader.ReadString();
                trackBarGradi.Value = (reader.ReadInt32() - 90) / 5;
                checkOpenExplorer.Checked = reader.ReadBoolean();
            }
            p.Close();
        }
        private void SettaModificato() {
            if (first) {
                return;
            }
            modified = true;
        }
        private void InserisciSeStringaValida(ref string campo, string val, string perErrore) {
            if (!String.IsNullOrWhiteSpace(val)) {
                campo = val;
            } else {
                throw new Exception($"Invalid \"{perErrore}\"");
            }
        }
    }
}
