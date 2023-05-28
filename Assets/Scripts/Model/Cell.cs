using System.Collections.Generic;
using UnityEngine;
using View;

namespace Model
{
    public class Cell : CubeElement<Cell>
    {
        private Dictionary<Direction, bool> _walls = new Dictionary<Direction, bool>()
        {
            [Direction.Left] = true,
            [Direction.Right] = true,
            [Direction.Up] = true,
            [Direction.Down] = true
        };
        
        public bool Visited { get; set; }

        public IReadOnlyDictionary<Direction, bool> Walls => _walls;

        public void DisableWall(Direction direction)
        {
            _walls[direction] = false;
        }
    }
}