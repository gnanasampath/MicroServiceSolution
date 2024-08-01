﻿using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly AppDbContext _dbContext;
        public PlatformRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public void CreatePlatform(Platform platform)
        {
            if (platform == null)
                throw new ArgumentNullException(nameof(platform));

            _dbContext.Platforms.Add(platform);

        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _dbContext.Platforms.ToList();
        }

        public Platform GetPlatformById(int id) => _dbContext.Platforms.FirstOrDefault(x => x.Id == id);

        public bool SaveChanges()
        {
            return _dbContext.SaveChanges() >= 0;
        }
    }
}
