using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            return _context.Nonconformities.AsNoTracking();
        } 

        public Nonconformity GetById(int id)
        {
            return _context.Nonconformities.AsNoTracking().FirstOrDefault(p => p.Id == id);
        } 

        public void Add(Nonconformity nonconformity){
            _context.Nonconformities.Add(nonconformity);
            _context.SaveChanges();
        }
    }
}