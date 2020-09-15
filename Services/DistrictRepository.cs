using MVIOperations.Models;
using MVIOperationsSystem.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MVIOperationsSystem.Repositories
{
	public class DistrictRepository : Repository<District>, IDistrictRepository
    {
        //OfflineContext _db = new OfflineContext();
        public DistrictRepository(OfflineContext db)
            : base(db)
        {
        }

        //public IEnumerable<District> GetTopSellingDistricts(int count)
        //{
        //    return _db.District.OrderByDescending(c => c.FullPrice).Take(count).ToList();
        //}

        //public IEnumerable<District> GetDistrictsWithAuthors(int pageIndex, int pageSize = 10)
        //{
        //    return PlutoContext.Districts
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