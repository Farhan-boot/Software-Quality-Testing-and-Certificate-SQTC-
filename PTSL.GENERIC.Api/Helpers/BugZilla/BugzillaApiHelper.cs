using System.Net.Http.Headers;
using Newtonsoft.Json;
using PTSL.GENERIC.Common.Model.EntityViewModels.BugZilla;
//using System.Text.Json.Serialization;
namespace PTSL.GENERIC.Api.Helpers.BugZilla
{
    public class BugzillaApiHelper
    {
        public async static Task<BugZillaBugsResponseModel> GetBugsApiCall(string projectName, string url, string key)
        {
            string names = projectName;
            string type = "accessible";

            BugZillaBugsResponseModel bugs = new BugZillaBugsResponseModel();
            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(10) })
                {
                    client.BaseAddress = new Uri(url);
                    string contentType = "application/json";
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

                    HttpResponseMessage result = new HttpResponseMessage();
                    try
                    {
                        result = await client.GetAsync("?names=" + names + "&type=" + type + "&api_key=" + key);
                        var responseContent = result.Content.ReadAsStringAsync().Result;
                        bugs = JsonConvert.DeserializeObject<BugZillaBugsResponseModel>(responseContent);
                    }
                    catch (Exception ex)
                    {
                        bugs = new BugZillaBugsResponseModel();
                    }
                    
                }
            }
            catch (Exception ex)
            {

                bugs = new BugZillaBugsResponseModel();
            }
            
            return bugs;
        }
    }
}
