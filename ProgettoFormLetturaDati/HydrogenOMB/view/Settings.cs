﻿using System;
using System.Windows.Forms;
using System.IO;

namespace HydrogenOMB {
    public partial class Settings : Form {
        bool first = true, modified = false;
        string[] velocita = new string[] { "9600", "115200" };
        const string testoLabel = "MAX GRADI: ";
        public Settings() {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e) {
            comboBoxPorta.DropDownStyle = comboBoxVelocita.DropDownStyle = ComboBoxStyle.DropDownList;
            for (byte i = 1; i < 7; i++) {
                comboBoxPorta.Items.Add($"COM{i}");
            }
            for (byte i = 0; i < velocita.Length; i++) {
                comboBoxVelocita.Items.Add($"{velocita[i]}");
            }

            InizializzaValori();

            trackBarGradi_Scroll(sender, e);//perchè sennò non si aggiorna
            first = false;
        }

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
        private void comboBoxVelocita_SelectedIndexChanged(object sender, EventArgs e) {
            SettaModificato();
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
            if (modified) {
                using (StreamWriter sw = new StreamWriter(PublicData.ConfigFileName)) {
                    sw.Write($"{comboBoxPorta.Text};{comboBoxVelocita.Text};{(trackBarGradi.Value * 5) + 90};{checkOpenExplorer.Checked}");
                }
            }
            modified = false;
            Visible = false;
        }

        private void InizializzaValori() {
            using (StreamReader sr = new StreamReader(PublicData.ConfigFileName)) {
                string[] elements = sr.ReadLine().Split(';');
                comboBoxPorta.Text = elements[0];
                comboBoxVelocita.Text = elements[1];
                trackBarGradi.Value = (int.Parse(elements[2]) - 90) / 5;
                checkOpenExplorer.Checked = bool.Parse(elements[3]);
            }
        }

        private void SettaModificato() {
            if (first) {
                return;
            }
            modified = true;
        }
    }
}
