
using ExamMidTerm.Models;

namespace ExamMidTerm.Repositories
{
    public interface IProvinceRepository : IRepository<Province>
    {
        void Update(Province province);
    }
}