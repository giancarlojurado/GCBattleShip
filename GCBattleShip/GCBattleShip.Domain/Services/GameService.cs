using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GCBattleShip.Domain.Classes;
using GCBattleShip.Domain.Enums;
using GCBattleShip.Domain.Interfaces.Repositories;
using GCBattleShip.Domain.Interfaces.Services;

namespace GCBattleShip.Domain.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<string> CreateGame()
        {
            return await _gameRepository.CreateGame();
        }

        public async Task<Game> GetGame(string id)
        {
            var game = await _gameRepository.GetGame(id);
            if (game == null)
            {
                throw new Exception("Invalid game ID");
            }

            return game;
        }

        public async Task AddBattleship(string gameId, BattleshipRequest request)
        {
            // get all the grid locations involved for this ship
            var locations = GetGridLocations(request.Size, request.Location, request.Direction);
            var game = await GetGame(gameId);
            
            // Verify that the ship does not overlap with any of the existing ships
            foreach (var ship in game.Board.Ships)
            {
                var shipLocations = ship.Location.Select(l => l.GridLocation);
                if (shipLocations.Intersect(locations).Any())
                {
                    throw new Exception("Battleship can not overlap with existing ships");
                }
            }
            
            // create the battleship and add it to the board
            var newShip = new Ship(locations);
            game.Board.Ships.Add(newShip);

            await _gameRepository.SaveGame(game);
        }

        public async Task<AttackResult> Attack(string gameId, string location)
        {
            var game = await GetGame(gameId);
            
            // check if that attack was done already
            var previousAttacks = game.Board.Attacks.Select(a => a.GridLocation);
            if (previousAttacks.Contains(location))
            {
                throw new Exception("You have already done this attack, try a different location");
            }
            
            // now check if the attack hits any ship
            foreach (var ship in game.Board.Ships)
            {
                var shipLocation = ship.Location.FirstOrDefault(l => l.GridLocation == location);
                if (shipLocation != null)
                {
                    shipLocation.Hit = true;
                    game.Board.Attacks.Add(RecordAttack(location,shipLocation.Hit));
                    await _gameRepository.SaveGame(game);
                    return AttackResult.Hit;
                }
            }
            game.Board.Attacks.Add(RecordAttack(location,false));
            await _gameRepository.SaveGame(game);
            return AttackResult.Miss;
        }

        private Location RecordAttack(string location, bool hit)
        {
            var attackRecord = new Location
            {
                GridLocation = location,
                Hit = hit
            };

            return attackRecord;
        }

        private List<string> GetGridLocations(int size, string initialLocation, Direction direction)
        {
            // add the initial location to the list
            var gridLocations = new List<string>
            {
                initialLocation.ToUpper()
            };


            var locationLetter = initialLocation[0].ToString().ToUpper();
            var locationNumber = Convert.ToInt32(initialLocation.Substring(1, initialLocation.Length - 1));

            // Verify that none of the grid locations will be outside of the board
            var locationLetterNumber = Common.LetterLocationTranslator[locationLetter];
            if ((direction == Direction.Horizontal && locationLetterNumber + size - 1 > 10) ||
                (direction == Direction.Vertical && locationNumber + size - 1 > 10))
            {
                throw new Exception("Battleship must be placed inside the board");
            }


            var newGridLocation = string.Empty;
            for (var i = 2; i <= size; i++)
            {
                switch (direction)
                {
                    case Direction.Horizontal:
                    {
                        locationLetterNumber++;
                        var newLetter =
                            Common.LetterLocationTranslator.First(l => l.Value == locationLetterNumber).Key;
                        newGridLocation = $"{newLetter}{locationNumber}";
                        break;
                    }
                    case Direction.Vertical:
                        locationNumber++;
                        newGridLocation = $"{locationLetter}{locationNumber}";
                        break;
                }
                gridLocations.Add(newGridLocation);
            }
            return gridLocations;
        }
    }
}