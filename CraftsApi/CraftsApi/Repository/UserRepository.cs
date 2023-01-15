using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.DataAccess;

namespace CraftsApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDataAccess _dataAccess;

        public UserRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<List<DomainModels.User>> GetUsersAsync()
        {
            return await _dataAccess.LoadData<DomainModels.User, dynamic>(
                "SELECT * FROM User",
                new { }
            );
        }

        public async Task<DomainModels.User> GetUserAsync(int userId)
        {
            return await _dataAccess.LoadSingleData<DomainModels.User, dynamic>(
                @"SELECT * FROM User WHERE Id = @UserId",
                new
                {
                    @UserId = userId
                }
            );
        }

        public async Task<DomainModels.User> GetUserByCredientialsAsync(string username, string password)
        {
            DomainModels.User user = await _dataAccess.LoadSingleData<DomainModels.User, dynamic>(
                @"SELECT * FROM User
                WHERE Username = @Username
                AND Password = @Password",
                new
                {
                    @Username = username,
                    @Password = password
                }
            );
            if (user == null)
            {
                return new DomainModels.User();
            }
            return user;
        }

        public async Task<bool> InsertUserAsync(DomainModels.User user)
        {
            string sql = @"
            INSERT INTO User (DomainId,username,password,firstname,lastname,address,zip,city,country,email,phone,active,createdBy)
            VALUES (DomainId,@Username,@Password,@Firstname,@Lastname,@Address,@Zip,@City,@Country,@Email,@Phone,@Active,@CreatedBy)
            ";
            int added = await _dataAccess.SaveData<dynamic>(sql, new
            {
                @DomainId = user.DomainId,
                @Username = user.Username,
                @Password = user.Password,
                @Firstname = user.Firstname,
                @Lastname = user.Lastname,
                @Address = user.Address,
                @Zip = user.Zip,
                @City = user.City,
                @Country = user.Country,
                @Email = user.Email,
                @Phone = user.Phone,
                @Active = user.Active,
                @CreatedBy = user.CreatedBy
            });
            return added > 0;
        }

        public async Task<bool> UpdateUserAsync(DomainModels.User user)
        {
            string sql = @"
            UPDATE User SET
            Username = @Username,
            Password = @Password,
            Firstname = @Firstname,
            Lastname = @Lastname,
            Address = @Address,
            Zip = @Zip,
            City = @City,
            Country = @Country,
            Email = @Email,
            Phone = @Phone,
            Active = @Active,
            Updateddate = @Updateddate,
            Updatedby = @Updatedby
            WHERE Id = @Id
            ";
            int updated = await _dataAccess.SaveData<dynamic>(sql, new
            {
                @Id = user.Id,
                @Username = user.Username,
                @Password = user.Password,
                @Firstname = user.Firstname,
                @Lastname = user.Lastname,
                @Address = user.Address,
                @Zip = user.Zip,
                @City = user.City,
                @Country = user.Country,
                @Email = user.Email,
                @Phone = user.Phone,
                @Active = user.Active,
                @Updateddate = user.UpdatedDate,
                @Updatedby = user.UpdatedBy
            });
            return updated > 0;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            string sql = @"
            DELETE FROM User
            WHERE Id = @UserId
            ";
            int deleted = await _dataAccess.SaveData<dynamic>(sql, new
            {
                @UserId = userId
            });
            return deleted > 0;
        }
    }
}
