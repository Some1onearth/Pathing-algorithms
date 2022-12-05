using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int attackDamage = 2;
    public GameObject radius;
    public float timer = Mathf.Infinity;
    public float counter = 2;

    public void Update()
    {
        timer += Time.deltaTime;
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (timer >= counter)
            {
                timer = 0;
                other.GetComponent<FollowPath>().Damage(attackDamage);
            }


        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Node")
        {
            Node n = other.GetComponentInParent<Node>();
            n.towerWeight = 9001; //add weight to the path
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Node")
        {
            if (other.tag == "Node")
            {
                Node n = other.GetComponentInParent<Node>();
                n.towerWeight = 0;
            }
        }
    }
}
