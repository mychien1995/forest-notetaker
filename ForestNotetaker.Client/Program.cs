using ForestNotetaker.Business;
using ForestNotetaker.Business.Services;
using ForestNotetaker;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var supabaseSetting = new SupabaseSettings(builder.Configuration.GetConnectionString("Supabase")!);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton(supabaseSetting);
builder.Services.AddSingleton(new Supabase.Client(supabaseSetting.Endpoint, Guid.NewGuid().ToString()));
builder.Services.AddSingleton<AuthService>();
builder.Services.AddSingleton<IDataService, DataService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddCascadingAuthenticationState();
await builder.Build().RunAsync();
