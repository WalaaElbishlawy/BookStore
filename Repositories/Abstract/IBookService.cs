using BookStore.Models.Domain;

namespace BookStore.Repositories.Abstract
{
    public interface IBookService
    {
        Task<bool> Add(Book model);
        Task<bool> Update(Book model);
        Task<bool> Delete(int id);
        Task<Book> GetById(int id);
        Task<IEnumerable<Book>> GetAll();
    }
}
