using System;
using System.Linq;
using FoodService.DAL.Entity;

namespace FoodService.DAL.RepositoryBag
{
    public class DayDishSetRepository : IRepository<DayDishSet>
    {
        readonly EntityContext _context;

        public DayDishSetRepository()
        {
            _context = new EntityContext();
        }

        public DayDishSetRepository(EntityContext context)
        {
            this._context = context;
        }

        public IQueryable<DayDishSet> QueryToTable => _context.DayDishSets;

        //public IQueryable<DayDishSet> GetAllDishSetsOnDay(DateTime date)
        //{
        //    var k = _context.DayDishSets.Where(x => x.Date == date);
        //    return k;
        //}

        public void Add(int dishId, DateTime date)
        {
            var result = (from r in _context.Dishes where r.ID == dishId select r).FirstOrDefault();
            DayDishSet entity = new DayDishSet {Date = date, Dish = result};
            _context.DayDishSets.Add(entity);
        }

        public void Add(DayDishSet entity)
        {
            //throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            //var result = (from r in _context.DayDishSets where id == r.ID select r).FirstOrDefault();
            //_context.DayDishSets.Remove(result);
        }

        public void Delete(DayDishSet entity)
        {
            _context.DayDishSets.Remove(entity);
        }

        public void Update(DayDishSet entity)
        {
        }

        public DayDishSet FindById(int id)
        {
            //return (from r in _context.DayDishSets where r.ID == id select r).FirstOrDefault();
            return null;
        }
       
        //public void DeleteByDate(DateTime date)
        //{
        //    ////it work
        //    //var result = (from r in _context.DayDishSets where date == r.Date select r).FirstOrDefault();
        //    //while (result != null)
        //    //{
        //    //    _context.DayDishSets.Remove(result);
        //    //    _context.SaveChanges();
        //    //    result = (from r in _context.DayDishSets where date == r.Date select r).FirstOrDefault();
        //    //}


        //    //var dayMenu = _context.DayDishSets.Where(x => x.Date == date);
        //    //foreach (var day in dayMenu)
        //    //{
        //    //    _context.DayDishSets.Remove(day);
        //    //}


        //    //var result = (from r in context.WeekDishSets where date == r.Date select r).FirstOrDefault();
        //    //context.WeekDishSets.Remove(result);
        //    //context.SaveChanges();

        //}

    }
}
