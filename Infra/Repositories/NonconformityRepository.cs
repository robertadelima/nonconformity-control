using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NonconformityControl.Api.ViewModels;
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
            return _context.Nonconformities.Include(p => p.Actions).AsNoTracking();
        } 

        public Nonconformity GetById(int id)
        {
            return _context.Nonconformities.Include(p => p.Actions).AsNoTracking().FirstOrDefault(p => p.Id == id);
        } 

        public void Add(Nonconformity nonconformity){
            _context.Nonconformities.Add(nonconformity);
            _context.SaveChanges();
        }

        public void AddActionToNonconformity(int id, Models.Action action)
        {
            Nonconformity nonconformity = _context.Nonconformities.FirstOrDefault(p => p.Id == id);
            nonconformity?.Actions?.Add(action);
            _context.SaveChanges();

        }
    }
}