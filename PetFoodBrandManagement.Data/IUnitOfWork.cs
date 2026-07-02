using PetFoodBrandManagement.Model.Entities;

namespace PetFoodBrandManagement.Data.Abstract
{
    public interface IUnitOfWork
    {
        IRepository<Brand> Brand { get; }
        IRepository<Category> Category { get; }
        IRepository<Product> Product { get; }
        IRepository<Order> Order { get; }
        IRepository<Review> Review { get; }
        IRepository<News> News { get; }
        IRepository<User> User { get; }

        void Save();
    }
}