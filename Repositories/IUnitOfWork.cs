
namespace ExamMidTerm.Repositories
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IPersonRepository Person { get; }
        IVillageRepository Village { get; }
        IProvinceRepository Province { get; }
        IDistrictRepository District { get; }
        ICommuneRepository Commune { get; }
        void save();
    }

}