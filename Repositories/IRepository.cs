using System.Linq.Expressions;
using X.PagedList;

namespace ExamMidTerm.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IPagedList<T>> GetAll(int? page = 1, string? inCludes = null);
        Task<T> Get(Expression<Func<T, bool>> filter, string? inCludes = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
