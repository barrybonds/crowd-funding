using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Type = Entities.Models.Type;

namespace Contracts
{
    public interface IEndeavourRepository
    {
       // Task <System.Collections.Generic.IEnumerable<Endeavour>> GetAllEndeavoursAsync(bool trackChanges);
        Task<PagedList<Endeavour>> GetAllEndeavoursAsync(EndeavourParameters employeeParameters, bool trackChanges);
        Task<Endeavour> GetEndeavourAsync(Guid endeavourId, bool trackChanges);
        void CreateEndeavour(Endeavour endeavour);
        object GetAllEndeavoursAsync(bool v);
        void DeleteEndeavour(Endeavour endeavour);
       
    }

    public interface ICategoryRepository
    {
        Task<System.Collections.Generic.IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges);
        Task<Category> GetCategoryAsync(Guid endeavourCategoryId, bool trackChanges);
        void CreateCategory(Category endeavourCategory);
        System.Collections.Generic.IEnumerable<Category> GetByIds(System.Collections.Generic.IEnumerable<Guid> ids, bool trackChanges);
        void DeleteCategory(Category category);

    }

    public interface ITypeRepository
    {
        Task<System.Collections.Generic.IEnumerable<Type>> GetAllTypeAsync(bool trackChanges);
        Task<Type> GetTypeAsync(Guid endeavourRoleId, bool trackChanges);
        void CreateType(Entities.Models.Type endeRoleType);
        void DeleteType(Type type);
    }
    
}
