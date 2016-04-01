using System.Linq;
using System.Collections.Generic;
using System;

namespace BattleshipsGame
{

    class GridSquare
    {
        public int X { get; set; }
        public int Y { get; set; }
        public GridSquareStatus Status { get; set; } = GridSquareStatus.NONE;
        public event EventHandler HitEvent;

        public GridSquare(int headY, int headX)
        {
            X = headX;
            Y = headY;
        }

        public virtual void Hit()
        {
            Status = GridSquareStatus.HIT;
            OnChanged(EventArgs.Empty);
        }

        protected virtual void OnChanged(EventArgs e)
        {
            if (HitEvent != null)
                HitEvent(this, e);
        }

        public virtual void Miss()
        {
            Status = GridSquareStatus.MISS;
        }
    }

    class HullComponent : GridSquare
    {
        private Ship ship;

        public HullComponent(int headY, int headX, Ship ship) : base (headY, headX)
        {
            this.ship = ship;
        }
        
        public bool CheckSunk()
        {
            return ship.Sunk;
        } 

        public ShipType GetShipType()
        {
            return ship.ShipType;
        }

        //could implement armor or levels of damage later
    }

    class Ship
    {
        private List<HullComponent> Hull = new List<HullComponent>();
        public ShipType ShipType { get; private set; }
        public bool Sunk { get; private set; }

        public Ship(ShipType shipType)
        {
            ShipType = shipType;
        }

        public void AddHullComponent(HullComponent hullComponent)
        {
            hullComponent.HitEvent += HullComponent_HitEvent;
            Hull.Add(hullComponent);
        }

        private void HullComponent_HitEvent(object sender, EventArgs e)
        {
            Sunk = !CheckAlive();
            if(Sunk)
            {
                foreach(HullComponent h in Hull)
                {
                    h.Status = GridSquareStatus.SUNK;
                }
            }
        }

        private bool CheckAlive()
        {
            return Hull.Any(x => x.Status == GridSquareStatus.NONE);
        }
    }

    class GameGrid
    {
        private GridSquare[,] grid = new GridSquare[10, 10];

        public GameGrid()
        {

        }

        public GridSquare GetGridSquare(int y, int x)
        {
            if(y < 0 || y > 9 || x < 0 || x > 9)
            {
                return null;
            }
            return grid[y, x];
        }

        public void AddGridSquare(GridSquare gridSquare)
        {
            grid[gridSquare.Y, gridSquare.X] = gridSquare;
        }   
    }
}
