using ExamMidTerm.Data;
using ExamMidTerm.Models;

namespace ExamMidTerm.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        private ApplicationDbContext _db;

        public PersonRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Person person)
        {
            _db.Persons.Update(person);
        }
    }
}