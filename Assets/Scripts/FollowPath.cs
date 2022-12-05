using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private List<Transform> _waypoints = new List<Transform>();
    private int _moveIndex = 0;
    private float offset = 0.3f;
    private int enemyMaxHP = 100;
    [SerializeField] public int enemyCurHP;

    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        enemyCurHP = enemyMaxHP;
        UpdateWayPoint();
    }
    void UpdateWayPoint()
    {
        _waypoints.Clear();
        AStar pathFinder = GameObject.Find("GameHandler").GetComponent<AStar>();
        foreach (Node node in pathFinder.FindShortestPath(pathFinder.start, pathFinder.end))
        {
            _waypoints.Add(node.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateWayPoint();
        if (Vector3.Distance(transform.position, _waypoints[_moveIndex].transform.position) > offset)
        {
            dir = _waypoints[_moveIndex].position - transform.position;
            dir.Normalize();
            transform.Translate((dir * _moveSpeed) * Time.deltaTime);
        }


        if (Vector3.Distance(transform.position, _waypoints[_moveIndex].transform.position) <= offset && _moveIndex < _waypoints.Count - 1)
        {
            _moveIndex++;
        }

    }

    public void Damage(int damage)
    {
        enemyCurHP -= damage;
        // Debug.Log(enemyCurHP);

        if (enemyCurHP <= 0)
        {
            Debug.Log("Enemy Killed");
            Destroy(this.gameObject);
            GameManager.gameManager.AddScore(10);
        }
    }
}