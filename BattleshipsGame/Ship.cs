using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipsGame
{

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

    class HullComponent : GridSquare
    {
        private Ship ship;

        public HullComponent(int headY, int headX, Ship ship) : base(headY, headX)
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
}
