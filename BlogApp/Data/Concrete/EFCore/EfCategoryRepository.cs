using BlogApp.Data.Abstract;
using BlogApp.Data.Entity;
using IdentityApp.Data.Concrete.EFCore;

namespace BlogApp.Data.Concrete.EFCore
{
    public class EfCategoryRepository : ICategoryRepository
    {   

        private readonly BlogAppContext _context;

        public EfCategoryRepository(BlogAppContext context)
        {
            _context = context;
        }
        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetById(int categoryId)
        {
            return _context.Categories.Find(categoryId);
        }
    }
}