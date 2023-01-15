using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.DataAccess;
using CraftsApi.DomainModels;
using CraftsApi.Helpers;
using CraftsApi.Service.ViewModels;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;

namespace CraftsApi.Repository
{
    public class SettingRepository : ISettingRepository
    {
        private readonly IDataAccess _dataAccess;

        public SettingRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<List<DomainModels.SettingDTO>> GetSettingsAsync(string host)
        {
            List<DomainModels.SettingDTO> settingDTOs = await _dataAccess.LoadData<DomainModels.SettingDTO, dynamic>(
                @"SELECT distinct s.Id, s.DomainId, s.SettingKeyId, sk.`Key`, ifnull(s.Value, sk.DefaultValue) AS Value, 
                s.CreatedDate, s.CreatedBy, s.UpdatedDate, s.UpdatedBy FROM Setting s
                INNER JOIN SettingKey sk
                ON s.SettingKeyId = sk.Id 
                INNER JOIN Domain d
                ON s.DomainId IS NULL OR (
                    s.DomainId = d.Id
                    AND (
                        d.Host = @host
                        OR 0 = STRCMP(@host,'localhost')
                    )
                    AND d.Active = 1)
                INNER JOIN SettingType st
                ON sk.SettingTypeId = st.Id
                GROUP BY sk.`Key` 
                ORDER BY -s.DomainId ASC",
                new {
                    @host = host
                }
            );
            return settingDTOs;
        }

        public async Task<List<DomainModels.SettingKey>> GetSettingKeysAsync()
        {
            List<DomainModels.SettingKey> settingKeys = await _dataAccess
                .LoadData<DomainModels.SettingKey, dynamic>(
                    @"
                    SELECT * FROM settingkey
                    ORDER BY `Key` ASC
                    ",
                    null
                );
            return settingKeys;
        }

        public async Task<List<DomainModels.SettingType>> GetSettingTypesAsync()
        {
            List<DomainModels.SettingType> settingTypes = await _dataAccess
                .LoadData<DomainModels.SettingType, dynamic>(
                    @"
                    SELECT * FROM settingtype
                    ORDER BY `Name` ASC
                    ",
                    null
                );
            return settingTypes;
        }

        public async Task<DomainModels.SettingDTO> GetSettingAsync(string host, int settingId)
        {
            return await _dataAccess.LoadSingleData<DomainModels.SettingDTO, dynamic>(
                @"
                SELECT distinct s.Id, s.DomainId, s.SettingKeyId, sk.`Key`, ifnull(s.Value, sk.DefaultValue) AS Value, 
                s.CreatedDate, s.CreatedBy, s.UpdatedDate, s.UpdatedBy FROM Setting s
                INNER JOIN SettingKey sk
                ON s.SettingKeyId = sk.Id 
                INNER JOIN Domain d
                ON s.DomainId = d.Id AND (d.Host = @host OR 0 = STRCMP(@host,'localhost')) AND d.Active = 1
                WHERE s.Id = @SettingId
                GROUP BY sk.`Key`
                ",
                new
                {
                    @host = host,
                    @SettingId = settingId
                }
            );
        }

        public async Task<DomainModels.SettingKey> GetSettingKeyAsync(int settingKeyId)
        {
            DomainModels.SettingKey settingKey = await _dataAccess.LoadSingleData<DomainModels.SettingKey, dynamic>(
                @"SELECT sk.* FROM SettingKey sk
                WHERE sk.Id = @SettingKeyId",
                new
                {
                    @SettingKeyId = settingKeyId
                }
            );
            return settingKey;
        }

        public async Task<DomainModels.SettingKey> GetSettingKeyByKeyAsync(string key)
        {
            DomainModels.SettingKey settingKey = await _dataAccess.LoadSingleData<DomainModels.SettingKey, dynamic>(
                @"SELECT sk.* FROM SettingKey sk
                WHERE sk.`Key` = @Key",
                new
                {
                    @Key = key
                }
            );
            return settingKey;
        }

        public async Task<DomainModels.SettingKey> InsertSettingKeyAsync(
            int settingTypeId,
            string key,
            bool isDefault,
            string defaultValue)
        {
            string sql = @"
            INSERT INTO SettingKey (SettingTypeId,`Key`,IsDefault,DefaultValue)
            VALUES (@SettingTypeId,@Key,@IsDefault,@DefaultValue);
            SELECT LAST_INSERT_ID();
            ";

            int insertId = await _dataAccess.SaveDataWithReturn<dynamic>(sql, new
            {
                @SettingTypeId = settingTypeId,
                @Key = key,
                @IsDefault = isDefault,
                @DefaultValue = defaultValue
            });

            DomainModels.SettingKey insertedSettingKey = await GetSettingKeyAsync(insertId);
            return insertedSettingKey;
        }

        public async Task<DomainModels.SettingDTO> InsertSettingAsync(
            string host,
            int? domainId,
            int settingKeyId,
            string value,
            string createdBy
            )
        {
            string sql = @"
            INSERT INTO Setting (DomainId,SettingKeyId,Value,CreatedBy)
            VALUES (@DomainId,@SettingKeyId,@Value,@CreatedBy);
            SELECT LAST_INSERT_ID();
            ";

            int insertId = await _dataAccess.SaveDataWithReturn<dynamic>(sql, new
            {
                @DomainId = domainId,
                @SettingKeyId = settingKeyId,
                @Value = value,
                @CreatedBy = createdBy
            });

            DomainModels.SettingDTO insertedSetting = await GetSettingAsync(host, insertId);
            return insertedSetting;
        }

        public async Task<bool> UpdateSettingAsync(
            int id,
            int? domainId,
            int settingKeyId,
            string value,
            string updatedBy)
        {
            string sql = @"
            UPDATE Setting s SET
            s.SettingKeyId = @SettingKeyId,
            s.Value = @Value,
            s.UpdatedDate = NOW() + INTERVAL 1 HOUR,
            s.UpdatedBy = @UpdatedBy
            WHERE s.Id = @Id
            AND s.DomainId = @DomainId
            ";
            int updated = await _dataAccess.SaveData<dynamic>(sql, new
            {
                @Id = id,
                @DomainId = domainId,
                @SettingKeyId = settingKeyId,
                @Value = value,
                @UpdatedBy = updatedBy
            });
            return updated > 0;
        }

        public async Task<bool> DeleteSettingAsync(string host, int settingId)
        {
            string sql = @"
            DELETE s FROM Setting s
            INNER JOIN Domain d
            ON s.DomainId = d.Id AND (d.Host = @host OR 0 = STRCMP(@host,'localhost')) AND d.Active = 1
            WHERE s.Id = @SettingId
            ";
            int deleted = await _dataAccess.SaveData<dynamic>(sql, new
            {
                @host = host,
                @SettingId = settingId
            });
            return deleted > 0;
        }
    }
}
