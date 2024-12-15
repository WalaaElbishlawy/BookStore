using BookStore.Models.Domain;

namespace BookStore.Repositories.Abstract
{
    public interface IAuthorService
    {
        Task<bool> Add(Author model);
        Task<bool> Update(Author model);
        Task<bool> Delete(int id);
        Task<Author> GetById(int id);
        Task<IEnumerable<Author>> GetAll();
    }
}
