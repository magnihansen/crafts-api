namespace CraftsApi.Auth
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(Users users);
    }
}
