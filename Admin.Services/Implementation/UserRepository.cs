
using Admin.Contract.DBObjects;
using Admin.Contract.Interface.Implementation;
using Admin.Contract.Interface.Infrastructure;
using Admin.Contract.Models.User;
using Dapper;
using System.Data;
using System.Data.Common;
using System.Reflection.Metadata;
using System.Text;
using static System.Net.Mime.MediaTypeNames;


namespace Admin.Services.Repository.Implementation
{
    public class UserRepository : IUserRepositry
    {
        private readonly IConnectionFactory _connectionFactory;
        public UserRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<LoginUser> LoginUserAsync(string UserId, string password)
        {
            string msg = "";
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    conn.Open();
                    var query = "SELECT u.User_Code,dbo.Decrypt(u.Password)Password, First_Name, Last_Name, Phone, Email_Address, Address1, Address2,r.Role,u.COMP_CODE FROM  WebUsers u  left join UserRole r on u.RoleId = r.Roleid  left join SYS s on u.COMP_CODE = s.COMP_CODE where User_Code = @User_Code";
                    return await conn.QueryFirstOrDefaultAsync<LoginUser>(query, new { User_Code = UserId });
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;

            }

        }

        public async Task<IEnumerable<T>> ExcuteQuaryAsync<T>(string sql, object parameter = null)
        {
            string msg = "";
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    conn.Open();
                    return await conn.QueryAsync<T>(sql, parameter);

                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;

            }

        }

        public async Task<IEnumerable<T>> ExcuteSPAsync<T>(string query, object parameter)
        {
            try
            {
                var result = await SqlMapper.QueryAsync<T>(_connectionFactory.GetConnection, query, parameter, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                return result?.ToList();
            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<T> GetSingleValueAsync<T>(string query, object parameters = null)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                conn.Open();
                return await conn.QueryFirstOrDefaultAsync<T>(query, parameters);

            }

            //for implementation   string query = "SELECT COUNT(*) FROM YourTable WHERE SomeCondition = @Param";
            // int result = await repository.GetSingleValueAsync<int>(query, new { Param = "some value" });


        }

        public async Task<T> GetSingleModelRecordAsync<T>(string query, object parameters = null)
        {
            string msg = "";
            try
            {
                using (var conn = _connectionFactory.GetConnection)
                {
                    conn.Open();
                    return await conn.QueryFirstOrDefaultAsync<T>(query, parameters);

                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                throw;

            }

        }
        public async Task<List<T>> GetRecordsAsync<T>(string query, object parameters = null)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                conn.Open();
                var result = await conn.QueryAsync<T>(query, parameters);
                var records = await SqlMapper.QueryAsync<T>(_connectionFactory.GetConnection, query, parameters);
                return result.ToList();
            }
        }

        public async Task<List<LoginUser>> RegistrationUserAsync(LoginUser loginUser)
        {
            var query = StoredProcedures.REGISTRION;
            var parameters = new DynamicParameters();
            parameters.Add("@User_Code", loginUser.User_Code);
            parameters.Add("@Password", loginUser.Password);
            parameters.Add("@First_Name", loginUser.First_Name);
            parameters.Add("@Last_Name", loginUser.Last_Name);
            parameters.Add("@Phone", loginUser.Phone);
            parameters.Add("@Email_Address", loginUser.Email_Address);
            parameters.Add("@Address1", loginUser.Address1);
            parameters.Add("@Address2", loginUser.Address2);

            var result = await SqlMapper.QueryAsync<LoginUser>(_connectionFactory.GetConnection, query, parameters, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            return result?.ToList();
        }

        public async Task<T> SingleModelSPAsync<T>(string query, object parameter)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                return await SqlMapper.QueryFirstOrDefaultAsync<T>(_connectionFactory.GetConnection, query, parameter, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

            }


        }

        public async Task<List<T>> ExcuteQurListAsync<T>(string query, object parameter)
        {
            var result = await SqlMapper.QueryAsync<T>(_connectionFactory.GetConnection, query, parameter, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            return result?.ToList();
        }

        //public async Task<string> EncryptAsync(string Text)
        //{
        //    Text = Text.Trim();
        //    string encryptionKey = "Angeera";
        //    StringBuilder encrypted = new StringBuilder();

        //    for (int i = 0; i < Text.Length; i++)
        //    {
        //        encrypted.Append((char)(Text[i] + encryptionKey[i % encryptionKey.Length]));
        //    }

        //    return encrypted.ToString(); 
        //}


        public async Task<string> DecryptAsync(string encryptedText)
        {
            string encryptionKey = "Angeera";
            StringBuilder decrypted = new StringBuilder();

            for (int i = 0; i < encryptedText.Length; i++)
            {
                int charValue = encryptedText[i] - encryptionKey[i % encryptionKey.Length];
                while (charValue < 0)
                {
                    charValue += 255; // Adjust to ensure within ASCII range
                }
                decrypted.Append((char)(charValue % 255)); // Modulo 255 to ensure within ASCII range
            }

            return decrypted.ToString();
        }

        public async Task<string> EncryptAsync(string text)
        {
            string encryptionKey = "Angeera";
            StringBuilder encrypted = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                int charValue = text[i] + encryptionKey[i % encryptionKey.Length];
                encrypted.Append((char)(charValue % 255)); // Modulo 255 to ensure within ASCII range
            }

            return encrypted.ToString();
        }


        //public async Task<string> DecryptAsync(string Text)
        //{
        //    string encryptionKey = "Angeera";
        //    StringBuilder decrypted = new StringBuilder();

        //    for (int i = 0; i < Text.Length; i++)
        //    {
        //        decrypted.Append((char)(Text[i] - encryptionKey[i % encryptionKey.Length]));
        //    }

        //    return decrypted.ToString();
        //}
    }
}
