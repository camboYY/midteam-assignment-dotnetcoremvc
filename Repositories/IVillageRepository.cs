
using ExamMidTerm.Models;

namespace ExamMidTerm.Repositories
{
    public interface IVillageRepository : IRepository<Village>
    {
        void Update(Village village);
    }
}