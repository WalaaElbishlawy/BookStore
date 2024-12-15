using BookStore.Models.Domain;

namespace BookStore.Repositories.Abstract
{
    public interface IGenreService
    {
        Task<bool> Add(Genre model);
        Task<bool> Update(Genre model);
        Task<bool> Delete(int id);
        Task<Genre> GetById(int id);
        Task<IEnumerable<Genre>> GetAll();

    }
}
