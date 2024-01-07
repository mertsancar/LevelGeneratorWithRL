using System.Collections.Generic;
using UnityEngine;

public class AStarPathFinder : MonoBehaviour
{
    private List<Cell> openList;
    private List<Cell> closedList;

    public GridGenerator gridGenerator;

    public List<Cell> FindPath(Cell startCell, Cell targetCell)
    {
        var grid = gridGenerator.GetGridCells();
        
        openList = new List<Cell>();
        closedList = new List<Cell>();

        openList.Add(startCell);

        while (openList.Count > 0)
        {
            Cell currentCell = GetLowestFCostCell(openList);

            openList.Remove(currentCell);
            closedList.Add(currentCell);

            if (currentCell == targetCell)
            {
                // Path found, reconstruct the path
                List<Cell> path = ConstructPath(startCell, targetCell);
                foreach (var cell in path)
                {
                    cell.MarkedAsPath();
                    Debug.Log(cell.X  + "," + cell.Y);
                }
                Debug.Log("*******");
                return path;
            }

            List<Cell> neighbors = GetWalkableNeighbors(grid, currentCell);

            foreach (Cell neighbor in neighbors)
            {
                if (closedList.Contains(neighbor))
                {
                    continue;
                }

                int newMovementCostToNeighbor = currentCell.GCost + CalculateDistance(currentCell, neighbor);

                if (newMovementCostToNeighbor < neighbor.GCost || !openList.Contains(neighbor))
                {
                    neighbor.GCost = newMovementCostToNeighbor;
                    neighbor.HCost = CalculateDistance(neighbor, targetCell);
                    neighbor.Parent = currentCell;

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }

        return null;

    }

    private List<Cell> ConstructPath(Cell startCell, Cell targetCell)
    {
        List<Cell> path = new List<Cell>();
        Cell currentCell = targetCell;

        while (currentCell != startCell)
        {
            path.Add(currentCell);
            currentCell = currentCell.Parent;
        }

        path.Reverse();
        return path;
    }

    private int CalculateDistance(Cell cellA, Cell cellB)
    {
        int distanceX = Mathf.Abs(cellA.X - cellB.X);
        int distanceY = Mathf.Abs(cellA.Y - cellB.Y);

        return distanceX + distanceY;
    }

    private Cell GetLowestFCostCell(List<Cell> cellList)
    {
        Cell lowestFCostCell = cellList[0];

        for (int i = 1; i < cellList.Count; i++)
        {
            if (cellList[i].FCost < lowestFCostCell.FCost)
            {
                lowestFCostCell = cellList[i];
            }
        }

        return lowestFCostCell;
    }

    private List<Cell> GetWalkableNeighbors(Cell[,] grid, Cell cell)
    {
        List<Cell> neighbors = new List<Cell>();

        int gridSize = grid.GetLength(0);
        int x = cell.X;
        int y = cell.Y;

        // Check left neighbor
        if (x > 0 && grid[x - 1, y].IsWalkable)
        {
            neighbors.Add(grid[x - 1, y]);
        }

        // Check right neighbor
        if (x < gridSize - 1 && grid[x + 1, y].IsWalkable)
        {
            neighbors.Add(grid[x + 1, y]);
        }

        // Check top neighbor
        if (y > 0 && grid[x, y - 1].IsWalkable)
        {
            neighbors.Add(grid[x, y - 1]);
        }

        // Check bottom neighbor
        if (y < gridSize - 1 && grid[x, y + 1].IsWalkable)
        {
            neighbors.Add(grid[x, y + 1]);
        }

        return neighbors;
    }
}
