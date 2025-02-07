using Microsoft.AspNetCore.Components;

namespace ForestNotetaker.Business.Services;

public class AuthService(Supabase.Client supabaseClient, NavigationManager navigationManager, SupabaseSettings supabaseSettings)
{
    public bool IsUserLoggedIn()
    {
        var session = supabaseClient.Auth.CurrentSession;
        return session?.User != null; 
    }

    public void LoginWithGoogle()
    {
        var connectionString = supabaseSettings.Endpoint;
        var redirectUri = navigationManager.ToAbsoluteUri("/auth/callback"); // The page that handles the callback
        var googleAuthUrl = $"{connectionString}/auth/v1/authorize?provider=google&redirect_to={redirectUri}";
        navigationManager.NavigateTo(googleAuthUrl);
    }

    public Task LogoutAsync() => supabaseClient.Auth.SignOut();
}
