using AdventofCode.Core.PuzzleInput.Repository;
using AdventofCode.Core.PuzzleInput.Service.Interface;
using System.Text.Json;

namespace AdventofCode.Core.PuzzleInput.Service
{
    public class PuzzleInputService : IPuzzleInputService
    {

        private readonly PuzzleInputRepository _puzzleInputRepository;

        public PuzzleInputService(PuzzleInputRepository puzzleInputRepository)
        {
            _puzzleInputRepository = puzzleInputRepository;
        }

        public async Task<T?> GetPuzzleInput<T>(int day, bool isTest)
        {
            string? jsonString;

            if(isTest)
            {
                jsonString = await _puzzleInputRepository.GetPuzzleTestInputAsync(day).ConfigureAwait(false);
            }
            else
            {
                jsonString = await _puzzleInputRepository.GetPuzzleInputAsync(day).ConfigureAwait(false);
            }

            if(string.IsNullOrEmpty(jsonString))
            {
                return default;
            }

            try
            {
                T? dataObject = JsonSerializer.Deserialize<T>(jsonString);

                return dataObject;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return default;
        }
    }
}
