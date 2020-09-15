using MVIOperationsSystem.Data;
using MVIOperationsSystem.Repositories;
using System.Data.Entity.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly OfflineContext _db;

    public UnitOfWork(OfflineContext db)
    {
        _db = db;
        District = new DistrictRepository(_db);
        Region = new RegionRepository(_db);
        Employee = new EmployeeRepository(_db);
    }

    public IDistrictRepository District { get; private set; }
    public IRegionRepository Region { get; private set; }
    public IEmployeeRepository Employee { get; private set; }

  public int Complete()
    {
        return _db.SaveChanges();
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
