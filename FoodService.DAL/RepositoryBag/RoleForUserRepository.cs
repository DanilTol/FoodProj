using System.Data.Entity;
using System.Linq;
using FoodService.DAL.Entity;
using FoodService.DAL.Interfaces;

namespace FoodService.DAL.RepositoryBag
{
    public class RoleForUserRepository : IRepository<Role>
    {
        private readonly EntityContext _context;

        public RoleForUserRepository(EntityContext con)
        {
            _context = con;
        }

        public IQueryable<Role> QueryToTable => _context.Role;

        public void Add(Role entity)
        {
            _context.Role.Add(entity);
        }

        public void Delete(Role entity)
        {
            _context.Role.Remove(entity);
        }

        public void Update(Role entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}