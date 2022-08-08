using App.Worker.Entities;

namespace App.Worker.Repositories
{
    public interface IPersonRepository
    {
        Task<Person?> GetByIdAsync(Guid id);
        Task UpdateAsync(Person person);
    }
}