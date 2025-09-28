using SensitiveWords.Api.Models;

namespace SensitiveWords.Api.Interfaces
{
    public interface ISensitiveWordRepository
    {
        Task<IEnumerable<SensitiveWord>> GetAllAsync();
        Task<SensitiveWord?> GetByIdAsync(int id);
        Task<SensitiveWord> CreateAsync(string word);
        Task<SensitiveWord?> UpdateAsync(SensitiveWord entity);
        Task<bool> DeleteAsync(int id);
    }
}
