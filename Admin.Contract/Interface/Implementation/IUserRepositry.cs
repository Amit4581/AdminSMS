
using Admin.Contract.Models.User;

namespace Admin.Contract.Interface.Implementation
{
    public interface IUserRepositry
    {
        public Task<LoginUser> LoginUserAsync(string UserId, string password);
        public Task<List<LoginUser>> RegistrationUserAsync(LoginUser loginUser);
        public Task<IEnumerable<T>> ExcuteQuaryAsync<T>(string sql, object parameter = null);
        public Task<IEnumerable<T>> ExcuteSPAsync<T>(string query, object parameter);
        public Task<T> GetSingleValueAsync<T>(string query, object parameters = null);
        public Task<T> GetSingleModelRecordAsync<T>(string query, object parameters = null);
        public Task<List<T>> GetRecordsAsync<T>(string query, object parameters = null);


        public Task<T> SingleModelSPAsync<T>(string query, object parameter);
        public Task<List<T>> ExcuteQurListAsync<T>(string query, object parameter);

        public Task<string> EncryptAsync(string Text);
        public Task<string> DecryptAsync(string Text);

    }
}
