using BookStore.Models.Domain;
using BookStore.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Implementation
{
    public class PublisherService : IPublisherService
    {
        private readonly DatabaseContext context;
        public PublisherService(DatabaseContext context)
        {
            this.context = context;
        }
        public async Task<bool> Add(Publisher model)
        {
            try
            {
                await context.Publisher.AddAsync(model);
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
                context.Publisher.Remove(data);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Publisher>> GetAll()
        {
            return await context.Publisher.ToListAsync();
        }

        public async Task<Publisher> GetById(int id)
        {
            return await context.Publisher.FindAsync(id);
        }

        public async Task<bool> Update(Publisher model)
        {
            try
            {
                context.Publisher.Update(model);
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
