﻿using System;
using System.Collections.Generic;
using System.Linq;
using Model.Cube;

namespace Model
{
    public class BackTrackingMazeGenerator
    {
        public void Generate(Cell cell)
        {
            Stack<Cell> cellStack = new Stack<Cell>();
            Cell currentCell = cell;
            currentCell.Visited = true;
            currentCell.IsStart = true;

            Cell furthest = currentCell;
            int maxDistance = Int32.MinValue;

            do
            {
                List<Cell> unvisitedNeighbors = new List<Cell>();
                var directions = Enum.GetValues(typeof(Direction)).Cast<Direction>();

                foreach (Direction direction in directions)
                {
                    var neighbor = currentCell.Neighbors[direction];

                    if (neighbor.Visited == false)
                        unvisitedNeighbors.Add(neighbor);
                }

                if (unvisitedNeighbors.Count > 0)
                {
                    Cell chosenCell = unvisitedNeighbors[UnityEngine.Random.Range(0, unvisitedNeighbors.Count)];
                    DisableWalls(currentCell, chosenCell);
                    chosenCell.Visited = true;
                    currentCell = chosenCell;
                    cellStack.Push(currentCell);
                    
                    if (cellStack.Count > maxDistance)
                    {
                        maxDistance = cellStack.Count;
                        furthest = currentCell;
                    }

                }
                else
                {
                    currentCell = cellStack.Pop();
                }
            } while (cellStack.Count > 0);

            furthest.IsEnd = true;
        }

        private void DisableWalls(Cell current, Cell next)
        {
            current.DisableWall(current.NeighborDirections[next]);
            next.DisableWall(next.NeighborDirections[current]);
        }
    }
}