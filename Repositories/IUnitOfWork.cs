using System;

namespace MVIOperationsSystem.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IDistrictRepository District { get; }
        IRegionRepository Region { get; }
        IEmployeeRepository Employee { get; }
        int Complete();
    }
}
   