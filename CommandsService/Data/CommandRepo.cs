using CommandsService.Models;

namespace CommandsService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _appDbContext;

        public CommandRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void CreateCommand(int platformId, Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            if (PlatformExists(platformId))
            {
                command.PlatformId = platformId;
                _appDbContext.Commands.Add(command);
            }
        }

        public void CreatePlatform(Platform platform)
        {
            if (platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }
            _appDbContext.Platforms.Add(platform);

        }

        public bool ExternalPlatformExists(int externalPlatformId)
        {
            return _appDbContext.Platforms.Any(p => p.ExternalId == externalPlatformId);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _appDbContext.Platforms.ToList();
        }

        public Command GetCommand(int platformId, int commandId)
        {
            return _appDbContext.Commands.Where(c => c.Id == commandId && c.PlatformId == platformId).FirstOrDefault();
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            return _appDbContext.Commands.Where(p => p.PlatformId == platformId)
            .OrderBy(c => c.Platform.Name);
        }

        public bool PlatformExists(int platformId)
        {
            return _appDbContext.Platforms.Any(p => p.Id == platformId);
        }

        public bool SaveChanges()
        {
            return (_appDbContext.SaveChanges() >= 0);
        }
    }

}