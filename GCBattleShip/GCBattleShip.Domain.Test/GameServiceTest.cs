using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using GCBattleShip.Domain.Classes;
using GCBattleShip.Domain.Enums;
using GCBattleShip.Domain.Interfaces.Repositories;
using GCBattleShip.Domain.Services;
using NSubstitute;
using Xunit;

namespace GCBattleShip.Domain.Test
{
    public class GameServiceTest
    {
        private readonly Fixture _fixture;
        private readonly IGameRepository _gameRepository;
        private readonly GameService _gameService;

        public GameServiceTest()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoNSubstituteCustomization());

            _gameRepository = _fixture.Freeze<IGameRepository>();
            _gameService = _fixture.Create<GameService>();
        }

        [Fact]
        public async Task CreateGame_Should_Create_Game()
        {
            // arrange
            var newGameId = _fixture.Create<string>();
            _gameRepository.CreateGame().Returns(newGameId);

            // Act
            var result = await _gameService.CreateGame();

            // Assert
            result.Should().Be(newGameId);
        }

        [Fact]
        public async Task GetGame_Should_Get_Game()
        {
            // arrange
            var game = _fixture.Create<Game>();

            //var newGameId = _fixture.Create<string>();
            _gameRepository.GetGame(game.Id).Returns(game);

            // Act
            var result = await _gameService.GetGame(game.Id);

            // Assert
            result.Should().Be(game);
        }
        
        [Theory]
        [InlineData(null)]
        [InlineDataAttribute("")]
        public void GetGame_ShouldThrowException(string gameId)
        {
            // Act
            Func<Task> act = async () => await _gameService.GetGame(gameId);

            // Assert
            act.Should().Throw<Exception>();
        }
        
        [Fact]
        public async Task AddBattleShip_Should_Update_Game()
        {
            // Arrange
            var battleshipRequest = _fixture.Create<BattleshipRequest>();
            battleshipRequest.Size = 4;
            battleshipRequest.Location = "A2";
            var game = _fixture.Create<Game>();

            _gameRepository.GetGame(game.Id).Returns(game);

            // Act
            await _gameService.AddBattleship(game.Id, battleshipRequest);

            // Assert
            await _gameRepository.Received(1).SaveGame(game);
        }
        
        [Fact]
        public void AddBattleShip_Should_ThrowError()
        {
            // Arrange
            var battleshipRequest = _fixture.Create<BattleshipRequest>();
            var gameId = _fixture.Create<string>();

            // Act
            Func<Task> act = async () => await _gameService.AddBattleship(gameId, battleshipRequest);

            // Assert
            act.Should().Throw<Exception>();
        }
        
        [Fact]
        public async Task Attack_Should_Update_Game()
        {
            // Arrange
            var attackRequest = _fixture.Create<AttackRequest>();
            attackRequest.Location = "A5";
            var game = _fixture.Create<Game>();

            _gameRepository.GetGame(game.Id).Returns(game);

            // Act
            await _gameService.Attack(game.Id, attackRequest.Location);

            // Assert
            await _gameRepository.Received(1).SaveGame(game);
        }
        
        [Fact]
        public void Attack_Should_ThrowError()
        {
            // Arrange
            var attackRequest = _fixture.Create<AttackRequest>();
            var gameId = _fixture.Create<string>();

            // Act
            Func<Task> act = async () => await _gameService.Attack(gameId, attackRequest.Location);

            // Assert
            act.Should().Throw<Exception>();
        }
        
        [Fact]
        public async Task Attack_Should_Hit()
        {
            // Arrange
            var attackRequest = _fixture.Create<AttackRequest>();
            attackRequest.Location = "A2";
            var game = _fixture.Create<Game>();
            game.Board.Ships[0].Location = new List<Location>
            {
                new Location
                {
                    GridLocation = "A2"
                }
            };

            _gameRepository.GetGame(game.Id).Returns(game);

            // Act
            var result = await _gameService.Attack(game.Id, attackRequest.Location);

            // Assert
           result.Should().Be(AttackResult.Hit);
        }
        
        [Fact]
        public async Task Attack_Should_Miss()
        {
            // Arrange
            var attackRequest = _fixture.Create<AttackRequest>();
            attackRequest.Location = "B6";
            var game = _fixture.Create<Game>();
            game.Board.Ships[0].Location = new List<Location>
            {
                new Location
                {
                    GridLocation = "A2"
                }
            };

            _gameRepository.GetGame(game.Id).Returns(game);

            // Act
            var result = await _gameService.Attack(game.Id, attackRequest.Location);

            // Assert
            result.Should().Be(AttackResult.Miss);
        }
    }
}