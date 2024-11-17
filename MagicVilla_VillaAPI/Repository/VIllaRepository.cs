using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository
{
    public class VIllaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _db;

        public VIllaRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }


        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Villas.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }


        //public async Task CreateAsync(Villa entity)
        //{
        //    await _db.Villas.AddAsync(entity);
        //    await SaveAsync();
        //}

        //public async Task<Villa> GetAsync(Expression<Func<Villa, bool>> filter = null, bool tracked = true)
        //{
        //    IQueryable<Villa> query = _db.Villas; //It doesnt get executed right away, IQueryable

        //    if (!tracked)
        //    {
        //        query = query.AsNoTracking();
        //    }
        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }
        //    return await query.FirstOrDefaultAsync(); //At this point, the query will be executed. This is deferred execution. ToList causes immediate execution
        //}

        //public async Task<List<Villa>> GetAllAsync(Expression<Func<Villa, bool>> filter = null)
        //{
        //    IQueryable<Villa> query = _db.Villas; //It doesnt get executed right away, IQueryable

        //    if(filter != null)
        //    {
        //        query = query.Where(filter);
        //    }
        //    return await query.ToListAsync(); //At this point, the query will be executed. This is deferred execution. ToList causes immediate execution
        //}

        //public async Task RemoveAsync(Villa entity)
        //{
        //    _db.Villas.Remove(entity);
        //    await SaveAsync();
        //}

        //public async Task SaveAsync()
        //{
        //    await _db.SaveChangesAsync();
        //}

        //public async Task UpdateAsync(Villa entity)
        //{
        //    _db.Villas.Update(entity);
        //    await SaveAsync();
        //}
    }
}
