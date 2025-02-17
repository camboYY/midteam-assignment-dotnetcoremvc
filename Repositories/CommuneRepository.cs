using ExamMidTerm.Data;
using ExamMidTerm.Models;

namespace ExamMidTerm.Repositories
{
    public class CommuneRepository : Repository<Commune>, ICommuneRepository
    {
        private ApplicationDbContext _db;

        public CommuneRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Commune Commune)
        {
            _db.Communes.Update(Commune);
        }
    }
}