using System;
using System.Linq;

namespace BattleshipsGame
{
    class Game
    {
        public Player p1 { get; private set; } = new Player();
        public Player p2 { get; private set; } = new Player();

        public Game()
        {
            p1.CreateShip(ShipType.BATTLESHIP);
            p1.CreateShip(ShipType.DESTROYER);
            p1.CreateShip(ShipType.DESTROYER);

            p2.CreateShip(ShipType.BATTLESHIP);
            p2.CreateShip(ShipType.DESTROYER);
            p2.CreateShip(ShipType.DESTROYER);

            Console.WriteLine("Player's Grid");
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if(p1.PlayerGrid.GetGridSquare(y, x) != null)
                    {
                        //Console.Write(" " + p1.PlayerGrid.GetGridSquare(y, x).Status.ToString().First()+" ");
                        Console.Write(" " + ((HullComponent)p1.PlayerGrid.GetGridSquare(y, x)).GetShipType().ToString().First() + " ");
                    }
                    else
                    {
                        Console.Write(" - ");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("CPU");
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (p2.PlayerGrid.GetGridSquare(y, x) != null)
                    {
                        //Console.Write(" " + p2.PlayerGrid.GetGridSquare(y, x).Status.ToString().First() + " ");
                        Console.Write(" " + ((HullComponent)p2.PlayerGrid.GetGridSquare(y, x)).GetShipType().ToString().First() + " ");
                    }
                    else
                    {
                        Console.Write(" - ");
                    }
                }
                Console.WriteLine();
            }


            //test game!
            bool gameOver = false;
            for (int y = 0; y < 10; y++)
            {
                for(int x = 0; x < 10; x++)
                {
                    p1.Fire(y, x, p2.PlayerGrid);
                    p2.Fire(y, x, p1.PlayerGrid);
                    if (!p2.CheckFleet())
                    {
                        Console.WriteLine("Game Over, P1 wins!");
                        gameOver = true;
                        break;
                    }
                    if (!p1.CheckFleet())
                    {
                        Console.WriteLine("Game Over, P2 wins!");
                        gameOver = true;
                        break;
                    }
                }
                if(gameOver)
                {
                    break;
                }

            }

            Console.WriteLine("Player BattleGrid");
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (p1.BattleGrid.GetGridSquare(y, x) != null)
                    {
                        Console.Write(" " + p1.BattleGrid.GetGridSquare(y, x).Status.ToString().First() + " ");
                    }
                    else
                    {
                        Console.Write(" - ");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("CPU BattleGrid");
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (p2.BattleGrid.GetGridSquare(y, x) != null)
                    {
                        Console.Write(" " + p2.BattleGrid.GetGridSquare(y, x).Status.ToString().First() + " ");
                    }
                    else
                    {
                        Console.Write(" - ");
                    }
                }
                Console.WriteLine();
            }
        }
        
    }
}
