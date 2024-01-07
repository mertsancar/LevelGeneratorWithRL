using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridGenerator : MonoBehaviour
{
    public static GridGenerator instance;
    
    public GameObject gridCellPrefab;  // Prefab for the grid cell
    public GameObject playerPrefab;   // Prefab for the player
    public GameObject targetPrefab;   // Prefab for the target
    public GameObject obstaclePrefab;   // Prefab for the target
      // Prefab for the target

    private const int gridSize = 5;   // Size of the grid
    private Cell[,] gridCells;        // 2D array to store the grid cells

    private GameObject currentPlayer;
    private GameObject currentTarget;
    private List<GameObject> obstacles = new List<GameObject>();

    private AStarPathFinder pathfinder;
    
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        GenerateGrid();
        
        // StartCoroutine(Demo());
        
    }



    private IEnumerator Demo()
    {
        GenerateGrid();
        
        for (int i = 0; i < 10; i++)
        {
                
            RandomlyPlaceTarget(Random.Range(8, 12));
            RandomlyPlaceObstacle(Random.Range(4, 7));
            RandomlyPlaceObstacle(Random.Range(13, 18));
            RandomlyPlaceObstacle(Random.Range(0, 3));

            pathfinder = GetComponent<AStarPathFinder>();

            pathfinder.FindPath(GetPlayer(), GetTarget());
                
            yield return new WaitForSeconds(2f);
            
            ResetGrid();
        }
        
    } 

    private void GenerateGrid()
    {
        gridCells = new Cell[gridSize, gridSize];

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector2 position = new Vector2(x, y);
                CreateGridCell(position, x, y);
            }
        }

        // Create player and target
        Vector2 playerPosition = new Vector2(0, 0);  // Set the player's position
        CreatePlayer(playerPosition, 0,0);

        Vector2 targetPosition = new Vector2(gridSize - 1, gridSize - 1);  // Set the target's position
        CreateTarget(targetPosition, gridSize-1, gridSize-1);
    }

    private void CreateGridCell(Vector2 position, int x, int y)
    {
        GameObject cellObject = Instantiate(gridCellPrefab, position, Quaternion.identity);
        Cell cell = cellObject.GetComponent<Cell>();
        cell.Initialize(x, y);
        
        cellObject.transform.SetParent(transform);

        gridCells[x, y] = cell;
    }

    private void CreatePlayer(Vector2 position, int x, int y)
    {
        currentPlayer = Instantiate(playerPrefab, position, Quaternion.identity);
        currentPlayer.transform.SetParent(transform);
        gridCells[x, y].HasPlayer = true;
    }

    private void CreateTarget(Vector2 position, int x, int y)
    {
        currentTarget = Instantiate(targetPrefab, position, Quaternion.identity);
        currentTarget.transform.SetParent(transform);
        gridCells[x, y].HasTarget = true;
    }
    
    private void CreateObstacle(Vector2 position, int x, int y)
    {
        var go = Instantiate(obstaclePrefab, position, Quaternion.identity);
        obstacles.Add(go);
        
        go.transform.SetParent(transform);
        gridCells[x, y].IsWalkable = false;
    }
    
    public Cell GetPlayer()
    {
        foreach (var cell in gridCells)
        {
            if (cell.HasPlayer)
            {
                return cell;
            }
        }

        return null;
    }
    
    public Cell GetTarget()
    {
        foreach (var cell in gridCells)
        {
            if (cell.HasTarget)
            {
                return cell;
            }
        }

        return null;
    }
    
    private List<Cell> GetObstacles()
    {
        List<Cell> obstacleCells = new List<Cell>();
        
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Cell cell = gridCells[x, y];
                if (cell.IsWalkable)
                {
                    obstacleCells.Add(cell);
                }
            }
        }

        return obstacleCells;
    }
    
    public Cell[,] GetGridCells()
    {
        return gridCells;
    }
    
    public int GetGridSize()
    {
        return gridSize;
    }

    public void ResetGrid()
    {
        if (gridCells != null)
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    gridCells[x, y].Initialize(x, y);
                }
            }
            
            Destroy(currentPlayer);
            Destroy(currentTarget);
            foreach (var obstacle in obstacles)
            {
                Destroy(obstacle);
            }

            CreatePlayer(new Vector2(0,0), 0, 0);
            CreateTarget(new Vector2(gridSize-1,gridSize-1), gridSize-1, gridSize-1);
            CreateObstacle(new Vector2(2, 2), 2, 2);

        }
    }

    public void RandomlyPlacePlayer()
    {
        Cell playerCell = GetPlayer();
        gridCells[0, 0] = playerCell;
    }
    
    public void RandomlyPlaceTarget(int randomIndex)
    {
        List<Cell> emptyCells = new List<Cell>();
        
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Cell cell = gridCells[x, y];
                if (!cell.HasPlayer && !cell.HasTarget && cell.IsWalkable)
                {
                    emptyCells.Add(cell);
                }
            }
        }
        
        if (emptyCells.Count > 0)
        {
            Cell targetCell = GetTarget();
            targetCell.HasTarget = false;
            
            Destroy(currentTarget);

            var newCell = emptyCells[randomIndex];
            CreateTarget(new Vector2(newCell.X, newCell.Y), newCell.X, newCell.Y);
        }
    }
    
    public void RandomlyPlaceObstacle(int randomIndex)
    {
        List<Cell> emptyCells = new List<Cell>();
        
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Cell cell = gridCells[x, y];
                if (!cell.HasPlayer && !cell.HasTarget && cell.IsWalkable)
                {
                    emptyCells.Add(cell);
                }
            }
        }

        if (emptyCells.Count > 0)
        {
            var obstacleCells = GetObstacles();

            // if (obstacleCells.Count > 3)
            // {
            //     foreach (var obstacleCell in obstacleCells)
            //     {
            //         obstacleCell.IsWalkable = true;
            //     }
            //
            //     foreach (var obstacle in obstacles)
            //     {
            //         Destroy(obstacle);
            //     }
            // }

            var newCell = emptyCells[randomIndex];

            CreateObstacle(new Vector2(newCell.X, newCell.Y), newCell.X, newCell.Y);

        }

    }
    
    
    private Cell GetCellAtPosition(Vector2 position)
    {
        int x = Mathf.FloorToInt(position.x);
        int y = Mathf.FloorToInt(position.y);

        if (x >= 0 && x < gridSize && y >= 0 && y < gridSize)
        {
            return gridCells[x, y];
        }

        return null;
    }


}
