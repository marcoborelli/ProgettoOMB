using Newtonsoft.Json;

namespace HydrogenOMB {
    public class ValveFamily {
        private string _id;
        private CharacteristicValues _theoricValues;


        [JsonProperty("_id")]
        public string Id {
            get => _id;
            private set => PublicData.InsertIfObjValid(ref _id, value, "Id");
        }

        [JsonProperty("theoric_values")]
        public CharacteristicValues TheoricValues {
            get => _theoricValues;
            private set => PublicData.InsertIfObjValid(ref _theoricValues, value, "Theoric values");
        }
    }
}
