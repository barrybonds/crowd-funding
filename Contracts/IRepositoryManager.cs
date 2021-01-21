using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IEndeavourRepository Endeavour { get; }
        ICategoryRepository Category { get; }
        ITypeRepository Type { get; }
        Task SaveAsync();
    }
}
