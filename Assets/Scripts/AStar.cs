using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    private GameObject[] _nodes;
    
    public Node start;
    public Node end;
    public System.Diagnostics.Stopwatch stopwatch;

    private void Start()
    {
        //stopwatch.Start();

        List<Node> shortestPath = FindShortestPath(start, end);

        Node prevNode = null;

        if (shortestPath != null)
        {
            foreach (Node node in shortestPath)
            {
                if (prevNode != null)
                {
                    Debug.DrawLine(node.transform.position + Vector3.up, prevNode.transform.position + Vector3.up, Color.green, 10f);
                }

                Debug.Log(node.gameObject.name);
                prevNode = node;
            }
        }
        //stopwatch.Stop();
        //Debug.LogWarning("AStar: " + stopwatch.Elapsed);
    }


    public List<Node> FindShortestPath(Node start, Node end)
    {
        _nodes = GameObject.FindGameObjectsWithTag("Node");

        foreach (GameObject node in _nodes)
        {

        }

        if (AStarAlgorithm(start, end))
        {
            List<Node> result = new List<Node>();

            Node node = end;
            do
            {
                result.Insert(0, node);
                node = node.PreviousNode;
            } while (node != null);
            
            Debug.Log((AStarAlgorithm(start, end)));
            return result;
        }
        Debug.Log((AStarAlgorithm(start, end)));
        return null;
    }
    private bool AStarAlgorithm(Node start, Node end)
    {
        List<Node> unexplored = new List<Node>();

        foreach (GameObject obj in _nodes)
        {
            Node n = obj.GetComponent<Node>();
            if (n == null) continue;//continue stops at this point and continues the loop
            n.ResetNode();
            n.SetDirectDistanceToEnd(end.transform.position);
            unexplored.Add(n);
        }
        if (!unexplored.Contains(start) && !unexplored.Contains(end))
        {
            return false; //returns true if found a path and false if it doesn't
        }
        start.PathWeight = 0;
        while (unexplored.Count > 0)
        {
            //order based on path
            unexplored.Sort((x, y) => x.PathWeightHeuristic.CompareTo(y.PathWeightHeuristic)); //explaining to the function

            //current is the current shortest path possible
            Node current = unexplored[0];

            if (current == end)
            {
                break;
            }

            unexplored.RemoveAt(0); //faster than Remove, Remove is already fast

            foreach (Node neighbourNode in current.NeighbourNodes)
            {
                if (current.PreviousNode == neighbourNode) continue;
                if (!unexplored.Contains(neighbourNode)) continue; // ensure you don't explore the same things again

                float distance = Vector3.Distance(neighbourNode.transform.position, current.transform.position);

                distance += current.PathWeight;

                if (distance < neighbourNode.PathWeight)
                {
                    neighbourNode.PathWeight = distance;
                    neighbourNode.PreviousNode = current;
                }


            }
        }
        return true;
    }

    ///<summary>
    ///
    /// </summary>
    /// <param name="start"></param>
    /// <param name="node"></param>
    /// <returns></returns>
    /// 

}
