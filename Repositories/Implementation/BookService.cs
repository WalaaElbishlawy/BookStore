using BookStore.Models.Domain;
using BookStore.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Implementation
{
    public class BookService : IBookService
    {
        private readonly DatabaseContext context;
        public BookService(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<bool> Add(Book model)
        {
            try
            {
                await context.Book.AddAsync(model);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var data = await this.GetById(id);
                if (data == null) 
                    return false;
                context.Book.Remove(data);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            var data = await (from book in context.Book
                             join author in context.Author
                             on book.AuthorId equals author.Id
                             join publisher in context.Publisher
                             on book.PublisherId equals publisher.Id
                             join genre in context.Genre
                             on book.GenreId equals genre.Id
                             select new Book
                             {
                                Id = book.Id,
                                AuthorId = author.Id,
                                PublisherId = publisher.Id,
                                Isbn = book.Isbn,
                                Title = book.Title,
                                TotalPages = book.TotalPages,
                                GenreName = genre.Name,
                                AuthorName = author.AuthorName,
                                PublisherName = publisher.PublisherName
                             }
                             ).ToListAsync();
             return data;
        }

        public async Task<Book> GetById(int id)
        {
            return await context.Book.FindAsync(id);
        }

        public async Task<bool> Update(Book model)
        {
            try
            {
                context.Book.Update(model);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
