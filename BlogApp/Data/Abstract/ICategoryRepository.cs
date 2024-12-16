using BlogApp.Data.Entity;

namespace BlogApp.Data.Abstract
{
    public interface ICategoryRepository
    {
        void AddCategory(Category category);
        void DeleteCategory(int categoryId);
        Category GetById(int categoryId);
        List<Category> GetAllCategories();
    }
}