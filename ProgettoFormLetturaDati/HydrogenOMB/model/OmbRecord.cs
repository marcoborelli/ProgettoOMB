using System;

namespace HydrogenOMB {
    public class OMBRecord {
        private short _angle;
        private float _pair;
        private const byte ParamatersNumber = 2;


        public short Angle {
            get => _angle;
            private set => PublicData.InsertIfObjValid(ref _angle, value, "Angle");
        }

        public float Pair {
            get => _pair;
            private set => PublicData.InsertIfObjValid(ref _pair, value, "Pair");
        }


        public OMBRecord(string row, char separator) {
            string[] fields = row.Split(separator); //in caso ci siano più campi
            if (fields.Length != ParamatersNumber) {
                CampiDefault(ref fields);
            }

            if (int.Parse(fields[0]) > Settings.Instance.MaxDegrees) { //nel caso in cui i gradi (presenti all'indice 0) siano maggiori del limite imposto via software
                return;
            }

            Angle = short.Parse(fields[0]);
            Pair = float.Parse(fields[1]);
        }


        private void CampiDefault(ref string[] fields) {
            fields = new string[ParamatersNumber];
            for (byte i = 0; i < ParamatersNumber; i++) {
                fields[i] = "-";
            }
        }
    }
}
