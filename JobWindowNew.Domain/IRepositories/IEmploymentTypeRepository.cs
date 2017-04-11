using JobWindowNew.Domain.Model;
using System.Collections.Generic;

namespace JobWindowNew.Domain.IRepositories
{
    public interface IEmploymentTypeRepository
    {
        IEnumerable<EmploymentType> GetEmploymentTypes();
        EmploymentType GetEmploymentTypeById(int empId);
    }
}
