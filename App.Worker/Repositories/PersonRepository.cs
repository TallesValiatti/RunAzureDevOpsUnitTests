using App.Worker.Entities;

namespace App.Worker.Repositories
{
    // We could use the Entity core or other ORM
    public class PersonRepository : IPersonRepository
    {
        public async Task<Person?> GetByIdAsync(Guid id)
        {
            
            var person = Person.Create(Guid.Parse("b0d5eaf1-b457-44f7-ac85-d3913c5c5a55"),
                           "Talles Valiatti",
                           "tallesvaliatti@outlook.com");
            
            return await Task.FromResult(person.Value);
        }

        public Task UpdateAsync(Person person)
        {
            return Task.CompletedTask;
        }
    }
}