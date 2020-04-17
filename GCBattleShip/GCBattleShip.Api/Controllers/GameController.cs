using System;
using System.Threading.Tasks;
using GCBattleShip.Domain.Classes;
using GCBattleShip.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GCBattleShip.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // GET api/game/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            return Ok(await _gameService.GetGame(id));
        }

        // POST api/game
        [HttpPost]
        public async Task<IActionResult> CreateGame()
        {
            return Ok(await _gameService.CreateGame());
        }

        // POST api/api/{id}/battleship
        [HttpPost("{id}/battleship")]
        public async Task<IActionResult> AddBattleship([FromRoute] string id, [FromBody] BattleshipRequest request)
        {
            try
            {
                await _gameService.AddBattleship(id,request);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // POST api/api/{id}/attack
        [HttpPost("{id}/attack")]
        public async Task<IActionResult> Attack([FromRoute] string id, [FromBody] AttackRequest request)
        {
            try
            {
                var result = await _gameService.Attack(id,request.Location.ToUpper());
                return Ok(result.ToString());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
    }
}