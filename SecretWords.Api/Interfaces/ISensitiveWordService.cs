using SensitiveWords.Api.Models;

namespace SensitiveWords.Api.Interfaces
{
    public interface ISensitiveWordService
    {
        Task<IEnumerable<SensitiveWord>> GetAllAsync();
        Task<SensitiveWord> CreateAsync(string word);
        Task<SensitiveWord?> UpdateAsync(SensitiveWord s);
        Task<bool> DeleteAsync(int id);
        Task<string> BloopAsync(string message);
    }
}
