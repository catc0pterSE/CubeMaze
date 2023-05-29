using System.Collections.Generic;

namespace Model.Cube
{
    public class Cell : CubeElement<Cell>
    {
        private readonly Dictionary<Direction, bool> _walls = new Dictionary<Direction, bool>()
        {
            [Direction.Left] = true,
            [Direction.Right] = true,
            [Direction.Up] = true,
            [Direction.Down] = true
        };

        private readonly List<Direction> _edges = new List<Direction>();

        public bool Visited { get; set; }
        public bool IsStart { get; set; }
        public bool IsEnd { get; set; }

        public IReadOnlyDictionary<Direction, bool> Walls => _walls;
        public IReadOnlyCollection<Direction> Edges => _edges;

        public void AddEdge(Direction direction) =>
            _edges.Add(direction);

        public void DisableWall(Direction direction) =>
            _walls[direction] = false;
    }
}