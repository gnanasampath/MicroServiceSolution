using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _commandRepo;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo commandRepo, IMapper mapper)
        {
            _commandRepo = commandRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            Console.WriteLine("Get all Commands for a platform - CommandService - CommandsController");
            if (_commandRepo.PlatformExists(platformId))
            {
                var commands = _commandRepo.GetCommandsForPlatform(platformId);
                return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
            }
            else
                return NotFound();
        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int commandId, int platformId)
        {
            Console.WriteLine($"Get a Command for a platform - CommandService - CommandsController : Platform Id {0}, Command Id :{1}", platformId, commandId);
            if (_commandRepo.PlatformExists(platformId))
            {
                var command = _commandRepo.GetCommand(platformId, commandId);
                if (command == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<CommandReadDto>(command));
            }
            else
                return NotFound();
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandCreateDto)
        {
            Console.WriteLine($"Create a Command for a platform - CommandService - CommandsController : Platform Id {platformId}");
            if (!_commandRepo.PlatformExists(platformId))
            {
                return NotFound();
            }

            var command = _mapper.Map<Command>(commandCreateDto);
            if (command == null)
            {
                return NoContent();
            }

            _commandRepo.CreateCommand(platformId, command);
            _commandRepo.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(command);

            return CreatedAtRoute(nameof(GetCommandForPlatform), new { platformId = platformId, commandId = commandReadDto.Id }, commandReadDto);

        }
    }

}