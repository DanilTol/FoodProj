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
        private DishToImageRepository _dishToImageRepository;
        private UserRepository _userRepository;
        private RoleForUserRepository _roleForUser;
        private UserSetRepository _userSetRepository;

        private bool _disposed = false;

        public RoleForUserRepository Role => _roleForUser ?? (_roleForUser = new RoleForUserRepository(_context));

        public UserRepository User => _userRepository ?? (_userRepository = new UserRepository(_context));

        public DayDishSetRepository DayDish => _dayDishSetRepository ?? (_dayDishSetRepository = new DayDishSetRepository(_context));

        public DishRepository Dish => _dishRepository ?? (_dishRepository = new DishRepository(_context));

        public OrderRepository Order => _orderRepository ?? (_orderRepository = new OrderRepository(_context));

        public DishToImageRepository DishToImage
            => _dishToImageRepository ?? (_dishToImageRepository = new DishToImageRepository(_context));

        public  UserSetRepository UserSet => _userSetRepository ?? (_userSetRepository = new UserSetRepository(_context));



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