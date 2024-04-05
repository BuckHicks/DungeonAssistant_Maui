using DungeonAssistant.Business.MonsterManual.Interfaces;
using DungeonAssistant.Business.MonsterManual.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DungeonAssistant.Business.MonsterManual.Services;

public class MonsterDataAccessService : IMonsterDataAccessService
{
    //**************************************************\\
    //********************* Fields *********************\\
    //**************************************************\\
    private const string _baseUrl = "http://www.dnd5eapi.co";

    public async Task<List<MonsterModel>> GetAllMonsters()
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                client.BaseAddress = new Uri(_baseUrl);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("/api/monsters");
                if (response.IsSuccessStatusCode)
                {
                    string rawJSON = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<MonsterContainer>(rawJSON);
                    return data.Results;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        return null;
    }

    public async Task<MonsterModel> GetMonster(string name)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                client.BaseAddress = new Uri(_baseUrl);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                name = name.ToLower()
                    .Replace(" form", "")
                    .Replace(" ", "-")
                    .Replace("/", "-")
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace("'", "")
                    .Replace(",", "");
                HttpResponseMessage response = await client.GetAsync("/api/monsters/" + name);
                if (response.IsSuccessStatusCode)
                {
                    string rawJSON = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<MonsterModel>(rawJSON);
                    return data;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        return default(MonsterModel);
    }


    //**************************************************\\
    //****************** Nested Class ******************\\
    //**************************************************\\
    private class MonsterContainer
    {
        public MonsterContainer(int count, List<MonsterModel> results)
        {
            Count = count;
            Results = results;
        }

        public int Count { get; set; }
        public List<MonsterModel> Results { get; set; }
    }
}
