using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class GridGenerationAgent : Agent
{
    private GridGenerator gridGenerator;  
    private AStarPathFinder pathfinder;

    
    public int desiredPathLength;

    private bool isGameCreated = false;
    
    public override void Initialize()
    {
        gridGenerator = GetComponent<GridGenerator>();
        pathfinder = GetComponent<AStarPathFinder>();
    }

    public override void OnEpisodeBegin()
    {
        if (gridGenerator.GetGridCells() != null)
        {
            if (!isGameCreated) //!isGameCreated
            {
                gridGenerator.ResetGrid();
                
                gridGenerator.RandomlyPlacePlayer();
                
            }
            else
            {
                
                Destroy(GetComponent<DecisionRequester>());
                Destroy(GetComponent<GridGenerationAgent>());
            }
            
        }
        
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Add observations of the grid state
        Cell[,] gridCells = gridGenerator.GetGridCells();
        int gridSize = gridGenerator.GetGridSize();

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Cell cell = gridCells[x, y];
                // Add observation for player presence
                sensor.AddObservation(cell.HasPlayer ? 1 : 0);
                // Add observation for target presence
                sensor.AddObservation(cell.HasTarget ? 1 : 0);
            }
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int targetIndex = actions.DiscreteActions[0];
        int obstacle1Index = actions.DiscreteActions[1];
        int obstacle2Index = actions.DiscreteActions[2];
        int obstacle3Index = actions.DiscreteActions[3];

        gridGenerator.RandomlyPlaceTarget(targetIndex);
        gridGenerator.RandomlyPlaceObstacle(obstacle1Index);
        gridGenerator.RandomlyPlaceObstacle(obstacle2Index);
        gridGenerator.RandomlyPlaceObstacle(obstacle3Index);

        var path = pathfinder.FindPath(gridGenerator.GetPlayer(), gridGenerator.GetTarget());
        
        float reward = CalculateReward(path);
        SetReward(reward);

        Debug.Log("GetCumulativeReward: "+ GetCumulativeReward());

        isGameCreated = true;
        
        EndEpisode();

    }

    public override void Heuristic(in ActionBuffers actionsOut) 
    {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = GetHeuristicAction();
    }

    private float CalculateReward(List<Cell> path)
    {
        int reward = 0;
        if (path != null)
        {
            if (path.Count >= desiredPathLength)
            {
                reward += path.Count * 1;
            }
            else if (path.Count < desiredPathLength)
            {
                reward += Math.Abs(path.Count - desiredPathLength) * -2;
            }
        }

        return reward;
        
    }

    private int GetHeuristicAction()
    {
        // TODO: Define heuristic control for manual testing (optional)
        // Return the action index based on some heuristic strategy
        return 0;
    }
}
