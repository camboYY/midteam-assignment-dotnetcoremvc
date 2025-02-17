
using ExamMidTerm.Models;

namespace ExamMidTerm.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        void Update(Person person);
    }
}