using System;
using System.Collections.Generic;

namespace Model.Cube
{
    public class Cube
    {
        private const int CubeFacesCount = 6;

        private readonly Dictionary<CubeFaceType, Face> _typedFaces = new Dictionary<CubeFaceType, Face>();

        public Cube(int faceSize)
        {
            CreateFaces(faceSize);
            LinkFaces();
            LinkCells();
        }

        public IReadOnlyDictionary<CubeFaceType, Face> TypedFaces => _typedFaces;

        private void LinkCells()
        {
            foreach (CubeFaceType faceType in Enum.GetValues(typeof(CubeFaceType)))
                _typedFaces[faceType].LinkCells();
        }

        private void CreateFaces(int faceSize)
        {
            foreach (CubeFaceType faceType in Enum.GetValues(typeof(CubeFaceType)))
                _typedFaces[faceType] = new Face(faceSize);
        }

        private void LinkFaces()
        {
            CubeFacesRelations cubeFacesRelations = new CubeFacesRelations();
            var cubeFaceTypes = Enum.GetValues(typeof(CubeFaceType));
            var directions = Enum.GetValues(typeof(Direction));

            foreach (CubeFaceType cubeFaceType in cubeFaceTypes)
            {
                var directedFaceTypes = cubeFacesRelations.DirectedNeighbors[cubeFaceType];
                
                foreach (Direction direction in directions)
                {
                    Face face = _typedFaces[cubeFaceType];
                    Face neighbor = _typedFaces[directedFaceTypes[direction]];
                    face.SetNeighbor(direction, neighbor);

                    foreach (var nonConsistentFace in cubeFacesRelations.NonConsistentFaceTypes[cubeFaceType])
                        face.AddNonConsistentFace(_typedFaces[nonConsistentFace]);
                }
            }
        }
    }
}