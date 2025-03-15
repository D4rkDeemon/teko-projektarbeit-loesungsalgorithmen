using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.JSInterop;
using System.Threading.Tasks;

/**
 * Handles everything related to user sessions
 */
public class SessionService
{
    public readonly ProtectedSessionStorage ProtectedSessionStorage;

    public SessionService(ProtectedSessionStorage _ProtectedSessionStore)
    {
        ProtectedSessionStorage = _ProtectedSessionStore;
    }

    public async Task<int?> GetUserId()
    {
        var val = await ProtectedSessionStorage.GetAsync<int?>("userId");
        return val.Value;
    }

    public async Task RemoveSession()
    {
        await ProtectedSessionStorage.DeleteAsync("userId");
    }
}