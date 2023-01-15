using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CraftsApi.Helpers;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using MySqlConnector;

namespace CraftsApi.DataAccess
{
    public class DataAccess : IDataAccess
    {
        private readonly string _connectionString;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DataAccess(string connectionString, IWebHostEnvironment webHostEnvironment)
        {
            _connectionString = connectionString;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<T> LoadSingleData<T, U>(string sql, U parameters)
        {
            //using (StreamWriter file = new(_webHostEnvironment.WebRootPath + "LoadSingleData.txt", append: true))
            //{
            //    await file.WriteLineAsync(sql);
            //}

            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                T data = await connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
                return data != null ? data : default(T);
            }
        }

        public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var rows = await connection.QueryAsync<T>(sql, parameters);
                return rows.AsList();
            }
        }

        public async Task<int> SaveData<T>(string sql, T parameters)
        {
            //using (StreamWriter file = new(_webHostEnvironment.WebRootPath + "SaveData.txt", append: true))
            //{
            //    await file.WriteLineAsync(sql);
            //}

            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                int result = await connection.ExecuteAsync(sql, parameters);
                return result;
            }
        }

        public async Task<int> SaveDataWithReturn<T>(string sql, T parameters)
        {
            //using (StreamWriter file = new(_webHostEnvironment.WebRootPath + "SaveDataWithReturn.txt", append: true))
            //{
            //    await file.WriteLineAsync(sql);
            //}

            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                int results = await connection.ExecuteScalarAsync<int>(sql, parameters);
                return results;
            }
        }
    }
}
