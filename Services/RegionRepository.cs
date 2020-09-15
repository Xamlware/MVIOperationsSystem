using MVIOperationsSystem.Data;
using MVIOperationsSystem.Models;

namespace MVIOperationsSystem.Repositories
{
	public class RegionRepository : Repository<Region>, IRegionRepository
    {
        //OfflineContext _db = new OfflineContext();
        public RegionRepository(OfflineContext db)
            : base(db)
        {
        }

        //public IEnumerable<Region> GetTopSellingRegions(int count)
        //{
        //    return _db.Region.OrderByDescending(c => c.FullPrice).Take(count).ToList();
        //}

        //public IEnumerable<Region> GetRegionsWithAuthors(int pageIndex, int pageSize = 10)
        //{
        //    return PlutoContext.Regions
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