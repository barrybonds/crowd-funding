using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            
        }
        public void CreateCategory(Category endeavourCategory) => Create(endeavourCategory);

      
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges) => await FindAll(trackChanges).OrderBy(e => e.CategoryName).ToListAsync();
        public IEnumerable<Category> GetByIds(IEnumerable<Guid> ids, bool trackChanges) => FindByCondition(x => ids.Contains(x.Id), trackChanges).ToList();
        public async Task<Category> GetCategoryAsync(Guid endeavourCategoryId, bool trackChanges) => await FindByCondition(e => e.Id.Equals(endeavourCategoryId), trackChanges).SingleOrDefaultAsync();
        public void DeleteCategory(Category category)
        {
            DeleteCategory(category);
        }

    }
}
