using ExamMidTerm.Data;
using ExamMidTerm.Models;

namespace ExamMidTerm.Repositories
{
    public class ProvinceRepository : Repository<Province>, IProvinceRepository
    {
        private ApplicationDbContext _db;

        public ProvinceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Province Province)
        {
            _db.Provinces.Update(Province);
        }
    }
}