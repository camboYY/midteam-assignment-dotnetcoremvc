using ExamMidTerm.Data;
using ExamMidTerm.Models;

namespace ExamMidTerm.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public IPersonRepository Person { get; private set; }
        public IProvinceRepository Province { get; private set; }
        public IVillageRepository Village { get; private set; }
        public IDistrictRepository District { get; private set; }
        public ICommuneRepository Commune { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Person = new PersonRepository(_db);
            Province = new ProvinceRepository(_db);
            Village = new VillageRepository(_db);
            District = new DistrictRepository(_db);
            Commune = new CommuneRepository(_db);
        }

        public void save()
        {
            _db.SaveChanges();
        }
    }
}