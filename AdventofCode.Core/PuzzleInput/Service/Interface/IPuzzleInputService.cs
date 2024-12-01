namespace AdventofCode.Core.PuzzleInput.Service.Interface
{
    public interface IPuzzleInputService
    {
        Task<T?> GetPuzzleInput<T>(int day, bool isTest);
    }
}
