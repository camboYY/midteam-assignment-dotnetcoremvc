using ExamMidTerm.Data;
using ExamMidTerm.Models;

namespace ExamMidTerm.Repositories
{
    public class DistrictRepository : Repository<District>, IDistrictRepository
    {
        private ApplicationDbContext _db;

        public DistrictRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(District district)
        {
            _db.Districts.Update(district);
        }
    }
}