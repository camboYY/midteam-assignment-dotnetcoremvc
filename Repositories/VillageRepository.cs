using ExamMidTerm.Data;
using ExamMidTerm.Models;

namespace ExamMidTerm.Repositories
{
    public class VillageRepository : Repository<Village>, IVillageRepository
    {
        private ApplicationDbContext _db;

        public VillageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Village village)
        {
            _db.Villages.Update(village);
        }
    }
}