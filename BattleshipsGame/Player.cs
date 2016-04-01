using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipsGame
{
    class Player
    {
        public GameGrid PlayerGrid { get; } = new GameGrid();
        public GameGrid BattleGrid { get; } = new GameGrid();
        private List<Ship> ships = new List<Ship>();
        private static Random random = new Random();

        public Player()
        {

        }

        public bool Fire(int x, int y, GameGrid enemyGrid)
        {
            AttackInterface gridSqr = enemyGrid.GetGridSquare(y, x);
            if (gridSqr != null)
            {
                BattleGrid.AddGridSquare(gridSqr);
            }
            else
            {
                gridSqr = new GridSquare(y, x);
                BattleGrid.AddGridSquare(gridSqr);
                enemyGrid.AddGridSquare(gridSqr);
            }
            return gridSqr.Attack();
        }

        public void AddShip(Ship ship)
        {
            ships.Add(ship);
        }

        public void CreateShip(ShipType shipType)
        {
            Ship ship = new Ship(shipType);
            int headX = random.Next(0, 10);
            int headY = random.Next(0, 10);
            int test = random.Next(0, 2);
            Orientation orientation = (Orientation)test;

            if (CheckPlacement(orientation, shipType, headX, headY))
            {
                HullComponent hullComponent;
                for (int i = 0; i < (int)shipType; i++)
                {
                    switch (orientation)
                    {
                        case Orientation.HORIZONAL:
                            hullComponent = new HullComponent(headY, headX++, ship);
                            ship.AddHullComponent(hullComponent);
                            PlayerGrid.AddGridSquare(hullComponent);
                            break;
                        case Orientation.VERTICAL:
                            hullComponent = new HullComponent(headY++, headX, ship);
                            ship.AddHullComponent(hullComponent);
                            PlayerGrid.AddGridSquare(hullComponent);
                            break;
                    }
                }
            }
            else
            {
                CreateShip(shipType);
                return;
            }
            ships.Add(ship);
        }

        private bool CheckPlacement(Orientation orientation, ShipType shipType, int headX, int headY)
        {
            //check boundaries
            switch (orientation)
            {
                case Orientation.HORIZONAL:
                    if (headX + (int)shipType <= 9 && PlayerGrid.GetGridSquare(headY, headX) == null)
                    {
                        break;
                    }
                    return false;
                case Orientation.VERTICAL:
                    if (headY + (int)shipType <= 9 && PlayerGrid.GetGridSquare(headY, headX) == null)
                    {
                        break;
                    }
                    return false;
            }

            //check proximity
            for (int i = 0; i < (int)shipType; i++)
            {
                if (PlayerGrid.GetGridSquare(headY - 1, headX) == null
                    && PlayerGrid.GetGridSquare(headY + 1, headX) == null
                    && PlayerGrid.GetGridSquare(headY, headX - 1) == null
                    && PlayerGrid.GetGridSquare(headY, headX + 1) == null
                    )
                {
                    switch (orientation)
                    {
                        case Orientation.HORIZONAL:
                            headX++;
                            break;
                        case Orientation.VERTICAL:
                            headY++;
                            break;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckFleet()
        { 
            return !ships.All(x => x.Sunk);
        }
    }
}
