using System;
using System.Collections.Generic;
using UnityEngine;


public class AStarNode
{
    public Vector2Int gridPosition;

    public List<AStarNode> neighbours = new List<AStarNode>();

    public bool isObstacle = false;

    public int gCostDistanceFromStart = 0;

    public int hCostDistanceFromGoal = 0;

    public int fCostTotal = 0;

    public int pickedOrder = 0;

    bool isCostCalculated = false;

    public AStarNode(Vector2Int gridPosition_)
    {
        gridPosition = gridPosition_;
    }

    public void CalculateCostsForNode(Vector2Int aiPosition, Vector2Int aiDestination)
    {
        if (isCostCalculated)
            return;

        gCostDistanceFromStart = Mathf.Abs(gridPosition.x - aiPosition.x) + Mathf.Abs(gridPosition.y - aiPosition.y);

        hCostDistanceFromGoal = Mathf.Abs(gridPosition.x - aiDestination.x) + Mathf.Abs(gridPosition.y - aiDestination.y);

        fCostTotal = gCostDistanceFromStart + hCostDistanceFromGoal;

        isCostCalculated = true;
    }

    public void Reset()
    {
        isCostCalculated = false;
        pickedOrder = 0;
        gCostDistanceFromStart = 0;
        hCostDistanceFromGoal = 0;
        fCostTotal = 0;
    }
}

