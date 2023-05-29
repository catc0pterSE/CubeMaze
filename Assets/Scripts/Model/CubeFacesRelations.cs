using System.Collections.Generic;

namespace Model
{
    public class CubeFacesRelations
    {
        private readonly Dictionary<CubeFaceType, Dictionary<Direction, CubeFaceType>> _directedNeighbors =
            new Dictionary<CubeFaceType, Dictionary<Direction, CubeFaceType>>()
            {
                [CubeFaceType.Front] = new Dictionary<Direction, CubeFaceType>()
                {
                    [Direction.Left] = CubeFaceType.Left,
                    [Direction.Right] = CubeFaceType.Right,
                    [Direction.Up] = CubeFaceType.Top,
                    [Direction.Down] = CubeFaceType.Bottom
                },
                [CubeFaceType.Left] = new Dictionary<Direction, CubeFaceType>()
                {
                    [Direction.Left] = CubeFaceType.Back,
                    [Direction.Right] = CubeFaceType.Front,
                    [Direction.Up] = CubeFaceType.Top,
                    [Direction.Down] = CubeFaceType.Bottom
                },
                [CubeFaceType.Right] = new Dictionary<Direction, CubeFaceType>()
                {
                    [Direction.Left] = CubeFaceType.Front,
                    [Direction.Right] = CubeFaceType.Back,
                    [Direction.Up] = CubeFaceType.Top,
                    [Direction.Down] = CubeFaceType.Bottom
                },
                [CubeFaceType.Back] = new Dictionary<Direction, CubeFaceType>()
                {
                    [Direction.Left] = CubeFaceType.Right,
                    [Direction.Right] = CubeFaceType.Left,
                    [Direction.Up] = CubeFaceType.Top,
                    [Direction.Down] = CubeFaceType.Bottom
                },
                [CubeFaceType.Top] = new Dictionary<Direction, CubeFaceType>()
                {
                    [Direction.Left] = CubeFaceType.Left,
                    [Direction.Right] = CubeFaceType.Right,
                    [Direction.Up] = CubeFaceType.Back,
                    [Direction.Down] = CubeFaceType.Front
                },
                [CubeFaceType.Bottom] = new Dictionary<Direction, CubeFaceType>()
                {
                    [Direction.Left] = CubeFaceType.Left,
                    [Direction.Right] = CubeFaceType.Right,
                    [Direction.Up] = CubeFaceType.Front,
                    [Direction.Down] = CubeFaceType.Back
                },
            };

        private readonly Dictionary<CubeFaceType, CubeFaceType[]> _nonConsistentFaceTypes =
            new Dictionary<CubeFaceType, CubeFaceType[]>()
            {
                [CubeFaceType.Front] = new CubeFaceType[]{ },
                [CubeFaceType.Left] = new CubeFaceType[] { CubeFaceType.Bottom },
                [CubeFaceType.Right] = new CubeFaceType[] { CubeFaceType.Top },
                [CubeFaceType.Back] = new CubeFaceType[] { CubeFaceType.Bottom, CubeFaceType.Top },
                [CubeFaceType.Bottom] = new CubeFaceType[] { CubeFaceType.Left, CubeFaceType.Back },
                [CubeFaceType.Top] = new CubeFaceType[] { CubeFaceType.Right, CubeFaceType.Back }
            };

        public IReadOnlyDictionary<CubeFaceType, Dictionary<Direction, CubeFaceType>> DirectedNeighbors =>
            _directedNeighbors;

        public IReadOnlyDictionary<CubeFaceType, CubeFaceType[]> NonConsistentFaceTypes => _nonConsistentFaceTypes;
    }
}