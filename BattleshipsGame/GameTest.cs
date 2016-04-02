using System;
using System.Linq;

namespace BattleshipsGame
{
    class GameTest
    {
        private Player cpuPlayer1 = new Player("CPU Player 1");
        private Player cpuPlayer2 = new Player("CPU Player 2");

        public GameTest()
        {
            cpuPlayer1.CreateShip(ShipType.BATTLESHIP);
            cpuPlayer1.CreateShip(ShipType.DESTROYER);
            cpuPlayer1.CreateShip(ShipType.DESTROYER);

            cpuPlayer2.CreateShip(ShipType.BATTLESHIP);
            cpuPlayer2.CreateShip(ShipType.DESTROYER);
            cpuPlayer2.CreateShip(ShipType.DESTROYER);

            GameTestStart();
        }

        public void GameTestStart()
        {
            //test game!
            ConsoleDisplay.ShowPlayerGrid(cpuPlayer1);
            ConsoleDisplay.ShowPlayerGrid(cpuPlayer2);

            bool gameOver = false;
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    cpuPlayer1.Fire(y, x, cpuPlayer2.PlayerGrid);
                    cpuPlayer2.Fire(y, x, cpuPlayer1.PlayerGrid);
                    if (!cpuPlayer2.CheckFleet())
                    {
                        Console.WriteLine("Game Over, P1 wins!");
                        gameOver = true;
                        break;
                    }
                    if (!cpuPlayer1.CheckFleet())
                    {
                        Console.WriteLine("Game Over, P2 wins!");
                        gameOver = true;
                        break;
                    }
                }
                if (gameOver)
                {
                    break;
                }

            }

            ConsoleDisplay.ShowPlayerBattleGrid(cpuPlayer1);
            ConsoleDisplay.ShowPlayerBattleGrid(cpuPlayer2);

            ConsoleDisplay.ShowPlayerGrid(cpuPlayer1);
            ConsoleDisplay.ShowPlayerGrid(cpuPlayer2);
        }
        
    }
}
