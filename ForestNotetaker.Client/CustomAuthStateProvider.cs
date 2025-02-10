using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace ForestNotetaker;

public class CustomAuthStateProvider(Supabase.Client supabaseClient) : AuthenticationStateProvider
{
    private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var session =  supabaseClient.Auth.CurrentSession;
        if (session?.User != null)
        {
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, session.User.Email!),
                new Claim(ClaimTypes.NameIdentifier, session.User.Id!)
            }, "SupabaseAuth"));
        }
        else
        {
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        }
        return Task.FromResult(new AuthenticationState(_currentUser));
    }
}
