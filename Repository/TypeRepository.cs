using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = Entities.Models.Type;

namespace Repository
{
    public class TypeRepository : RepositoryBase<Type>, ITypeRepository
    {
        public TypeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
        public void CreateType(Type endeavourRoleType) => Create(endeavourRoleType);
        public async Task<IEnumerable<Type>> GetAllTypeAsync(bool trackChanges) => await FindAll(trackChanges).OrderBy(e => e.TypeName).ToListAsync();

        public async Task<Type> GetTypeAsync(Guid endeavourRoleTypeId, bool trackChanges) => await FindByCondition(e => e.Id.Equals(endeavourRoleTypeId), trackChanges).SingleOrDefaultAsync();

        public async Task<IEnumerable<Type>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) => await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();

        public void DeleteType(Type type)
        {
            DeleteType(type);
        }
    }



}
