
using ExamMidTerm.Models;

namespace ExamMidTerm.Repositories
{
    public interface ICommuneRepository : IRepository<Commune>
    {
        void Update(Commune Commune);
    }
}