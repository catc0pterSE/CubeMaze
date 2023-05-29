using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Model.Cube
{
    public class Face : CubeElement<Face>
    {
        private readonly int _faceSize;
        private readonly Cell[,] _cells;

        private List<Face> _nonConsistentFaces = new List<Face>();

        public Face(int faceSize)
        {
            _faceSize = faceSize;
            _cells = new Cell[faceSize, faceSize];
            CreateCells();
        }

        public int FaceSize => _faceSize;

        public void LinkCells()
        {
            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    var directions = Enum.GetValues(typeof(Direction)).Cast<Direction>();

                    Cell cell = _cells[i, j];
                    Vector2 point = new Vector2(i, j);

                    foreach (var direction in directions)
                        SetCellNeighbor(cell, point, direction);
                }
            }
        }

        public void AddNonConsistentFace(Face face) =>
            _nonConsistentFaces.Add(face);

        public Cell this[int i, int j] =>
            _cells[i, j];

        private Cell GetEdgeCell(Face fromFace, int index, Direction outcomingDirection)
        {
            Dictionary<Direction, Func<int, Cell>> getCell = new Dictionary<Direction, Func<int, Cell>>()
            {
                [Direction.Left] = edgeIndex => _cells[edgeIndex, 0],
                [Direction.Right] = edgeIndex => _cells[edgeIndex, _faceSize - 1],
                [Direction.Up] = edgeIndex => _cells[0, edgeIndex],
                [Direction.Down] = edgeIndex => _cells[_faceSize - 1, edgeIndex]
            };

            Direction incomingDirection = NeighborDirections[fromFace];


            if (_nonConsistentFaces.Contains(fromFace))
                index = _faceSize - 1 - index;

            return getCell[incomingDirection](index);
        }

        private void CreateCells()
        {
            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    Cell cell = new Cell();
                    
                    if (i==0)
                        cell.AddEdge(Direction.Up);
                    if (i == _faceSize-1)
                        cell.AddEdge(Direction.Down);
                    if (j == 0)
                        cell.AddEdge(Direction.Left);
                    if (j == _faceSize-1)
                        cell.AddEdge(Direction.Right);
                    
                    _cells[i, j] = cell;
                }
            }
        }

        private void SetCellNeighbor(Cell cell, Vector2 cellPosition, Direction direction)
        {
            Dictionary<Direction, Vector2> move = new Dictionary<Direction, Vector2>()
            {
                [Direction.Up] = new Vector2(-1, 0),
                [Direction.Right] = new Vector2(0, 1),
                [Direction.Down] = new Vector2(1, 0),
                [Direction.Left] = new Vector2(0, -1)
            };

            Vector2 targetPoint = cellPosition + move[direction];

            Cell neighbour;

            if (!OutOfRange(targetPoint))
            {
                neighbour = _cells[(int)targetPoint.X, (int)targetPoint.Y];
            }
            else
            {
                int index = IsHorizontalDirection(direction) ? (int)cellPosition.X : (int)cellPosition.Y;
                neighbour = Neighbors[direction].GetEdgeCell(this, index, direction);
            }

            cell.SetNeighbor(direction, neighbour);
            Console.WriteLine();
        }

        bool OutOfRange(Vector2 point) => point.X < 0 || point.X >= _faceSize || point.Y < 0 || point.Y >= _faceSize;

        private bool IsHorizontalDirection(Direction direction) =>
            direction == Direction.Left || direction == Direction.Right;
    }
}