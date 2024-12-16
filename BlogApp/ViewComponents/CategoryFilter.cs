using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.ViewComponents
{
    public class CategoryFilter : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryFilter(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryRepository.GetAllCategories();
            return View(categories);
        }
    }
}