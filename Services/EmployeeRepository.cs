using MVIOperations.Models;
using MVIOperationsSystem.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MVIOperationsSystem.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        //OfflineContext _db = new OfflineContext();
        public EmployeeRepository(OfflineContext db)
            : base(db)
        {
        }

        //public IEnumerable<Employee> GetTopSellingEmployees(int count)
        //{
        //    return _db.Employee.OrderByDescending(c => c.FullPrice).Take(count).ToList();
        //}

        //public IEnumerable<Employee> GetEmployeesWithAuthors(int pageIndex, int pageSize = 10)
        //{
        //    return PlutoContext.Employees
        //        .Include(c => c.Author)
        //        .OrderBy(c => c.Name)
        //        .Skip((pageIndex - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToList();
        //}

        //public PlutoContext PlutoContext
        //{
        //    get { return Context as PlutoContext; }
        //}
    }
}