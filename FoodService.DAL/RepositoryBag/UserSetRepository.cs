using System.Data.Entity;
using System.Linq;
using FoodService.DAL.Entity;
using FoodService.DAL.Interfaces;

namespace FoodService.DAL.RepositoryBag
{
    public class UserSetRepository : IRepository<UserSet>
    {
        private readonly EntityContext _context;

        public UserSetRepository(EntityContext context)
        {
            _context = context;
        }

        public IQueryable<UserSet> QueryToTable => _context.UserSet;
        public void Add(UserSet entity)
        {
            _context.UserSet.Add(entity);
        }

        public void Delete(UserSet entity)
        {
            _context.UserSet.Remove(entity);
        }

        public void Update(UserSet entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        
    }
}