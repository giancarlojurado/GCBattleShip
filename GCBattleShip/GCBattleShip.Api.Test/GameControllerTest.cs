using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using GCBattleShip.Api.Controllers;
using GCBattleShip.Domain.Classes;
using GCBattleShip.Domain.Interfaces.Services;
using NSubstitute;
using Xunit;

namespace GCBattleShip.Api.Test
{
    public class GameControllerTest
    {
        private readonly Fixture _fixture;

        private readonly GameController _gameController;
        private readonly IGameService _gameService;
        
        public GameControllerTest()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoNSubstituteCustomization());

            _gameService = _fixture.Freeze<IGameService>();
            
            
            _gameController = new GameController(_gameService);
        }
        
        [Fact]
        public async Task CreateGame_CallsGameService()
        {
            // Act
            await _gameController.CreateGame();

            // Assert
            await _gameService.Received(1).CreateGame();
        }
        
        [Fact]
        public async Task GetGame_CallsGameService()
        {
            // Arrange
            var gameId = _fixture.Create<string>();

            // Act
            await _gameController.Get(gameId);

            // Assert
            await _gameService.Received(1).GetGame(gameId);
        }
        
        [Fact]
        public async Task AddBattleship_CallsGameService()
        {
            // Arrange
            var gameId = _fixture.Create<string>();
            var request = _fixture.Create<BattleshipRequest>();

            // Act
            await _gameController.AddBattleship(gameId,request);

            // Assert
            await _gameService.Received(1).AddBattleship(gameId, request);
        }
        
        [Fact]
        public async Task Attack_CallsGameService()
        {
            // Arrange
            var gameId = _fixture.Create<string>();
            var request = _fixture.Create<AttackRequest>();

            // Act
            await _gameController.Attack(gameId,request);

            // Assert
            await _gameService.Received(1).Attack(gameId, request.Location.ToUpper());
        }
    }
}