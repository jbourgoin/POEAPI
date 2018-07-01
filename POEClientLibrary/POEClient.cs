using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace POEClientLibrary
{
    public class POEClient
    {
        private HttpClient HttpClient;
        public POEClient()
        {
            this.HttpClient = new HttpClient();
            this.HttpClient.BaseAddress = new Uri("http://api.pathofexile.com/");
        }

        public async Task<StashTab> GetPublicStashTabsASync()
        {
            return await this.getASync<StashTab>("public-stash-tabs");
        }

        public async Task<List<League>> GetLeaguesAsync(string type = null, string id = null)
        {

            string querystring = new QueryStringBuilder("leagues")
                .Add("type", type)
                .Add("id", id)
                .Builder();

            return await this.getASync<List<League>>(querystring);
        }

        private async Task<T> getASync<T>(string route)
        {


            HttpResponseMessage Message = await this.HttpClient.GetAsync(route);
            string Contentbody = await Message.Content.ReadAsStringAsync();

            switch ((int)Message.StatusCode)
            {
                case 200:
                    return JsonConvert.DeserializeObject<T>(Contentbody);
                default:
                    if (Contentbody == null)
                    {
                        throw new Exception("Unknown error / bad server request.");
                    }

                    Error ErrorMessage = JsonConvert.DeserializeObject<Error>(Contentbody);
                    throw new Exception($"Error Code: {ErrorMessage.error.code}  Message: {ErrorMessage.error.msg}");
            }
            
        }


    }

    public class Error
    {
        [JsonProperty("error")]
        public ErrorDetails error;
    }

    public class ErrorDetails
    {

        [JsonProperty("code")]
        public int code;
        [JsonProperty("message")]
        public string msg;
    }

    public class League
    {
        public string id;
        public string uri;
        public DateTime? startAt;
        public DateTime? endAt;
    }

    public class Stash
    {
        public string accountName;
        public string lastCharacterName;
        public string id;
        public string stash;
        public string stashtype;
        public item[] items;
        public bool @public;

    }

    public class StashTab
    {
        public string next_change_id;
        public Stash[] stashes;

    }

    public class item
    {

    }
}