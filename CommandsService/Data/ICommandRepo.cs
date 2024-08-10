using CommandsService.Models;

namespace CommandsService.Data
{
    public interface ICommandRepo
    {
        #region Platform related

        bool SaveChanges();
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform platform);
        bool PlatformExists(int platformId);

        bool ExternalPlatformExists(int externalPlatformId);

        #endregion

        #region Commands related

        IEnumerable<Command> GetCommandsForPlatform(int platformId);
        Command GetCommand(int platformId, int commandId);
        void CreateCommand(int platformId, Command command);

        #endregion
    }

}