namespace BMWStore.Services.Interfaces
{
    public interface ISeedDbService
    {
        void SeedAdmin(string password, string userName);
        void SeedRoles(params string[] roles);
    }
}
