using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using blazorappca;
using System.Net.Http.Headers;
using SpotifyApp.Services;
using blazorappca.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddHttpClient<SpotifyService>(client =>
{
    client.BaseAddress = new Uri("https://spotify23.p.rapidapi.com/search");
    client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "a11cf6dff3msh6bd5e873b4b9b3ep18f22ejsn61929a88d79c");
    client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "spotify23.p.rapidapi.com");
});

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

await builder.Build().RunAsync();


var client = new HttpClient();
var request = new HttpRequestMessage
{
	Method = HttpMethod.Get,
	RequestUri = new Uri("https://spotify23.p.rapidapi.com/"),
	Headers =
	{
		{ "X-RapidAPI-Key", "a11cf6dff3msh6bd5e873b4b9b3ep18f22ejsn61929a88d79c" },
		{ "X-RapidAPI-Host", "spotify23.p.rapidapi.com" },
	},
};
using (var response = await client.SendAsync(request))
{
	response.EnsureSuccessStatusCode();
	var body = await response.Content.ReadAsStringAsync();
	Console.WriteLine(body);
}

builder.Services.AddScoped<SpotifyService>();
builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    });
