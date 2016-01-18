using System;
using System.Data.Entity;
using System.Linq;
using FoodService.DAL.Entity;
using FoodService.DAL.Interfaces;

namespace FoodService.DAL.RepositoryBag
{
    public class UserRepository : IRepository<User>
    {
        private readonly EntityContext _context;

        public UserRepository(EntityContext context)
        {
            _context = context;
        }

        public IQueryable<User> QueryToTable => _context.User;

        public void Add(User entity)
        {
            _context.User.Add(entity);
        }

        public void Delete(User entity)
        {
            _context.User.Remove(entity);
        }

        public void Update(User entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}