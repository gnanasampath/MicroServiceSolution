using CommandsService.Models;

namespace CommandsService.Dtos
{
    public class CommandCreateDto
    {        
        public required string HowTo { get; set; }
        public required string CommandLine { get; set; }
        public int PlatformId { get; set; }
    }
}