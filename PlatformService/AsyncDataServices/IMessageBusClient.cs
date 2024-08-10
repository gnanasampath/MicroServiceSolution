using PlatformService.Dtos;

namespace PlatformService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublisheNewPlatform(PlatformPublishedDto platformPublishedDto);
    }
}