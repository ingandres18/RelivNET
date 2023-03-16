using Core.Entities;

namespace Core.Interfaces
{
    public interface IStateRepository
    {
        Task<IEnumerable<State>> GetStateAsync();
        Task<State> GetStateByIdAsync(int id);
        void AddState(State state);
        void UpdateState(State state);
        void DeleteState(int Id);
        void Save();
    }
}
