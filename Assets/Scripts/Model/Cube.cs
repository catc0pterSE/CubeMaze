using System;
using System.Collections.Generic;

namespace Model
{
    public class Cube
    {
        private const int CubeFacesCount = 6;

        private readonly Face[] _faces = new Face[CubeFacesCount];

        private readonly int[,] _cubeFacesRelationModel = // index i = Front - 0; Left - 1; Right - 2; Back - 3; Top - 4; Bot - 5   || значение j - сосед L R U D соответвенно
        {
            { 1, 2, 4, 5 },
            { 3, 0, 4, 5 },
            { 0, 3, 4, 5 },
            { 2, 1, 4, 5 },
            { 1, 2, 3, 0 },
            { 1, 2, 0, 3 }
        };

        public Cube(int faceSize)
        {
            CreateFaces(faceSize);
            LinkFaces();
            LinkCells();
        }

        public IReadOnlyCollection<Face> Faces => _faces;

        private void LinkCells()
        {
            foreach (Face face in _faces)
                face.LinkCells();
        }

        private void CreateFaces(int faceSize)
        {
            for (int i = 0; i < _faces.Length; i++)
            {
                _faces[i] = new Face(faceSize);
            }
        }

        private void LinkFaces()
        {
            for (int i = 0; i < _cubeFacesRelationModel.GetLength(0); i++)
            {
                for (int j = 0; j < _cubeFacesRelationModel.GetLength(1); j++)
                {
                    _faces[i].SetNeighbor((Direction)j, _faces[_cubeFacesRelationModel[i, j]]);
                }
            }
        }
    }
}