using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace HydrogenOMB {
    public class Test {
        private object _instanceId;
        private List<OMBRecord> _data;
        private DateTime _timestamp;


        [JsonProperty("instance_id")]
        public object InstanceId {
            get => _instanceId;
            private set {
                if (value is JObject) {
                    _instanceId = ((JObject)value).ToObject<ValveInstance>();
                } else {
                    _instanceId = value.ToString();
                }
            }
        }

        [JsonProperty("data")]
        public List<OMBRecord> Data {
            get => _data;
            set => PublicData.InsertIfObjValid(ref _data, value, "Data (OmbRecord)");
        }

        [JsonProperty("timestamp")]
        public DateTime Timestamp {
            get => _timestamp;
            set => PublicData.InsertIfObjValid(ref _timestamp, value, "Timestamp");
        }


        public Test(DateTime timestamp, object instanceId, List<OMBRecord> data) {
            Timestamp = timestamp;
            InstanceId = instanceId;
            Data = data;
        }
    }
}
