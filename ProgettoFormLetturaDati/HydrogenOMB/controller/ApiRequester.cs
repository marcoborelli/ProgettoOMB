using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HydrogenOMB {
    public class ApiRequester {
        private HttpClient HttpClient { get; set; }
        private static ApiRequester _instance;

        public static ApiRequester Instance {
            get => _instance;
            private set {
                if (value != null && Instance == null) {
                    _instance = value;
                }
            }
        }


        private ApiRequester(string baseAddress) {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri(baseAddress);
        }


        public static void Init(string baseAddress) {
            Instance = new ApiRequester(baseAddress);
        }



        public async Task<bool> IsInstanceValid(string instanceId) {
            try {
                HttpResponseMessage response = await HttpClient.GetAsync($"/api/instances/{instanceId}");
                return response.IsSuccessStatusCode; //TODO: verificare cosa succede se il server non e' raggiungibile
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<string[]> GetAllInstances() {
            try {
                HttpResponseMessage response = await HttpClient.GetAsync($"/api/instances/all?showTests=false&showModel=false");

                if (response.IsSuccessStatusCode) {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    List<ValveInstance> instances = JsonConvert.DeserializeObject<List<ValveInstance>>(jsonString);
                    string[] ids = instances.Select(i => i.Id).ToArray();
                    return ids;
                }

                return null;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
