using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenHitterScript : MonoBehaviour
{
    public GameObject Table;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
       
        if (collision.gameObject.tag == "Table")
        { 
            Physics.IgnoreCollision(GameObject.Find("Table").GetComponent<Collider>(), GetComponent<Collider>());
        }

        if (collision.gameObject.tag == "BluePen")
        {
            Destroy(this.gameObject);
        }
    }
}
