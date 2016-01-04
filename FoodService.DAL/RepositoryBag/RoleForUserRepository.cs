using System;
using System.Linq;
using FoodService.DAL.Entity;

namespace FoodService.DAL.RepositoryBag
{
    public class RoleForUserRepository : IRepository<RolesForUser>
    {
        private readonly EntityContext _context;

        public RoleForUserRepository(EntityContext con)
        {
            _context = con;
        }

        public IQueryable<RolesForUser> QueryToTable { get { return _context.RolesForUsers; } }
        public void Add(RolesForUser entity)
        {
            //empty
        }

        public void Delete(int id)
        {
            // empty
        }

        public void Update(RolesForUser entity)
        {
            // empty
        }

        public RolesForUser FindById(int id)
        {
           return _context.RolesForUsers.FirstOrDefault(x => x.ID == id);
        }

        public IQueryable<RolesForUser> Find(Func<RolesForUser, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}