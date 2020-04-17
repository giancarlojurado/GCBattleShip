
namespace GCBattleShip.Domain.Classes
{
    public class Game : Base
    {
        public Board Board { get; set; }

        public Game()
        {
            Board = new Board();
        }
    }
}