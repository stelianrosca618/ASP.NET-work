using Microsoft.EntityFrameworkCore;
using MyRazorPagesApp.Data;
using MyRazorPagesApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRazorPagesApp.Dtos
{
    public interface IViewsDto
    {
        Task<int> CreateView(View view);
        Task<int> UpdateView(View update);
        Task<List<View>> GetView();
        Task<View> GetView(int id);
        Task<bool> DeleteView(int id);
    }

    public class ViewDto : IViewsDto
    {
        protected readonly ApplicationDbContext _context;

        public ViewDto(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<int> CreateView(View view)
        {
            try
            {
                if (view != null)
                {
                    var entity = _context.Views.Add(
                        new View
                        {
                            Id = view.Id,
                            PostId = view.PostId,
                            ViewCount = view.ViewCount
                        });
                    await _context.SaveChangesAsync();

                    return entity.Entity.Id;
                }
            }
            catch { }

            return 0;
        }

        public async Task<int> UpdateView(View update)
        {
            try
            {
                var existing = await _context.Views.FirstOrDefaultAsync(x => x.Id == update.Id);
                if (existing != null)
                {
                    existing.PostId = update.PostId;
                    existing.ViewCount = update.ViewCount;
                    await _context.SaveChangesAsync();

                    return existing.Id;
                }
            }
            catch (Exception ex) { }

            return 0;
        }

        public async Task<List<View>> GetView()
        {
            try
            {
                return await _context.Views.ToListAsync();
            }
            catch { }

            return null;
        }

        public async Task<View> GetView(int id)
        {
            try
            {
                return await _context.Views.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch { }

            return null;
        }

        public async Task<bool> DeleteView(int id)
        {
            try
            {
                var viewToDel = await _context.Views.SingleOrDefaultAsync(x => x.Id == id);
                if (viewToDel != null)
                {
                    _context.Views.Remove(viewToDel);
                    await _context.SaveChangesAsync();

                    return true;
                }
            }
            catch { }

            return false;
        }
    }
}