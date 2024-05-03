namespace HydrogenOMB {
    public class CharacteristicValues {
        public float Bto { get; private set; } //break to open (0)
        public float Runo { get; private set; } //run open (45)
        public float Eto { get; private set; } //end to open (90)
        public float Btc { get; private set; } //break to close (90)
        public float Runc { get; private set; } //run close (45)
        public float Etc { get; private set; } //end to close (0)

        public CharacteristicValues(float bto, float runo, float eto, float btc, float runc, float etc) {
            Bto = bto;
            Runo = runo;
            Eto = eto;
            Btc = btc;
            Runc = runc;
            Etc = etc;
        }
    }
}
