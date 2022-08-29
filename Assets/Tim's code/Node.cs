using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Node : MonoBehaviour
{
    public float weightDifference;
    private static bool _showNodes;
#if UNITY_EDITOR

    [MenuItem("Nodes/Set Neighbors")]
    public static void SetNeighbors()
    {
        GameObject[] nodeObjects = GameObject.FindGameObjectsWithTag("Node");
        List<Node> nodes = new List<Node>();
        foreach (GameObject gameObject in nodeObjects)
        {
            nodes.Add(gameObject.GetComponent<Node>());
            nodes[nodes.Count - 1].NeighbouringNodes.Clear();
        }
        foreach (Node node in nodes)
        {
            foreach (Node otherNode in nodes)
            {
                if (node == otherNode)
                {
                    continue;
                }
                if (Vector3.Distance(otherNode.transform.position,node.transform.position) < 4)
                {
                    node.NeighbouringNodes.Add(otherNode);
                }
            }
        }
    }
    [MenuItem("Nodes/Clear Neighbors")]
    public static void ClearNeighbors()
    {
        GameObject[] nodeObjects = GameObject.FindGameObjectsWithTag("Node");
        List<Node> nodes = new List<Node>();
        foreach (GameObject gameObject in nodeObjects)
        {
            nodes.Add(gameObject.GetComponent<Node>());
        }
        foreach (Node node in nodes)
        {
            node._neighbouringNodes.Clear();
        }
    }
    [MenuItem("Nodes/Toggle Paths")]
    public static void TogglePaths()
    {
        _showNodes = !_showNodes;
    }
#endif
    private float _directDistanceToEnd = float.MaxValue;
    public float DirectDistanceToEnd
    {
        get { return _directDistanceToEnd; }
        set { _directDistanceToEnd = value; }
    }
    /// <summary>
    /// Total cost of the shortest path to this node
    /// </summary>
    private float _pathWeight = int.MaxValue;
    public float TotalPathWeight
    {
        get { return _pathWeight + _directDistanceToEnd; }
    }
    public float PathWeight
    {
        get { return _pathWeight; }
        set { _pathWeight = value; }
    }
    /// <summary>
    /// The previous node to this node folloing the shortest path
    /// </summary>
    private Node _previousNode = null;
    public Node PreviousNode
    {
        get { return _previousNode; }
        set { _previousNode = value; }
    }
    /// <summary>
    /// Nodes that this node is connected to
    /// </summary>
    [SerializeField] private List<Node> _neighbouringNodes;
    public List<Node> NeighbouringNodes { get => _neighbouringNodes; }
    private void Start()
    {
        ResetNode();
        ValidateNeighbours();
        //Destroy(gameObject);
    }
    public void ResetNode()
    {
        _pathWeight = int.MaxValue;
        _previousNode = null;
        DirectDistanceToEnd = float.MaxValue;
    }
    public List<Node> AddNeighbourNode(Node node)
    {
        _neighbouringNodes.Add(node);
        return _neighbouringNodes;
    }
    private void OnDrawGizmos()
    {
        foreach (Node node in _neighbouringNodes)
        {
            if (node == null || !_showNodes)
            {
                continue;
            }
            Debug.DrawLine(transform.position, node.transform.position, Color.black);
        }
    }
    private void OnValidate()
    {
        ValidateNeighbours();
    }
    private void ValidateNeighbours()
    {
        foreach (Node node in _neighbouringNodes)
        {
            if (node == null)
            {
                continue;
            }
            if (!node._neighbouringNodes.Contains(this))
            {
                node.AddNeighbourNode(this);
            }
        }
    }
    public void SetDirectDistanceToEnd(Vector3 endPosition)
    {
        DirectDistanceToEnd = Vector3.Distance(transform.position, endPosition);
    }
}