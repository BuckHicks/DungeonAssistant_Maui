using DungeonAssistant.Business.Equipment.Interfaces;
using DungeonAssistant.Business.Equipment.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DungeonAssistant.Business.Equipment.Services;

public class MagicItemDataAccessService : IMagicItemDataAccessService
{
    //**************************************************\\
    //********************* Fields *********************\\
    //**************************************************\\
    private const string _baseUrl = "http://www.dnd5eapi.co";

    public async Task<List<MagicItemModel>> GetAllMagicItems()
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                client.BaseAddress = new Uri(_baseUrl);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("/api/magic-items");
                if (response.IsSuccessStatusCode)
                {
                    string rawJSON = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<MagicItemContainer>(rawJSON);
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

    public async Task<MagicItemModel> GetMagicItem(string name)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                client.BaseAddress = new Uri(_baseUrl);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                name = name.ToLower()
                    //.Replace(" form", "")
                    .Replace(" or ", "")
                    .Replace(" +", "")
                    .Replace("+", "")
                    .Replace(" ", "-")
                    .Replace("/", "-")
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace("'", "")
                    .Replace(":", "")
                    .Replace(" +", "")
                    .Replace("1", "")
                    .Replace("2", "")
                    .Replace("3", "")
                    .Replace("4", "")
                    .Replace("5", "")
                    .Replace(",", "");
                HttpResponseMessage response = await client.GetAsync("/api/magic-items/" + name);
                if (response.IsSuccessStatusCode)
                {
                    string rawJSON = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<MagicItemModel>(rawJSON);
                    return data;
                }
                else
                {
                    Console.WriteLine("HERE");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        return default(MagicItemModel);
    }


    //**************************************************\\
    //****************** Nested Class ******************\\
    //**************************************************\\
    private class MagicItemContainer
    {
        public MagicItemContainer(int count, List<MagicItemModel> results)
        {
            Count = count;
            Results = results;
        }

        public int Count { get; set; }
        public List<MagicItemModel> Results { get; set; }
    }
}
