using PlatformService.Dtos;

namespace PlatformService.SynDataServices.Http
{
public interface ICommandDataClient
{
        Task SendPlatformToCommand(PlatformReadDto plat);
}
}