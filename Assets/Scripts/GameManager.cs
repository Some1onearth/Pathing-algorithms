using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] private GameObject _spawnLocation;
    [SerializeField] private GameObject _enemy;
    [SerializeField] public GameObject _tower;

    public Text scoreText;
    public static int playerScore;

    public int towerCount;
    public Vector3 towerLocation;


    public float timer = Mathf.Infinity;
    public float counter = 2;

    public void Awake()
    {
        gameManager = this;
    }

    public void Start()
    {
        _tower = GameObject.Find("Tower");
    }
    public void Update()
    {
        timer += Time.deltaTime;

        if (timer >= counter)
        {

            timer = 0;
            SpawnEnemy();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Clicked();


        }
    }

    public void AddScore(int score)
    {
        playerScore += score;
        scoreText.text = "Score: " + playerScore;
    }

    public void SpawnEnemy()
    {
        Instantiate(_enemy, _spawnLocation.transform.position, _spawnLocation.transform.rotation);
    }

    public void Clicked()
    {
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
        if (hit)
        {
            //  Debug.Log("Hit " + hitInfo.transform.gameObject.name);
            if (hitInfo.transform.gameObject.tag == "Tower")
            {
                towerLocation = hitInfo.transform.GetComponentInParent<Transform>().position;
                _tower.transform.position = towerLocation;
                // hitInfo.transform.GetComponentInParent();
            }
        }
    }



}
