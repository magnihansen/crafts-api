using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.DataAccess;

namespace CraftsApi.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly IDataAccess _dataAccess;

        public UserApplication(IDataAccess dataAccess)
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
                @"SELECT * FROM User WHERE Id = @userId",
                new
                {
                    userId = userId
                }
            );
        }

        public async Task<DomainModels.User> GetUserByCredientialsAsync(string username, string password)
        {
            return await _dataAccess.LoadSingleData<DomainModels.User, dynamic>(
                @"SELECT * FROM User WHERE Username = @userName AND Password = @password",
                new
                {
                    userName = username,
                    password = password
                }
            );
        }

        public async Task<bool> AddUserAsync(DomainModels.User user)
        {
            string sql = @"
            INSERT INTO User (username,password,firstname,lastname,address,zip,city,country,email,phone,active,createdBy)
            VALUES (@username,@password,@firstname,@lastname,@address,@zip,@city,@country,@email,@phone,@active,@createdBy)
            ";
            int added = await _dataAccess.SaveData<dynamic>(sql, new
            {
                @username = user.Username,
                @password = user.Password,
                @firstname = user.Firstname,
                @lastname = user.Lastname,
                @address = user.Address,
                @zip = user.Zip,
                @city = user.City,
                @country = user.Country,
                @email = user.Email,
                @phone = user.Phone,
                @active = user.Active,
                @createdBy = user.CreatedBy
            });
            return added > 0;
        }

        public async Task<bool> UpdateUserAsync(DomainModels.User user)
        {
            string sql = @"
            UPDATE User SET
            username = @username,
            password = @password,
            firstname = @firstname,
            lastname = @lastname,
            address = @address,
            zip = @zip,
            city = @city,
            country = @country,
            email = @email,
            phone = @phone,
            active = @active,
            updateddate = @updateddate,
            updatedby = @updatedby
            WHERE Id = @id
            ";
            int updated = await _dataAccess.SaveData<dynamic>(sql, new
            {
                @id = user.Id,
                @username = user.Username,
                @password = user.Password,
                @firstname = user.Firstname,
                @lastname = user.Lastname,
                @address = user.Address,
                @zip = user.Zip,
                @city = user.City,
                @country = user.Country,
                @email = user.Email,
                @phone = user.Phone,
                @active = user.Active,
                @updateddate = user.UpdatedDate,
                @updatedby = user.UpdatedBy
            });
            return updated > 0;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            string sql = @"
            DELETE FROM User WHERE Id = @userId
            ";
            int deleted = await _dataAccess.SaveData<dynamic>(sql, new
            {
                @userId = userId
            });
            return deleted > 0;
        }
    }
}
