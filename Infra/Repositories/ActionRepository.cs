using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NonconformityControl.Models;

namespace NonconformityControl.Infra.Repositories
{
    public class ActionRepository
    {
        private readonly NonconformityContext _context;

        public ActionRepository(NonconformityContext context)
        {
            _context = context;
        }

        public IEnumerable<Action> GetAll()
        {
            return _context.Actions.AsNoTracking().ToList();
        }

        public void Add(Action action){
            _context.Actions.Add(action);
            _context.SaveChanges();
        }

        public void Remove(Action action){
            _context.Actions.Remove(action);
            _context.SaveChanges();
        }
    }
}