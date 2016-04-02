using System;

namespace BattleshipsGame
{
    class MainGame
    {
        static void Main(string[] args)
        {
            //GameTest game = new GameTest();
            Game game = new Game();
            game.StartGame();
            Console.ReadKey();
        }
    }
}
