using System.Data.Entity;
using System.Linq;
using FoodService.DAL.Entity;
using FoodService.DAL.Interfaces;

namespace FoodService.DAL.RepositoryBag
{
    public class ReportRepository : IRepository<Report>
    {
        readonly EntityContext _context;

        public ReportRepository(EntityContext context)
        {
            this._context = context;
        }

        public IQueryable<Report> QueryToTable => _context.Report;

        public void Add(Report entity)
        {
            _context.Report.Add(entity);
        }

        public void Delete(Report entity)
        {
            _context.Report.Remove(entity);
        }

        public void Update(Report entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        
    }
}
