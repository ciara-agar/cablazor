using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using blazorappca.Models;
using System.Text.Json;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace SpotifyApp.Services
{
    public class SpotifyService
    {
        private readonly HttpClient _httpClient;

        public SpotifyService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<ConcertData> ExploreConcerts()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://spotify23.p.rapidapi.com/concert/?id=6PodeS6Nvq7AwacfxsxHKT"),
                Headers =
                {
                    { "X-RapidAPI-Key", "a11cf6dff3msh6bd5e873b4b9b3ep18f22ejsn61929a88d79c" },
                    { "X-RapidAPI-Host", "spotify23.p.rapidapi.com" },
                },
            };

            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ConcertApiResponse>(content);

                return apiResponse?.Concert;
            }
        }

        public async Task<BrowseData> GetBrowseData()
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://spotify23.p.rapidapi.com/browse_all/"),
                    Headers =
                {
                    { "X-RapidAPI-Key", "a11cf6dff3msh6bd5e873b4b9b3ep18f22ejsn61929a88d79c" },
                    { "X-RapidAPI-Host", "spotify23.p.rapidapi.com" },
                },
                };

                using (var response = await _httpClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<BrowseData>(content);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<List<UserData>> SearchUser(string query)
        {
            var result = new List<UserData>();

            try
            {
                var apiUrl = $"https://spotify23.p.rapidapi.com/user_profile/?id={query}&playlistLimit=10&artistLimit=10"; // Replace with the actual Spotify API endpoint

                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var jsonObject = JObject.Parse(jsonString);

                    var users = jsonObject["users"];
                    var items = users?["items"];

                    if (items != null)
                    {
                        foreach (var item in items)
                        {
                            var user = new UserData();

                            user.Name = item["data"]["name"]?.ToString();
                            user.FollowersCount = item["data"]["followers"]?.ToString();
                            var profileArtArray = item["data"]["profileArt"]?["sources"];
                            user.ImageUrl = profileArtArray?[0]?["url"]?.ToString();

                            // Add additional property assignments based on your JSON structure

                            result.Add(user);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception/logging if needed
                Console.WriteLine($"Error: {ex.Message}");
            }

            return result;
        }


        private List<UserData> DeserializeUserResults(string json)
        {
            var result = new List<UserData>();

            try
            {
                var jsonObject = JObject.Parse(json);

                var users = jsonObject["users"];

                if (users != null)
                {
                    var items = users["items"];
                    if (items != null)
                    {
                        foreach (var item in items)
                        {
                            var userData = new UserData();

                            userData.Name = item["data"]?["name"]?.ToString();
                            userData.FollowersCount = item["data"]?["followers"]?.ToString();
                            userData.FollowingCount = item["data"]?["following"]?.ToString();
                            var profileImage = item["data"]?["images"]?["sources"];
                            if (profileImage != null)
                            {
                                userData.ImageUrl = profileImage[0]?["url"]?.ToString();
                            }

                            result.Add(userData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle parsing/deserialization error
                Console.WriteLine($"Error deserializing: {ex.Message}");
            }

            return result;
        }


        public async Task<List<Song>> SearchSongs(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return new List<Song>();
            }

            try
            {
                var formattedQuery = Uri.EscapeDataString(query);
                var apiUrl = $"https://spotify23.p.rapidapi.com/search/?q={formattedQuery}&type=track&offset=0&limit=10&numberOfTopResults=5";

                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return DeserializeSearchResults(json);
                }
                else
                {
                    Console.WriteLine($"Failed to fetch data: {response.ReasonPhrase}");
                    return new List<Song>();
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return new List<Song>();
            }
        }


        private List<Song> DeserializeSearchResults(string json)
        {
            var result = new List<Song>();

            try
            {
                var jsonObject = JObject.Parse(json);

                var albums = jsonObject["albums"];
                var items = albums["items"];

                foreach (var item in items)
                {
                    var song = new Song();

                    song.Name = item["data"]["name"].ToString();
                    song.Artist = item["data"]["artists"]["items"][0]["profile"]["name"].ToString();
                    song.AlbumUri = item["data"]["uri"].ToString();
                    song.ArtistUri = item["data"]["artists"]["items"][0]["uri"].ToString();

                    // Getting cover art
                    var coverArtArray = item["data"]["coverArt"]["sources"];
                    song.CoverUrl = coverArtArray[0]["url"].ToString(); // You might choose a specific size or source

                    song.ReleaseYear = item["data"]["date"]["year"].ToString();

                    // Other properties can be extracted similarly based on the JSON structure

                    // Adding the constructed song to the result list
                    result.Add(song);
                }
            }
            catch (Exception ex)
            {
                // Handle parsing/deserialization error
                Console.WriteLine($"Error deserializing: {ex.Message}");
            }

            return result;
        }

        public BrowseData DeserializeExploreResults(string json)
        {
            try
            {
                var exploreData = JsonConvert.DeserializeObject<BrowseData>(json);
                return exploreData;
            }
            catch (JsonSerializationException ex)
            {
                // Handle deserialization error
                Console.WriteLine($"Error deserializing: {ex.Message}");
                return null;
            }
        }

        public ConcertData DeserializeConcertResults(string json)
        {
            try
            {
                var concertData = JsonConvert.DeserializeObject<ConcertData>(json);
                return concertData;
            }
            catch (JsonSerializationException ex)
            {
                // Handle deserialization error
                Console.WriteLine($"Error deserializing: {ex.Message}");
                return null;
            }
        }

    }
    public class ConcertApiResponse
    {
        [JsonProperty("concert")]
        public ConcertData Concert { get; set; }
    }
}