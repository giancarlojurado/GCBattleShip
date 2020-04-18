using System;
using System.Net;
using System.Threading.Tasks;
using GCBattleShip.Domain.Classes;
using GCBattleShip.Domain.Enums;
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
        /// <summary>
        /// Gets the <see cref="Game"/> given an <paramref name="id"/>
        /// </summary>
        /// <param name="id">Game Id</param>
        /// <returns></returns>
        /// <returns>Game info</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Game), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            try
            {
                return Ok(await _gameService.GetGame(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/game
        /// <summary>
        /// Creates a new game and returns its <paramref name="id"/>
        /// </summary>
        /// <returns></returns>
        /// <returns>Game Id</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateGame()
        {
            return Ok(await _gameService.CreateGame());
        }

        // POST api/api/{id}/battleship
        /// <summary>
        /// Places a battleship in the board, note that location must be beween "A1" and "J10"
        /// </summary>
        /// <param name="id">Game Id</param>
        /// <param name="request">Request containing the info where to place the ship </param>
        /// <returns></returns>
        /// <returns>Ok if placed correctly</returns>
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
        /// <summary>
        /// Sends an attack given a location <see cref="AttackRequest"/>
        /// </summary>
        /// <param name="id">Game Id</param>
        /// <param name="request">Attack request containing the location which must be between "A1" and "J10"</param>
        /// <returns></returns>
        /// <returns>Hit or Miss</returns>
        [HttpPost("{id}/attack")]
        [ProducesResponseType(typeof(AttackResult), (int)HttpStatusCode.OK)]
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