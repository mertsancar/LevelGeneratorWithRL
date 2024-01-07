using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : Agent
{
    // public static LevelGenerator instance;
    //
    // public int gridSize;
    // public Cell[,] grid;
    // public Transform gridLayout;
    // public GameObject cellPrefab;
    //
    // public Cell target;
    // public int pathLength = 0;
    //
    // private void Awake()
    // {
    //     if (instance == null)
    //     {
    //         instance = this;
    //     }
    //     
    //     GenerateGrid();
    //
    //     target = grid[gridSize-1, gridSize-1];
    // }
    //
    //
    // public override void OnEpisodeBegin()
    // {
    //     if (grid != null)
    //     {
    //         ResetCells();
    //         SetRandomTargetPosition();
    //     }
    // }
    //
    //
    // public override void CollectObservations(VectorSensor sensor)
    // {
    //     foreach (var cell in grid)
    //     {
    //         sensor.AddObservation(cell);
    //     }
    //     
    //     sensor.AddObservation(pathLength);
    // }
    //
    // public override void OnActionReceived(ActionBuffers actions)
    // {
    //     var cellX = actions.DiscreteActions[0];
    //     var cellY = actions.DiscreteActions[1];
    //     var cellTypeIndex = actions.DiscreteActions[2];
    //
    //     StartCoroutine(UpdateGrid(cellX, cellY, cellTypeIndex));
    // }

    // private IEnumerator UpdateGrid(int cellX, int cellY, int cellTypeIndex)
    // {
    //     var cellTypeList = Enum.GetValues(typeof(CellType)).Cast<CellType>().ToList();
    //
    //     if ((cellX != 0 && cellY != 0) || (cellX != target.x && cellY != target.y))
    //     {
    //         grid[cellX, cellY].SetCellType(cellTypeList[cellTypeIndex]);
    //     }
    //     else
    //     {
    //         AddReward(-1f);
    //         EndEpisode();
    //     }
    //
    //     CheckForReward();
    //     
    //     yield return new WaitForSeconds(3f);
    //     
    //     EndEpisode();
    // }
    
    // private void CheckForReward()
    // {
    //     var path = PathFinding.instance.Path(grid[0,0], grid[target.y, target.x]);
    //     if (path != null)
    //     {
    //         pathLength = path.Count;
    //         if (pathLength >= 3)
    //         {
    //             AddReward(2f);
    //             Debug.Log("YES");
    //         }
    //         else
    //         {
    //             AddReward(-1f);
    //             Debug.Log("NO");
    //         }
    //     }
    //     else
    //     {
    //         AddReward(-2f);
    //         Debug.Log("NO");
    //     }
    //
    //     Debug.Log("GetCumulativeReward: " + GetCumulativeReward());
    // }
    
    // private void SetRandomTargetPosition()
    // {
    //     var newTargetX = Random.Range(0, gridSize-1);
    //     var newTargetY = Random.Range(0, gridSize-1);
    //     if (newTargetX == 0 && newTargetY == 0)
    //     {
    //         newTargetX = Random.Range(0, gridSize-1);
    //         newTargetY = Random.Range(0, gridSize-1);
    //     }
    //     target.Init(newTargetX, newTargetY, CellType.Way, false, true);
    //     grid[target.y, target.x] = target;
    // }

    // private void GenerateGrid()
    // {
    //     grid = new Cell[gridSize, gridSize];
    //
    //     for (int x = 0; x < gridSize; x++)
    //     {
    //         for (int y = 0; y < gridSize; y++)
    //         {
    //             var cell = Instantiate(cellPrefab, gridLayout).GetComponent<Cell>();
    //             cell.transform.localPosition = new Vector2(x * 150, y * 150);
    //             if (x == 0 && y == 0)
    //             {
    //                 cell.Init(y, x, CellType.Way, true, false);
    //                 grid[y, x] = cell;
    //                 continue;
    //             }
    //
    //             if (x == gridSize-1 && y == gridSize-1)
    //             {
    //                 cell.Init(gridSize-1, gridSize-1, CellType.Way, false, true);
    //                 grid[gridSize-1, gridSize-1] = cell;
    //                 continue;
    //             }
    //
    //             cell.Init(y, x, CellType.Way, false, false);
    //             grid[y, x] = cell;
    //         }
    //     }
    // }
    //
    // private void ResetCells()
    // {
    //     foreach (var cell in grid)
    //     {
    //         cell.ResetCell();
    //     }
    // }
}
