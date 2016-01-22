using System;
using FoodService.DAL.Interfaces;
using FoodService.DAL.RepositoryBag;

namespace FoodService.DAL
{
    public class UnityOfWork : IUnitOfWork
    {
        private readonly EntityContext _context = new EntityContext();
        private DishRepository _dishRepository;
        private OrderRepository _orderRepository;
        private DayDishSetRepository _dayDishSetRepository;
        private DishImageRepository _dishImageRepository;
        private UserRepository _userRepository;
        private RoleForUserRepository _roleForUser;
        private OrderDishRepository _orderDishes;
        private ReportRepository _report;

        private bool _disposed = false;

        public RoleForUserRepository Role => _roleForUser ?? (_roleForUser = new RoleForUserRepository(_context));

        public UserRepository User => _userRepository ?? (_userRepository = new UserRepository(_context));

        public DayDishSetRepository DayDish => _dayDishSetRepository ?? (_dayDishSetRepository = new DayDishSetRepository(_context));

        public DishRepository Dish => _dishRepository ?? (_dishRepository = new DishRepository(_context));

        public OrderRepository Order => _orderRepository ?? (_orderRepository = new OrderRepository(_context));

        public OrderDishRepository OrderDishes => _orderDishes ?? (_orderDishes = new OrderDishRepository(_context));

        public DishImageRepository DishImage
            => _dishImageRepository ?? (_dishImageRepository = new DishImageRepository(_context));

        public ReportRepository Report => _report ?? (_report = new ReportRepository(_context));

        public void Save()
        {
            _context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this._disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



    }
}