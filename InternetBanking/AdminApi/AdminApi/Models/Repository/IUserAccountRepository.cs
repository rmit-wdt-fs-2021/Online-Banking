namespace AdminApi.Models.Repository
{
    public interface IUserAccountRepository : IDataRepository<Login, string>
    {
        public void LockAccount(string loginID);
    }
}
