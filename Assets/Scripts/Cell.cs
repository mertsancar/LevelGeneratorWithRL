using UnityEngine;

public class Cell : MonoBehaviour
{
    public int X { get; private set; }          // X coordinate of the cell
    public int Y { get; private set; }          // Y coordinate of the cell
    public bool HasPlayer { get; set; }         // Indicates if the cell has the player
    public bool HasTarget { get; set; }         // Indicates if the cell has the target
    public bool IsWalkable  { get; set; }         // Indicates if the cell has the target

    // A* pathfinding variables
    public int GCost { get; set; }              // Cost of the path from the start node to this node
    public int HCost { get; set; }              // Heuristic cost from this node to the target node
    public int FCost => GCost + HCost;          // Total cost of the node (GCost + HCost)
    public Cell Parent { get; set; }            // Parent node in the path
    

    public void Initialize(int x, int y)
    {
        X = x;
        Y = y;
        GCost = 0;
        HCost = 0;
        Parent = null;
        IsWalkable = true;
        
        HasPlayer = false;
        HasTarget = false;
        
        var spriteColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 1f);
    }

    public void MarkedAsPath()
    {
        var spriteColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 0.5f);
    }
    
}