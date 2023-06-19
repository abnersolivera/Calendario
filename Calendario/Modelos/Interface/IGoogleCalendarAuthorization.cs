using Google.Apis.Auth.OAuth2;

namespace Calendario.Modelos.Interface
{
    public interface IGoogleCalendarAuthorization
    {
        Task<UserCredential> GetUserCredential();

        Task<UserCredential> GetUserCredentialConsole();
    }
}
