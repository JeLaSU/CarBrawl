using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using UnityEditor;


public class AStarLite : MonoBehaviour
{
    int gridSizeX = 100;
    int gridSizeY = 70;

    float cellSize = 1;

    AStarNode[,] aStarNodes;

    AStarNode startNode;

    List<AStarNode> nodesToCheck = new List<AStarNode>();
    List<AStarNode> nodesChecked = new List<AStarNode>();

    List<Vector2> aiPath = new List<Vector2>();


    Vector3 startPositionDebug = new Vector3(1000, 0, 0);
    Vector3 destinationPositionDebug = new Vector3(1000, 0, 0);

    public bool isDebugActiveForCar = false;


    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {

        aStarNodes = new AStarNode[gridSizeX, gridSizeY];


        for (int x = 0; x < gridSizeX; x++)
            for (int y = 0; y < gridSizeY; y++)
            {
                aStarNodes[x, y] = new AStarNode(new Vector2Int(x, y));

                Vector3 worldPosition = ConvertGridPositionToWorldPosition(aStarNodes[x, y]);


                Collider2D hitCollider2D = Physics2D.OverlapCircle(worldPosition, cellSize / 2.0f);

                if (hitCollider2D != null)
                {
                    if (hitCollider2D.isTrigger)
                        continue;

                    if (hitCollider2D.gameObject.layer == LayerMask.NameToLayer("UI"))
                        continue;

                    if (hitCollider2D.transform.root.CompareTag("AI"))
                        continue;

                    if (hitCollider2D.transform.root.CompareTag("Car"))
                        continue;

                    aStarNodes[x, y].isObstacle = true;

                }

            }



        for (int x = 0; x < gridSizeX; x++)
            for (int y = 0; y < gridSizeY; y++)
            {
                if (y - 1 >= 0)
                {
                    if (!aStarNodes[x, y - 1].isObstacle)
                        aStarNodes[x, y].neighbours.Add(aStarNodes[x, y - 1]);
                }

                if (y + 1 <= gridSizeY - 1)
                {
                    if (!aStarNodes[x, y + 1].isObstacle)
                        aStarNodes[x, y].neighbours.Add(aStarNodes[x, y + 1]);
                }

                if (x - 1 >= 0)
                {
                    if (!aStarNodes[x - 1, y].isObstacle)
                        aStarNodes[x, y].neighbours.Add(aStarNodes[x - 1, y]);
                }

                if (x + 1 <= gridSizeX - 1)
                {
                    if (!aStarNodes[x + 1, y].isObstacle)
                        aStarNodes[x, y].neighbours.Add(aStarNodes[x + 1, y]);
                }
            }
    }
    private void Reset()
    {
        nodesToCheck.Clear();
        nodesChecked.Clear();
        aiPath.Clear();

        for (int x = 0; x < gridSizeX; x++)
            for (int y = 0; y < gridSizeY; y++)
                aStarNodes[x, y].Reset();
    }


    public List<Vector2> FindPath(Vector2 destination)
    {
        if (aStarNodes == null)
            return null;

        Reset();

        Vector2Int destinationGridPoint = ConvertWorldToGridPoint(destination);
        Vector2Int currentPositionGridPoint = ConvertWorldToGridPoint(transform.position);

        destinationPositionDebug = destination;

        startNode = GetNodeFromPoint(currentPositionGridPoint);

        startPositionDebug = ConvertGridPositionToWorldPosition(startNode);

        AStarNode currentNode = startNode;

        bool isDoneFindingPath = false;
        int pickedOrder = 1;

        while (!isDoneFindingPath)
        {
            nodesToCheck.Remove(currentNode);

            currentNode.pickedOrder = pickedOrder;

            pickedOrder++;

            nodesChecked.Add(currentNode);

            if (currentNode.gridPosition == destinationGridPoint)
            {
                isDoneFindingPath = true;
                break;
            }

            CalculateCostsForNodeAndNeighbours(currentNode, currentPositionGridPoint, destinationGridPoint);

            foreach (AStarNode neighbourNode in currentNode.neighbours)
            {
                if (nodesChecked.Contains(neighbourNode))
                    continue;

                if (nodesToCheck.Contains(neighbourNode))
                    continue;

                nodesToCheck.Add(neighbourNode);
            }

            nodesToCheck = nodesToCheck.OrderBy(x => x.fCostTotal).ThenBy(x => x.hCostDistanceFromGoal).ToList();

            if (nodesToCheck.Count == 0)
            {
                Debug.LogWarning($"No nodes left in next nodes to check, we have no solution {transform.name}");
                return null;
            }
            else
            {
                currentNode = nodesToCheck[0];
            }
        }

        aiPath = CreatePathForAI(currentPositionGridPoint);

        return aiPath;
    }

    List<Vector2> CreatePathForAI(Vector2Int currentPositionGridPoint)
    {
        List<Vector2> resultAIPath = new List<Vector2>();
        List<AStarNode> aiPath = new List<AStarNode>();

        nodesChecked.Reverse();

        bool isPathCreated = false;

        AStarNode currentNode = nodesChecked[0];

        aiPath.Add(currentNode);

        int attempts = 0;

        while (!isPathCreated)
        {
            currentNode.neighbours = currentNode.neighbours.OrderBy(x => x.pickedOrder).ToList();

            foreach (AStarNode aStarNode in currentNode.neighbours)
            {
                if (!aiPath.Contains(aStarNode) && nodesChecked.Contains(aStarNode))
                {
                    aiPath.Add(aStarNode);
                    currentNode = aStarNode;

                    break;
                }
            }

            if (currentNode == startNode)
                isPathCreated = true;

            if (attempts > 1000)
            {
                Debug.LogWarning("CreatePathForAI failed after too many attempts");
                break;
            }

            attempts++;
        }

        foreach (AStarNode aStarNode in aiPath)
        {
            resultAIPath.Add(ConvertGridPositionToWorldPosition(aStarNode));
        }

        resultAIPath.Reverse();

        return resultAIPath;
    }

    void CalculateCostsForNodeAndNeighbours(AStarNode aStarNode, Vector2Int aiPosition, Vector2Int aiDestination)
    {
        aStarNode.CalculateCostsForNode(aiPosition, aiDestination);

        foreach (AStarNode neighbourNode in aStarNode.neighbours)
        {
            neighbourNode.CalculateCostsForNode(aiPosition, aiDestination);
        }
    }

    AStarNode GetNodeFromPoint(Vector2Int gridPoint)
    {
        if (gridPoint.x < 0)
            return null;

        if (gridPoint.x > gridSizeX - 1)
            return null;

        if (gridPoint.y < 0)
            return null;

        if (gridPoint.y > gridSizeY - 1)
            return null;

        return aStarNodes[gridPoint.x, gridPoint.y];
    }

    Vector2Int ConvertWorldToGridPoint(Vector2 position)
    {
        Vector2Int gridPoint = new Vector2Int(Mathf.RoundToInt(position.x / cellSize + gridSizeX / 2.0f), Mathf.RoundToInt(position.y / cellSize + gridSizeY / 2.0f));

        return gridPoint;
    }


    Vector3 ConvertGridPositionToWorldPosition(AStarNode aStarNode)
    {
        return new Vector3(aStarNode.gridPosition.x * cellSize - (gridSizeX * cellSize) / 2.0f, aStarNode.gridPosition.y * cellSize - (gridSizeY * cellSize) / 2.0f, 0);
    }

    void OnDrawGizmos()
    {
        if (aStarNodes == null)
            return;

        if (!isDebugActiveForCar)
            return;

        for (int x = 0; x < gridSizeX; x++)
            for (int y = 0; y < gridSizeY; y++)
            {
                  if(aStarNodes[x, y].isObstacle)
		                    Gizmos.color = Color.red;
                else Gizmos.color = Color.green;

                Gizmos.DrawWireCube(ConvertGridPositionToWorldPosition(aStarNodes[x, y]), new Vector3(cellSize, cellSize, cellSize));
            }

        foreach (AStarNode checkedNode in nodesChecked)
        {
            Gizmos.color = Color.green;

#if UNITY_EDITOR

            Vector3 labelPosition = ConvertGridPositionToWorldPosition(checkedNode);

            labelPosition.z = -1;

            GUIStyle style = new GUIStyle();

            style.normal.textColor = Color.green;

            Handles.Label(labelPosition + new Vector3(-0.6f, 1f, 0), $"{checkedNode.hCostDistanceFromGoal}", style);

            style.normal.textColor = Color.red;

            Handles.Label(labelPosition + new Vector3(0.5f, 1f, 0), $"{checkedNode.gCostDistanceFromStart}", style);

            style.normal.textColor = Color.yellow;

            Handles.Label(labelPosition + new Vector3(0.5f, -0.5f, 0), $"{checkedNode.pickedOrder}", style);

            style.normal.textColor = Color.white;

            Handles.Label(labelPosition + new Vector3(0, 0.2f, 0), $"{checkedNode.fCostTotal}", style);
#endif
        }

        foreach (AStarNode toCheckNode in nodesToCheck)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(ConvertGridPositionToWorldPosition(toCheckNode), 1.0f);
        }

        Vector3 lastAIPoint = Vector3.zero;
        bool isFirstStep = true;

        Gizmos.color = Color.black;

        foreach (Vector2 point in aiPath)
        {
            if (!isFirstStep)
                Gizmos.DrawLine(lastAIPoint, point);

            lastAIPoint = point;

            isFirstStep = false;

        }

        Gizmos.color = Color.black;
        Gizmos.DrawSphere(startPositionDebug, 0.2f);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(destinationPositionDebug, 0.2f);
    }

}
