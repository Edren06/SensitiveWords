using SensitiveWords.Api.Interfaces;
using SensitiveWords.Api.Models;
using System.Text.RegularExpressions;

namespace SensitiveWords.Api.Services
{
    /// <summary>
    /// Service which performs all of the sensitive word functions.
    /// </summary>
    public class SensitiveWordService : ISensitiveWordService
    {
        private readonly ISensitiveWordRepository _repo;
        private Regex? _regex;

        public SensitiveWordService(ISensitiveWordRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Used to get a list of words to be used with REGEX.
        /// </summary>
        /// <returns></returns>
        private async Task RefreshRegexAsync()
        {
            var words = await _repo.GetAllAsync();
            if (!words.Any()) return;

            // Escape special regex characters in words
            var escaped = words.Select(w => Regex.Escape(w.Word));
            var pattern = @"\b(" + string.Join("|", escaped) + @")\b";

            _regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        /// <summary>
        /// Get all the words in the table
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SensitiveWord>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        /// <summary>
        /// Create a new word
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public async Task<SensitiveWord> CreateAsync(string word)
        {
            var created = await _repo.CreateAsync(word);
            return created;
        }

        /// <summary>
        /// Update an existing word using an ID
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public async Task<SensitiveWord?> UpdateAsync(SensitiveWord s)
        {
            var updated = await _repo.UpdateAsync(s);
            return updated;
        }

        /// <summary>
        /// Delete an existing word with it's ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var ok = await _repo.DeleteAsync(id);
            return ok;
        }

        /// <summary>
        /// Bloop the message by getting a list of words populated in a regex expression in order to * out any sensitive words.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<string> BloopAsync(string message)
        {
            if (string.IsNullOrEmpty(message)) return message;

            // Refresh the regex before using it
            await RefreshRegexAsync();

            if (_regex == null) return message;

            return _regex.Replace(message, m => new string('*', m.Value.Length));
        }
    }
}
