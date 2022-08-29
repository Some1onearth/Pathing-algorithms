using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    private void Start()
    {
        //List<Node> shortestPath = AFindShortestPath(start, end);
        //DrawPath(shortestPath);
    }
    private void DrawPath(List<Node> path)
    {
        Node prevNode = null;
        foreach (Node node in path)
        {
            if (prevNode != null)
            {
                Debug.DrawLine(node.transform.position, prevNode.transform.position, Color.yellow, 5, false);
            }
            prevNode = node;
        }
    }
    public List<Node> AFindShortestPath(Node start, Node end)
    {
        if (AStarAlgorithm(start, end))
        {
            List<Node> result = new List<Node>();
            Node node = end;
            do
            {
                result.Insert(0, node);
                node = node.PreviousNode;
            } while (node != null);
            return result;
        }
        return null;
    }
    private bool AStarAlgorithm(Node start, Node end)
    {
        end.PreviousNode = null;
        if (start == end)
        {
            return true;
        }
        List<Node> unexplored = new List<Node>();
        List<Node> explored = new List<Node>();
        start.ResetNode();
        start.PathWeight = 0;
        start.SetDirectDistanceToEnd(end.transform.position);
        unexplored.Add(start);

        while (unexplored.Count > 0)
        {
            unexplored.Sort((x, y) => (x.TotalPathWeight + x.weightDifference).CompareTo(y.TotalPathWeight + y.weightDifference));
            Node current = unexplored[0];
            if (current.PreviousNode != null)
            {
                Debug.DrawLine(current.transform.position, current.PreviousNode.transform.position, Color.cyan, 1, false);
            }
            if (current == end)
            {
                break;
            }
            unexplored.RemoveAt(0);
            foreach (Node neighborNode in current.NeighbouringNodes)
            {
                if (neighborNode == current.PreviousNode) continue;
                if (!explored.Contains(neighborNode)) neighborNode.ResetNode();
                float distance = Vector3.Distance(neighborNode.transform.position, current.transform.position);
                distance += current.PathWeight + neighborNode.weightDifference;
                if (distance < neighborNode.PathWeight + current.weightDifference)
                {
                    //Debug.DrawLine(current.transform.position, neighborNode.transform.position, Color.magenta, 1, false);
                    neighborNode.SetDirectDistanceToEnd(end.transform.position);
                    neighborNode.PathWeight = distance;
                    neighborNode.PreviousNode = current;
                    if (!unexplored.Contains(neighborNode)) unexplored.Add(neighborNode);
                }
            }
            if (unexplored.Count > 2000)
            {
                Debug.LogError("A* pathfinding unexplored count is over 2000 \n Returning false");
                return false;
            }
            explored.Add(current);
        }
        return !(end.PreviousNode == null);
    }
}
