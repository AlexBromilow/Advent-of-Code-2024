using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventofCode.Core.PuzzleInput.Repository
{
    public class PuzzleInputRepository
    {
        public readonly IConfiguration _config;

        public PuzzleInputRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string?> GetPuzzleInputAsync(int day)
        {
            return await GetFileContentAsync(day, false).ConfigureAwait(false);
        }

        public async Task<string?> GetPuzzleTestInputAsync(int day)
        {
            return await GetFileContentAsync(day, true).ConfigureAwait(false);
        }

        public async Task<string?> GetFileContentAsync(int day, bool isTest)
        {
            var memoryCache = new MemoryCache(new MemoryCacheOptions());

            string cacheKey = $"Day_{day}-isTest_{isTest}";

            string? cacheValue;

            if (!memoryCache.TryGetValue(cacheKey, out cacheValue))
            {
                var baseFilePath = $"{_config["AppSettings:WorkingDirectory"]}\\AppData\\PuzzleInputs\\Day{day}";

                string filePath;

                if (isTest)
                {
                    filePath = $"{baseFilePath}\\test_input.json";
                }
                else
                {
                    filePath = $"{baseFilePath}\\input.json";
                }

                if (!File.Exists(filePath))
                {
                    throw new FileLoadException($"{filePath} cannot be found");
                }

                cacheValue = await File.ReadAllTextAsync(filePath).ConfigureAwait(false);

                memoryCache.Set(cacheKey, cacheValue, TimeSpan.FromMinutes(10));
            }

            return cacheValue;
        }
    }
}
