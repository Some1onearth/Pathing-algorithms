using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private float _directDistanceToEnd = 0;

    public float DirectDistanceToEnd//finding direct distance to end
    {
        get { return _directDistanceToEnd; }
    }

    public void SetDirectDistanceToEnd(Vector3 endPosition)
    {
        _directDistanceToEnd = Vector3.Distance(transform.position, endPosition);
    }

    /// <summary>
    /// total cost of shortest "parth" to this node
    /// </summary>

    private float _pathWeight = 1;

    //breakpoint
    public float PathWeightHeuristic
    {
        get { return _pathWeight + towerWeight; }
        set { _pathWeight = value; }
    }

    public float PathWeight
    {
        get { return _pathWeight + _directDistanceToEnd; }
        set { _pathWeight = value; }
    }

    /// <summary>
    /// following the shortest path, previousNode is the previous step on that path
    /// </summary>
    private Node _previousNode = null;
    public Node PreviousNode
    {
        get { return _previousNode; }
        set { _previousNode = value; }
    }

    /// <summary>
    /// Nodes this node is connected to
    /// </summary>

    public float towerWeight = 0;
    [SerializeField] private List<Node> _neighbourNodes;
    public List<Node> NeighbourNodes
    {
        get
        {
            List<Node> result = new List<Node>(_neighbourNodes);
            return result; 
        } 
    }

    private void Start()
    {
        ResetNode();
        ValidateNeighbours();
    }

    public void ResetNode()
    {
        _pathWeight = int.MaxValue;
        _previousNode = null;
        _directDistanceToEnd = 0;
    }

    public void AddNeighbourNode(Node node)
    {
        _neighbourNodes.Add(node);
    }

    private void OnDrawGizmos()
    {
        foreach(Node node in _neighbourNodes)
        {
            if (node == null) continue;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
    }

    private void OnValidate()
    {
        ValidateNeighbours();
    }

    private void ValidateNeighbours()
    {
        foreach (Node node in _neighbourNodes)
        {
            if (node == null) continue;

            if (!node._neighbourNodes.Contains(this))
            {
                node.AddNeighbourNode(this);
            }
        }
    }

}
