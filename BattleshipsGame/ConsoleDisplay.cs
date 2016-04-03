using System;
using System.Linq;

namespace BattleshipsGame
{
    static class ConsoleDisplay
    {
        public const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static void ShowPlayerGrid(Player player)
        {
            Console.WriteLine(player.Name + "'s Grid");
            for (int y = -1; y < 10; y++)
            {
                for (int x = -1; x < 10; x++)
                {
                    if (y >= 0)
                    {
                        if (x >= 0)
                        {
                            IAttack gridRef = player.PlayerGrid.GetGridSquare(y, x);
                            if (gridRef != null)
                            {
                                if (gridRef.CheckStatus() == GridSquareStatus.NONE)
                                {
                                    Console.Write(" " + ((HullComponent)gridRef).GetShipType().ToString().First() + " ");
                                }
                                else
                                {
                                    Console.Write(" " + gridRef.CheckStatus().ToString().First() + " ");
                                }
                            }
                            else
                            {
                                Console.Write(" - ");
                            }
                        }
                        else
                        {
                             Console.Write(" " +y+" ");
                        }
                    }
                    else
                    {
                        Console.Write(x >= 0 ? (" " + ALPHABET[x > 0 ? x : 0] + " ") : " - ");
                    }
                }
                Console.WriteLine();
            }
        }

        public static void ShowPlayerBattleGrid(Player player)
        {
            Console.WriteLine(player.Name + "'s BattleGrid");
            for (int y = -1; y < 10; y++)
            {
                for (int x = -1; x < 10; x++)
                {
                    if (y >= 0)
                    {
                        if (x >= 0)
                        {
                            if (player.BattleGrid.GetGridSquare(y, x) != null)
                            {
                                Console.Write(" " + player.BattleGrid.GetGridSquare(y, x).CheckStatus().ToString().First() + " ");
                            }
                            else
                            {
                                Console.Write(" - ");
                            }
                        }
                        else
                        {
                            Console.Write(" " + y + " ");
                        }
                    }
                    else
                    {
                        Console.Write(x >= 0 ? (" " + ALPHABET[x > 0 ? x : 0] + " ") : " - ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
