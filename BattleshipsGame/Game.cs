using System;
using System.Linq;

namespace BattleshipsGame
{
    class Game
    {
        public Player p1 { get; private set; } = new Player();
        public Player cpu { get; private set; } = new Player();

        public Game()
        {
            p1.CreateShip(ShipType.BATTLESHIP);
            p1.CreateShip(ShipType.DESTROYER);
            p1.CreateShip(ShipType.DESTROYER);

            cpu.CreateShip(ShipType.BATTLESHIP);
            cpu.CreateShip(ShipType.DESTROYER);
            cpu.CreateShip(ShipType.DESTROYER);

            Console.WriteLine("Player's Grid");
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (p1.PlayerGrid.GetGridSquare(y, x) != null)
                    {
                        HullComponent hc = (HullComponent)p1.PlayerGrid.GetGridSquare(y, x);
                        if (hc.Status == GridSquareStatus.NONE)
                        {
                            Console.Write(" " + hc.GetShipType().ToString().First() + " ");
                        }
                        else
                        {
                            Console.Write(" " + hc.Status.ToString().First() + " ");
                        }


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
                    if (cpu.PlayerGrid.GetGridSquare(y, x) != null)
                    {
                        HullComponent hc = (HullComponent)cpu.PlayerGrid.GetGridSquare(y, x);
                        if (hc.Status == GridSquareStatus.NONE)
                        {
                            Console.Write(" " + hc.GetShipType().ToString().First() + " ");
                        }
                        else
                        {
                            Console.Write(" " + hc.Status.ToString().First() + " ");
                        }
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
                    p1.Fire(y, x, cpu.PlayerGrid);
                    cpu.Fire(y, x, p1.PlayerGrid);
                    if (!cpu.CheckFleet())
                    {
                        Console.WriteLine("Game Over, P1 wins!");
                        gameOver = true;
                        break;
                    }
                    if (!p1.CheckFleet())
                    {
                        Console.WriteLine("Game Over, CPU wins!");
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
                    if (cpu.BattleGrid.GetGridSquare(y, x) != null)
                    {
                        Console.Write(" " + cpu.BattleGrid.GetGridSquare(y, x).Status.ToString().First() + " ");
                    }
                    else
                    {
                        Console.Write(" - ");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("Player's Grid");
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (p1.PlayerGrid.GetGridSquare(y, x) != null)
                    {
                        HullComponent hc = (HullComponent)p1.PlayerGrid.GetGridSquare(y, x);
                        if (hc.Status == GridSquareStatus.NONE)
                        {
                            Console.Write(" " + hc.GetShipType().ToString().First() + " ");
                        }
                        else
                        {
                            Console.Write(" " + hc.Status.ToString().First() + " ");
                        }
                        

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
                    if (cpu.PlayerGrid.GetGridSquare(y, x) != null)
                    {
                        HullComponent hc = (HullComponent)cpu.PlayerGrid.GetGridSquare(y, x);
                        if (hc.Status == GridSquareStatus.NONE)
                        {
                            Console.Write(" " + hc.GetShipType().ToString().First() + " ");
                        }
                        else
                        {
                            Console.Write(" " + hc.Status.ToString().First() + " ");
                        }
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
