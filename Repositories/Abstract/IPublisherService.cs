using BookStore.Models.Domain;

namespace BookStore.Repositories.Abstract
{
    public interface IPublisherService
    {
        Task<bool> Add(Publisher model);
        Task<bool> Update(Publisher model);
        Task<bool> Delete(int id);
        Task<Publisher> GetById(int id);
        Task<IEnumerable<Publisher>> GetAll();
    }
}
