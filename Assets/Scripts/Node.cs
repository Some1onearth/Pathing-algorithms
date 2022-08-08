using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    /// <summary>
    /// total cost of shortest "parth" to this node
    /// </summary>

    private float _pathWeight = int.MaxValue;
    private Node _previousNode = null;
    
    //breakpoint
    public float PathWeight
    {
        get { return _pathWeight; }
        set { _pathWeight = value; }
    }
    
    /// <summary>
    /// following the shortest path, previousNode is the previous step on that path
    /// </summary>
    public Node PreviousNode 
    {
        get { return _previousNode; }
        set { _previousNode = value; }
    }

    /// <summary>
    /// Nodes this node is connected to
    /// </summary>

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
