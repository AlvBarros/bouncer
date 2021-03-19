using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bouncer.Models;
using Microsoft.EntityFrameworkCore;

namespace bouncer.Repositories
{
    public class PlatformRepository : IRepository<Platform>
    {
        private readonly BouncerContext _context;
        public PlatformRepository(BouncerContext context) : base(context)
        {
            _context = context;
        }


        public async new Task<Platform> Get(int id)
        {
            var platform = await _context.Set<Platform>()
                .SingleAsync(p => p.Id == id);
            _context.Entry(platform).Reference(p => p.Owner).Load();
            return platform;
        }

        public async Task<Platform[]> GetByOwnerId(int id)
        {
            Platform[] platforms = await _context.Platforms
                .Where(p => p.OwnerId == id)
                .ToArrayAsync();
            return platforms;
        }
    }
}