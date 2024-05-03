using Newtonsoft.Json;

namespace HydrogenOMB {
    public class ValveModel {
        private string _id;
        private ValveFamily _valveFamily;


        [JsonProperty("description")]
        public string Description { get; private set; }

        [JsonProperty("gear_model")]
        public string GearModel { get; private set; }

        [JsonProperty("ma_gear")]
        public float MAGear { get; private set; }

        [JsonProperty("_id")]
        public string Id {
            get => _id;
            private set => PublicData.InsertIfObjValid(ref _id, value, "Id");
        }

        [JsonProperty("valve_family")]
        public ValveFamily TheoricValues {
            get => _valveFamily;
            private set => PublicData.InsertIfObjValid(ref _valveFamily, value, "Valve Family");
        }
    }
}
