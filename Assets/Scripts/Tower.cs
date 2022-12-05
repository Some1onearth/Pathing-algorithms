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




    public void FixedUpdate()
    {


    }


    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //   Debug.Log(other.gameObject.name);
            if (timer >= counter)
            {
                timer = 0;
                other.GetComponent<FollowPath>().Damage(attackDamage);
                //   Debug.Log("Doing Damage");
            }


        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Nodes")
        {
            Node n = other.GetComponentInParent<Node>();
            n.towerWeight = 1000000;
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Nodes")
        {
            if (other.tag == "Nodes")
            {
                Node n = other.GetComponentInParent<Node>();
                n.towerWeight = 0;
            }
        }
    }
}
