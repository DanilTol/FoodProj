using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using FoodService.DAL.Entity;
using FoodService.DAL.Interfaces;

namespace FoodService.DAL.RepositoryBag
{
    public class WeekDishSetRepository : IRepository<WeekDishSet>
    {
        readonly EntityContext context;

        public WeekDishSetRepository()
        {
            context = new EntityContext();
        }

        public WeekDishSetRepository(EntityContext context)
        {
            this.context = context;
        }

        public IQueryable<WeekDishSet> GetAll
        {
            get { return context.WeekDishSets; }
        }

        public IQueryable<WeekDishSet> GetAllDishSetsOnDay(DateTime date)
        {
            
            //int weekNumber = GetWeekOfYearNumber(date);
            //List<WeekDishSet> needeDishSets = new List<WeekDishSet>();
            //var fromDB = GetAll;

            //foreach (var s in fromDB.ToList())
            //{
            //    if ((GetWeekOfYearNumber(s.Date) == weekNumber) && (s.Date.Year == date.Year))
            //    {
            //     needeDishSets.Add(s);   
            //    }
            //}

            //var k = from s in context.WeekDishSets.AsEnumerable()
            //    where (GetWeekOfYearNumber(s.Date) == weekNumber) && (s.Date.Year == date.Year)
            //    select s;




            return (from r in context.WeekDishSets where r.Date == date select r);

            //return context.WeekDishSets.Where(s => (GetWeekOfYearNumber(s.Date) == weekNumber) && (s.Date.Year == date.Year));
        }

        //private int GetWeekOfYearNumber(DateTime date)
        //{
        //    DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
        //    Calendar cal = dfi.Calendar;
        //    return cal.GetWeekOfYear(date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

        //}


        public void Add(WeekDishSet entity)
        {
            var result = (from r in context.Dishes where r.ID == entity.DishId select r).FirstOrDefault();
            entity.Dish = result;
            context.WeekDishSets.Add(entity);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var result = (from r in context.WeekDishSets where id == r.ID select r).FirstOrDefault();
            context.WeekDishSets.Remove(result);
            context.SaveChanges();
        }

        public void Update(WeekDishSet entity)
        {
            var result = (from r in context.WeekDishSets where entity.ID == r.ID select r).FirstOrDefault();
            if (result != null)
            {
                result.Date = entity.Date;
                result.Dish = entity.Dish;
                
                context.Entry(result).State = EntityState.Modified;
            }

            context.SaveChanges();
        }

        public WeekDishSet FindById(int id)
        {
            var result = (from r in context.WeekDishSets where r.ID == id select r).FirstOrDefault();
            return result;
        }

        public IQueryable<WeekDishSet> Find(Func<WeekDishSet, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void DeleteByDate(DateTime date)
        {
            var result = (from r in context.WeekDishSets where date == r.Date select r).FirstOrDefault();
            while (result != null)
            {
                context.WeekDishSets.Remove(result);
                context.SaveChanges();
                result = (from r in context.WeekDishSets where date == r.Date select r).FirstOrDefault();
            }
            //var result = (from r in context.WeekDishSets where date == r.Date select r).FirstOrDefault();
            //context.WeekDishSets.Remove(result);
            //context.SaveChanges();

        }

    }
}
