using System.Linq;
using System.Collections.Generic;
using System;

namespace BattleshipsGame
{

    interface AttackInterface
    {
        bool Attack();
        GridSquareStatus CheckStatus();
    }

    class GameGrid
    {
        private AttackInterface[,] grid = new GridSquare[10, 10];

        public GameGrid()
        {

        }

        public AttackInterface GetGridSquare(int y, int x)
        {
            if (y < 0 || y > 9 || x < 0 || x > 9)
            {
                return null;
            }
            return grid[y, x];
        }

        public void AddGridSquare(AttackInterface gridSquare)
        {
            grid[((GridSquare)gridSquare).Y, ((GridSquare)gridSquare).X] = gridSquare;
        }
    }

    class GridSquare : AttackInterface
    {
        public int X { get; set; }
        public int Y { get; set; }
        protected GridSquareStatus Status = GridSquareStatus.NONE;
        public event EventHandler HitEvent;

        public GridSquare(int headY, int headX)
        {
            X = headX;
            Y = headY;
        }

        public virtual GridSquareStatus CheckStatus()
        {
            return Status;
        }

        protected virtual void Hit()
        {
            Status = GridSquareStatus.HIT;
            OnChanged(EventArgs.Empty);
        }

        protected virtual void OnChanged(EventArgs e)
        {
            if (HitEvent != null)
                HitEvent(this, e);
        }

        protected virtual void Miss()
        {
            Status = GridSquareStatus.MISS;
        }

        public virtual bool Attack()
        {
            if(GetType() == typeof(HullComponent))
            {
                Hit();
                return true;
            }
            else
            {
                Miss();
                return false;
            }
        }
    }

    class HullComponent : GridSquare
    {
        private Ship ship;

        public HullComponent(int headY, int headX, Ship ship) : base (headY, headX)
        {
            this.ship = ship;
        }

        public ShipType GetShipType()
        {
            return ship.ShipType;
        }

        public override GridSquareStatus CheckStatus()
        {
            if (ship.Sunk && Status != GridSquareStatus.SUNK)
            {
                Status = GridSquareStatus.SUNK;
            }
            return Status;
        }

        //could implement armor or levels of damage later
    }

    class Ship
    {
        private List<HullComponent> Hull = new List<HullComponent>();
        public ShipType ShipType { get; private set; }
        public bool Sunk { get; private set; }
        public Guid ShipID { get; private set; } = Guid.NewGuid();

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
        }

        private bool CheckAlive()
        {
            return Hull.Any(x => x.CheckStatus() == GridSquareStatus.NONE);
        }
    }
}
