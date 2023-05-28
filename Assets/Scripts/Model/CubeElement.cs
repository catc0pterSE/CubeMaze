using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public abstract class CubeElement<T>
    {
        private readonly Dictionary<Direction, T> _neighbors = new Dictionary<Direction, T>();

        public IReadOnlyDictionary<Direction, T> Neighbors => _neighbors;
        
        public Dictionary<T, Direction> NeighborDirections =>
          _neighbors.ToDictionary(pair => pair.Value, pair => pair.Key);

        public void SetNeighbor(Direction direction, T neighbor) =>
            _neighbors[direction] = neighbor;
    }
}