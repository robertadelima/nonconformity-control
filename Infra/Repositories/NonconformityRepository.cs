using System.Collections.Generic;
using NonconformityControl.Models;

namespace NonconformityControl.Infra.Repositories
{
    public class NonconformityRepository
    {
        private readonly NonconformityContext _context;
        public NonconformityRepository(NonconformityContext context)
        {
            _context = context;
        }
        public IEnumerable<Nonconformity> GetAll()
        {
            return _context.Nonconformities;
        } 

        public void Add(Nonconformity nonconformity){
            _context.Nonconformities.Add(nonconformity);
            _context.SaveChanges();
        }
    }
}