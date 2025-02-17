
using ExamMidTerm.Models;

namespace ExamMidTerm.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    }
}