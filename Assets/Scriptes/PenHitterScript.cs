using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenHitterScript : MonoBehaviour
{
    public GameObject Table;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);

       
        if (collision.gameObject.tag != "BluePen")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        if (collision.gameObject.tag == "BluePen")
        {
            Destroy(this.gameObject);
        }
        
    }
}
