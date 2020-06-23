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

        public Nonconformity Add(Nonconformity nonconformity){
            _context.Nonconformities.Add(nonconformity);
            _context.SaveChanges();

            var addedNonconformity = _context.Nonconformities.OrderByDescending(p => p.Id).FirstOrDefault();
            addedNonconformity = UpdateCodeWhenAdd(addedNonconformity);
            return addedNonconformity;
        }

        public void Remove(Nonconformity nonconformity){
            _context.Nonconformities.Remove(nonconformity);
            _context.SaveChanges();
        }

        public void AddActionToNonconformity(int id, Models.Action action)
        {
            Nonconformity nonconformity = _context.Nonconformities.FirstOrDefault(p => p.Id == id);
            nonconformity?.Actions?.Add(action);
            _context.SaveChanges();

        }

        public void UpdateAsEfficient(int id)
        {
            Nonconformity nonconformity = _context.Nonconformities.FirstOrDefault(p => p.Id == id);
            nonconformity.setAsEfficient();
            _context.Nonconformities.Update(nonconformity);
            _context.SaveChanges();
        }

        public void UpdateAsInefficient(int id)
        {
            Nonconformity nonconformity = _context.Nonconformities.FirstOrDefault(p => p.Id == id);
            nonconformity.setAsInefficient();
            _context.Nonconformities.Update(nonconformity);
            _context.SaveChanges();
        }

        private Nonconformity UpdateCodeWhenAdd(Nonconformity addedNonconformity)
        {
            var code = string.Concat((DateTime.UtcNow.Year) + ":" + addedNonconformity.Id.ToString("D2") + ":" 
                + addedNonconformity.Version.ToString("D2")); 
            addedNonconformity.UpdateCode(code);
            _context.Nonconformities.Update(addedNonconformity);
            _context.SaveChanges();
            return addedNonconformity;
        }
    }
}