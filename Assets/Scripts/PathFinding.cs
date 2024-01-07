using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    // public static PathFinding instance;
    //
    // private void Start()
    // {
    //     if (instance == null)
    //     {
    //         instance = this;
    //     }
    //     
    //     gridSize = LevelGenerator.instance.gridSize;
    //     grid = new Node[gridSize, gridSize];
    //
    //     for (int x = 0; x < gridSize; x++)
    //     {
    //         for (int y = 0; y < gridSize; y++)
    //         {
    //             grid[x, y] = new Node(x, y, true);
    //         }
    //     }
    // }
    //
    // public class Node
    // {
    //     public int x;
    //     public int y;
    //     public bool walkable;
    //     public Node parent;
    //     public int gCost;
    //     public int hCost;
    //
    //     public int fCost
    //     {
    //         get { return gCost + hCost; }
    //     }
    //
    //     public Node(int x, int y, bool walkable)
    //     {
    //         this.x = x;
    //         this.y = y;
    //         this.walkable = walkable;
    //     }
    // }
    //
    // private Node[,] grid;
    // private int gridSize;
    //
    // public List<Node> Path(Cell startNode, Cell targetNode)
    // {
    //     // Call the A* pathfinding algorithm
    //
    //     foreach (var cell in LevelGenerator.instance.grid)
    //     {
    //         if (cell.type == CellType.Empty)
    //         {
    //             grid[cell.y, cell.x].walkable = false;
    //         }
    //     }
    //     
    //     List<Node> path = FindPath(startNode, targetNode);
    //
    //     foreach (var node in grid)
    //     {
    //         node.walkable = true;
    //     }
    //     
    //     if (path != null)
    //     {
    //         // Path found, do something with it (e.g., move along the path)
    //         Debug.Log("PATH FOUND!!");
    //         Debug.Log("Path Length " + path.Count);
    //         foreach (Node node in path)
    //         {
    //             Debug.Log("Path node: (" + node.x + ", " + node.y + ")");
    //         }
    //
    //         return path;
    //     }
    //     Debug.Log("No path found!");
    //     return null;
    // }
    //
    // private List<Node> FindPath(Cell startPos, Cell targetPos)
    // {
    //     Node startNode = GetNodeFromPosition(startPos);
    //     Node targetNode = GetNodeFromPosition(targetPos);
    //
    //     // Create open and closed lists
    //     List<Node> openList = new List<Node>();
    //     HashSet<Node> closedSet = new HashSet<Node>();
    //
    //     openList.Add(startNode);
    //
    //     while (openList.Count > 0)
    //     {
    //         Node currentNode = openList[0];
    //
    //         // Find the node with the lowest F cost in the open list
    //         for (int i = 1; i < openList.Count; i++)
    //         {
    //             if (openList[i].fCost < currentNode.fCost || openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost)
    //             {
    //                 currentNode = openList[i];
    //             }
    //         }
    //
    //         openList.Remove(currentNode);
    //         closedSet.Add(currentNode);
    //
    //         // Path found
    //         if (currentNode == targetNode)
    //         {
    //             return RetracePath(startNode, targetNode);
    //         }
    //
    //         // Generate the neighboring nodes
    //         List<Node> neighbors = GetNeighbors(currentNode);
    //
    //         foreach (Node neighbor in neighbors)
    //         {
    //             if (!neighbor.walkable || closedSet.Contains(neighbor))
    //             {
    //                 continue;
    //             }
    //
    //             int newCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
    //
    //             if (newCostToNeighbor < neighbor.gCost || !openList.Contains(neighbor))
    //             {
    //                 neighbor.gCost = newCostToNeighbor;
    //                 neighbor.hCost = GetDistance(neighbor, targetNode);
    //                 neighbor.parent = currentNode;
    //
    //                 if (!openList.Contains(neighbor))
    //                 {
    //                     openList.Add(neighbor);
    //                 }
    //             }
    //         }
    //     }
    //
    //     // No path found
    //     return null;
    // }
    //
    // private List<Node> RetracePath(Node startNode, Node endNode)
    // {
    //     List<Node> path = new List<Node>();
    //     Node currentNode = endNode;
    //
    //     while (currentNode != startNode)
    //     {
    //         path.Add(currentNode);
    //         currentNode = currentNode.parent;
    //     }
    //
    //     path.Reverse();
    //     return path;
    // }
    //
    // private List<Node> GetNeighbors(Node node)
    // {
    //     List<Node> neighbors = new List<Node>();
    //
    //     for (int x = -1; x <= 1; x++)
    //     {
    //         for (int y = -1; y <= 1; y++)
    //         {
    //             if (x == 0 && y == 0)
    //             {
    //                 continue;
    //             }
    //
    //             int checkX = node.x + x;
    //             int checkY = node.y + y;
    //
    //             if (checkX >= 0 && checkX < gridSize && checkY >= 0 && checkY < gridSize)
    //             {
    //                 neighbors.Add(grid[checkX, checkY]);
    //             }
    //         }
    //     }
    //
    //     return neighbors;
    // }
    //
    // private int GetDistance(Node nodeA, Node nodeB)
    // {
    //     int distX = Mathf.Abs(nodeA.x - nodeB.x);
    //     int distY = Mathf.Abs(nodeA.y - nodeB.y);
    //
    //     return distX + distY;
    // }
    //
    // private Node GetNodeFromPosition(Cell position)
    // {
    //     // int rounedX = Mathf.RoundToInt(position.x);
    //     // int roundedY = Mathf.RoundToInt(position.y);
    //     //
    //     // var x = rounedX == 0 ? 0 : rounedX - 1;
    //     // var y = roundedY == 0 ? 0 : roundedY - 1;
    //     
    //     return grid[position.x, position.y];
    // }
}
