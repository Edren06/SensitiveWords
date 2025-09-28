using SensitiveWords.Api.Interfaces;
using SensitiveWords.Api.Models;
using Dapper;

namespace SensitiveWords.Api.Repositories
{
    /// <summary>
    /// Sensitive word repository used to connect and perform functions against the database.
    /// This makes use of dapper to map back to an object to be sent back to the controllers.
    /// </summary>
    public class SensitiveWordRepository : ISensitiveWordRepository
    {
        private readonly IDbConnectionFactory _dbFactory;
        public SensitiveWordRepository(IDbConnectionFactory dbFactory) { _dbFactory = dbFactory; }

        public async Task<IEnumerable<SensitiveWord>> GetAllAsync()
        {
            using var conn = _dbFactory.CreateConnection();
            var sql = "SELECT Id, Word FROM dbo.SensitiveWords ORDER BY Word";
            return await conn.QueryAsync<SensitiveWord>(sql);
        }

        public async Task<SensitiveWord?> GetByIdAsync(int id)
        {
            using var conn = _dbFactory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<SensitiveWord>("SELECT * FROM dbo.SensitiveWords WHERE Id = @Id", new { Id = id });
        }

        public async Task<SensitiveWord> CreateAsync(string word)
        {
            using var conn = _dbFactory.CreateConnection();
            var sql = @"
                INSERT INTO dbo.SensitiveWords (Word)
                VALUES (@Word);
                SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = await conn.ExecuteScalarAsync<int>(sql, new { word });

            return new SensitiveWord() { Id = id, Word = word };
        }

        public async Task<SensitiveWord?> UpdateAsync(SensitiveWord entity)
        {
            using var conn = _dbFactory.CreateConnection();
            var sql = @"
                UPDATE dbo.SensitiveWords
                SET Word = @Word
                WHERE Id = @Id;
                SELECT Word FROM dbo.SensitiveWords WHERE Id = @Id;";

            var updated = await conn.QueryFirstOrDefaultAsync<SensitiveWord>(sql, entity);
            return updated;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = _dbFactory.CreateConnection();
            var sql = "DELETE FROM dbo.SensitiveWords WHERE Id = @Id";
            var rows = await conn.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}
