using BookStore.Models.Domain;

namespace BookStore.Repositories.Abstract
{
    public interface IGenreService
    {
        bool Add(Genre model);
        bool Update(Genre model);
        bool Delete(int id);
        Genre GetById(int id);
        IEnumerable<Genre> GetAll();

    }
}
