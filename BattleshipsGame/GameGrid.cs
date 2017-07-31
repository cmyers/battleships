using System.Linq;
using System.Collections.Generic;
using System;

namespace BattleshipsGame
{

    interface IAttack
    {
        bool Attack();
        GridSquareStatus CheckStatus();
    }

    class GameGrid
    {
        private IAttack[,] grid = new GridSquare[10, 10];

        public GameGrid()
        {

        }

        public IAttack GetGridSquare(int y, int x)
        {
            if (y < 0 || y > 9 || x < 0 || x > 9)
            {
                return null;
            }
            return grid[y, x];
        }

        public void AddGridSquare(IAttack gridSquare)
        {
            grid[((GridSquare)gridSquare).Y, ((GridSquare)gridSquare).X] = gridSquare;
        }
    }

    class GridSquare : IAttack
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
            HitEvent?.Invoke(this, e);
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

    
}
