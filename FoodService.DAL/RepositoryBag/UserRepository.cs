using System;
using System.Data.Entity;
using System.Linq;
using FoodService.DAL.Entity;

namespace FoodService.DAL.RepositoryBag
{
    public class UserRepository : IRepository<User>
    {

        private readonly EntityContext _context;

        public UserRepository(EntityContext context)
        {
            _context = context;
        }

        public IQueryable<User> GetAll {
            get { return _context.Users; }
                }
        public void Add(User entity)
        {
            entity.Role = _context.RolesForUsers.FirstOrDefault(x => x.Name == "user");
            _context.Users.Add(entity);
        }

        public void Delete(int id)
        {
            var result = (from r in _context.Users where id == r.ID select r).FirstOrDefault();
            _context.Users.Remove(result);

        }

        public void Update(User entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public User FindById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.ID == id);
        }

        public IQueryable<User> Find(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}