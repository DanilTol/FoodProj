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

        public IQueryable<RolesForUser> QueryToTable => _context.RolesForUsers;

        public void Add(RolesForUser entity)
        {
            _context.RolesForUsers.Add(entity);
        }

        public void Delete(RolesForUser entity)
        {
            _context.RolesForUsers.Remove(entity);
        }

        public void Update(RolesForUser entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}