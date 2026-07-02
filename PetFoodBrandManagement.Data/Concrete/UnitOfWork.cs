using PetFoodBrandManagement.Data.Abstract;
using PetFoodBrandManagement.Data.Context;
using PetFoodBrandManagement.Model.Entities;

namespace PetFoodBrandManagement.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IRepository<Brand> Brand { get; private set; }
        public IRepository<Category> Category { get; private set; }
        public IRepository<Product> Product { get; private set; }
        public IRepository<Order> Order { get; private set; }
        public IRepository<Review> Review { get; private set; }
        public IRepository<News> News { get; private set; }
        public IRepository<User> User { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Brand = new Repository<Brand>(_context);
            Category = new Repository<Category>(_context);
            Product = new Repository<Product>(_context);
            Order = new Repository<Order>(_context);
            Review = new Repository<Review>(_context);
            News = new Repository<News>(_context);
            User = new Repository<User>(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}