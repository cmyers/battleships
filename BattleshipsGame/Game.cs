using System;
using System.Linq;

namespace BattleshipsGame
{
    class Game
    {
        private Player player;
        private Player cpu;
        private static Random random = new Random();

        public Game()
        {
            cpu = new Player("CPU");
            cpu.CreateShip(ShipType.BATTLESHIP);
            cpu.CreateShip(ShipType.DESTROYER);
            cpu.CreateShip(ShipType.DESTROYER);
        }

        public bool StartGame()
        {
            Console.WriteLine("Please enter your name:");
            player = new Player(Console.ReadLine());
            player.CreateShip(ShipType.BATTLESHIP);
            player.CreateShip(ShipType.DESTROYER);
            player.CreateShip(ShipType.DESTROYER);
            GameLoop();
            Console.ReadKey();
            return true;
        }

        private bool GameLoop()
        {        
            bool gameOver = false;
            while(!gameOver)
            {
                Console.Clear();
                int y;
                int x;
                ConsoleDisplay.ShowPlayerGrid(player);
                ConsoleDisplay.ShowPlayerBattleGrid(player);
                Console.WriteLine("Select a Coordinate to Attack: ");
                string coord = Console.ReadLine();
                if(coord.Count() > 2 || !char.IsLetter(coord[0]) || !char.IsDigit(coord[1]) || string.IsNullOrEmpty(coord))
                {
                    Console.WriteLine("Invalid Coord. Example: A1");
                    Console.ReadKey();
                }
                else
                {
                    y = int.Parse(coord[1].ToString().ToUpper());
                    x = ConsoleDisplay.ALPHABET.IndexOf(coord[0].ToString().ToUpper());
                    if (y > 9 || x > 9)
                    {
                        Console.WriteLine("Coord outside boundaries. Please try again.");
                        Console.ReadKey();
                        continue;
                    }
                    bool hit = player.Fire(x, y, cpu.PlayerGrid);
                    if(hit)
                    {
                        Console.WriteLine("HIT!");
                        if(player.BattleGrid.GetGridSquare(y, x).CheckStatus() == GridSquareStatus.SUNK)
                        {
                            Console.WriteLine("SUNK!");
                        }
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("MISS!");
                        Console.ReadKey();
                    }
                    x = random.Next(0, 10);
                    y = random.Next(0, 10);
                    hit = cpu.Fire(x, y, player.PlayerGrid);
                    if (hit)
                    {
                        Console.WriteLine("One of your Ships was HIT!");
                        if (player.PlayerGrid.GetGridSquare(y, x).CheckStatus() == GridSquareStatus.SUNK)
                        {
                            Console.WriteLine("One of your Ships SUNK!");
                        }
                        Console.ReadKey();
                    }

                    if (!player.CheckFleet())
                    {
                        Console.WriteLine("Game Over, " + cpu.Name + " Wins!");
                        gameOver = true;
                    }
                    if (!cpu.CheckFleet())
                    {
                        Console.WriteLine("Game Over, "+ player.Name+" Wins!");
                        gameOver = true;
                    }
                }
            }
            return false;
        }
    }
}
