using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonDataAccess.Context;
using FarmerzonDataAccess.Interfaces;
using FarmerzonDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonDataAccess.Implementations
{
    public class ArticleRepository : IArticleRepository
    {
        public async Task<IList<Article>> GetEntities(int? id, string name, string description, double? price, 
            int? amount, double? size, DateTime? createdAt, DateTime? updatedAt, FarmerzonContext context)
        {
            return await context.Articles
                .Include(a => a.Person)
                .Include(a => a.Unit)
                .Where(a => id == null || a.ArticleId == id)
                .Where(a => name == null || a.Name == name)
                .Where(a => description == null || a.Description == description)
                .Where(a => price == null || a.Price == price)
                .Where(a => size == null || a.Size == size)
                .Where(a => createdAt == null || a.CreatedAt == createdAt)
                .Where(a => updatedAt == null || a.UpdatedAt == updatedAt)
                .ToListAsync();
        }
    }
}