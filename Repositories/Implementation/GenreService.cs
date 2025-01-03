﻿using BookStore.Models.Domain;
using BookStore.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Implementation
{
    public class GenreService : IGenreService
    {
        private readonly DatabaseContext context;
        public GenreService(DatabaseContext context)
        {
            this.context = context;
        }
        public async Task<bool> Add(Genre model)
        {
            try
            {
                await context.Genre.AddAsync(model);
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
                context.Genre.Remove(data);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await context.Genre.ToListAsync();
        }

        public async Task<Genre> GetById(int id)
        {
            return await context.Genre.FindAsync(id);
        }

        public async Task<bool> Update(Genre model)
        {
            try
            {
                context.Genre.Update(model);
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
