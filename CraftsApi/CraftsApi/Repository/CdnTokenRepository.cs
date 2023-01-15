using System.Threading.Tasks;
using CraftsApi.DataAccess;

namespace CraftsApi.Repository
{
    public class CdnTokenRepository : ICdnTokenRepository
    {
        private readonly IDataAccess _dataAccess;

        public CdnTokenRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<DomainModels.CdnToken> GetCdnTokenAsync(int cdnTokenId)
        {
            return await _dataAccess.LoadSingleData<DomainModels.CdnToken, dynamic>(
                @"SELECT * FROM CdnToken WHERE Id = @cdnTokenId",
                new
                {
                    @cdnTokenId = cdnTokenId
                }
            );
        }

        public async Task<bool> InsertCdnTokenAsync(DomainModels.CdnToken cdnToken)
        {
            string sql = @"
            INSERT INTO CdnToken (Token,ExpireDate,CreatedBy)
            VALUES (@token,@expireDate,@createdBy)
            ";

            int inserted = await _dataAccess.SaveData<dynamic>(sql, new
            {
                @token = cdnToken.Token,
                @expireDate = cdnToken.ExpireDate,
                @createdBy = cdnToken.CreatedBy
            });
            return inserted > 0;
        }

        public async Task<bool> UpdateCdnTokenAsync(DomainModels.CdnToken cdnToken)
        {
            string sql = @"
            UPDATE CdnToken SET
            Token = @token,
            ExpireDate = @expireDate,
            UpdatedDate = @updateddate,
            UpdatedBy = @updatedby
            WHERE Id = @cdnTokenId
            ";
            int updated = await _dataAccess.SaveData<dynamic>(sql, new
            {
                @cdnTokenId = cdnToken.Id,
                @token = cdnToken.Token,
                @expireDate = cdnToken.ExpireDate,
                @updateddate = cdnToken.UpdatedDate,
                @updatedby = cdnToken.UpdatedBy
            });
            return updated > 0;
        }

        public async Task<bool> DeleteCdnTokenAsync(int cdnTokenId)
        {
            string sql = @"
            DELETE FROM CdnToken
            WHERE Id = @cdnTokenId
            ";
            int deleted = await _dataAccess.SaveData<dynamic>(sql, new
            {
                @cdnTokenId = cdnTokenId
            });
            return deleted > 0;
        }
    }
}