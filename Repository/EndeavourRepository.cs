using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Extensions;

namespace Repository
{
    public class EndeavourRepository : RepositoryBase<Endeavour>, IEndeavourRepository
    {
        public EndeavourRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public void CreateEndeavour(Endeavour endeavour) => Create(endeavour);
    
        public async Task<Endeavour> GetEndeavourAsync(Guid endeavourId, bool trackChanges) => await FindByCondition(e => e.Id.Equals(endeavourId), trackChanges).SingleOrDefaultAsync();

        public async Task<System.Collections.Generic.IEnumerable<Endeavour>> GetByIdsAsync(System.Collections.Generic.IEnumerable<Guid> ids, bool trackChanges) => await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();

        public void DeleteEndeavour(Endeavour endeavour)
        {
            Delete(endeavour);
        }
       
        public async Task<PagedList<Endeavour>> GetAllEndeavoursAsync(EndeavourParameters endeavourParameters, bool trackChanges)
        {
             var endeavour = await FindAll(trackChanges).OrderBy(e => e.Name).ToListAsync();
            //TODO: Implement Filtering....Fix below code 
          //  var endeavour = await FindByCondition(e => e.GoalAmount.Equals(endeavourParameters.ValidGoalRange),trackChanges).FilterEndeavours(endeavourParameters.MinGoal, endeavourParameters.MaxGoal).Search(endeavourParameters.SearchTerm).OrderBy(e => e.Name).ToListAsync();
           // var employees = await FindByCondition(e => e.CompanyId.Equals(companyId),trackChanges).FilterEmployees(employeeParameters.MinAge, employeeParameters.MaxAge).Search(employeeParameters.SearchTerm).OrderBy(e => e.Name).ToListAsync();
            return PagedList<Endeavour>.ToPagedList(endeavour, endeavourParameters.PageNumber, endeavourParameters.PageSize);
        }

        public object GetAllEndeavoursAsync(bool v)
        {
            throw new NotImplementedException();
        }
    }
}
