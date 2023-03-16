using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Logic
{
    public class StateRepository : IStateRepository
    {
        private readonly RelivDbContext _context;
        public StateRepository(RelivDbContext context)
        {
            _context = context;
        }
        public async void DeleteState(int Id)
        {
            var stateToDelete = _context.States.Find(Id);

            if(stateToDelete!=null) _context.Remove(stateToDelete);
            Save();
            //_context.States.Remove(stateToDelete);

            //return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<State>> GetStateAsync()
        {
            return await _context.States.ToListAsync();
        }

        public async Task<State> GetStateByIdAsync(int Id)
        {
            return await _context.States.FindAsync(Id);
        }

        public async void AddState(State state)
        {
            _context.States.Add(state);
            Save();
            //return await _context.SaveChangesAsync() > 0;
        }

        public async void UpdateState(State state)
        {
            _context.Entry(state).State = EntityState.Modified;
            Save();
            //var stat = await _context.States.FindAsync(state.StateId);

            //stat.Description = state.Description;

            //_context.Update(stat);
            //return await _context.SaveChangesAsync() > 0;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
