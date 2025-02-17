
using ExamMidTerm.Models;

namespace ExamMidTerm.Repositories
{
    public interface IDistrictRepository : IRepository<District>
    {
        void Update(District district);
    }
}