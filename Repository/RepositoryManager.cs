using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IEndeavourRepository _endeavourRepository;
        private ICategoryRepository _endeavourCategoryRepository;
        private ITypeRepository _endeavourRoleTypeRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }



        public IEndeavourRepository Endeavour
        {
            get
            {
                if (_endeavourRepository == null)
                    _endeavourRepository = new EndeavourRepository(_repositoryContext);
                return _endeavourRepository;
            }
        }

        public ICategoryRepository Category
        {
            get
            {
                if (_endeavourCategoryRepository == null)
                    _endeavourCategoryRepository = new CategoryRepository(_repositoryContext);
                return _endeavourCategoryRepository;
            }
        }

        public ITypeRepository Type
        {
            get
            {
                if (_endeavourRoleTypeRepository == null)
                    _endeavourRoleTypeRepository = new TypeRepository(_repositoryContext);
                return _endeavourRoleTypeRepository;
            }
        }

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
       
    }
}
