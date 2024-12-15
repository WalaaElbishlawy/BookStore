using BookStore.Models.Domain;
using BookStore.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Implementation
{
    public class AuthorService : IAuthorService
    {
        private readonly DatabaseContext context;
        public AuthorService(DatabaseContext context)
        {
            this.context = context;
        }
        public async Task<bool> Add(Author model)
        {
            try
            {
                await context.Author.AddAsync(model);
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
                context.Author.Remove(data);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            return await context.Author.ToListAsync();
        }

        public async Task<Author> GetById(int id)
        {
            return await context.Author.FindAsync(id);
        }

        public async Task<bool> Update(Author model)
        {
            try
            {
                context.Author.Update(model);
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
