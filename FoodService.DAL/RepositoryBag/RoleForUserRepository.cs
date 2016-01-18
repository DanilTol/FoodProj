using System.Data.Entity;
using System.Linq;
using FoodService.DAL.Entity;
using FoodService.DAL.Interfaces;

namespace FoodService.DAL.RepositoryBag
{
    public class RoleForUserRepository : IRepository<RolesForUser>
    {
        private readonly EntityContext _context;

        public RoleForUserRepository(EntityContext con)
        {
            _context = con;
        }

        public IQueryable<RolesForUser> QueryToTable => _context.Role;

        public void Add(RolesForUser entity)
        {
            _context.Role.Add(entity);
        }

        public void Delete(RolesForUser entity)
        {
            _context.Role.Remove(entity);
        }

        public void Update(RolesForUser entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}