using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPlayer : MonoBehaviour
{

    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.tag == "Player")
    //    {
    //        Debug.Log("Da va cham");
    //        //collision.collider.GetComponent<Player>().AddBrick();
    //        //this.gameObject.SetActive(false);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if(other.tag == "Player")
        {
            other.GetComponent<Player>().AddBrick();
            this.gameObject.SetActive(false);
        }
    }
}
