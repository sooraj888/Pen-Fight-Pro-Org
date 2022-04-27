using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenController : MonoBehaviour
{
    public float power ;
    public float maxDrag ;
    public Rigidbody rb;
    public LineRenderer lr;
    public Object PenHitter;
    public Rigidbody rbPenHitter;


    Vector3 drageStartPos;
    Vector3 draggingPos;
    Touch touch;

    bool showLineIndicator;

    private void Start()
    {
        power = 10f;
        maxDrag = 5f;
        showLineIndicator = false;


    }


    public void Update()
    {
        if(Input.touchCount>0)
        {
            touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            /*Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);*/
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name);
                if (hit.collider != null)
                {

                    GameObject touchedObject = hit.transform.gameObject;

                    Debug.Log("Touched " + touchedObject.transform.name);
                   
                       
                        if (touch.phase == TouchPhase.Began)
                        {
                             if (touchedObject.transform.name == "Pen")
                             {
                                showLineIndicator = true;
                                DrageStart();

                             }

                        }
                }
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Dragging();
            }
            if (touch.phase == TouchPhase.Ended)
            {
               
                DrageEnd();
              
            }

        }
    }

    void DrageStart()
    {
        if(showLineIndicator)
        {
            drageStartPos = Camera.main.ScreenToWorldPoint(touch.position);
            drageStartPos.y = 1f;
            lr.positionCount = 1;
            lr.SetPosition(0, drageStartPos);
           
        }
       
       
    }
    void Dragging()
    {
        if (showLineIndicator)
        {
             draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
            draggingPos.y = 1f;
            lr.positionCount = 2;
            lr.SetPosition(1, draggingPos);
          
            if (GameObject.Find("PenHitter(Clone)")==null)
            {
                Debug.Log("hitter not found 404 error");
                Instantiate(PenHitter, new Vector3(draggingPos.x, rb.position.y, draggingPos.z), Quaternion.identity);
                GameObject.Find("PenHitter(Clone)").GetComponent<Collider>().enabled = false;
            }
            else
            {
                GameObject.Find("PenHitter(Clone)").GetComponent<Transform>().position = new Vector3(draggingPos.x, rb.position.y+0.06f, draggingPos.z);
            }
           
        }
    }
    void DrageEnd()
    {
       if(showLineIndicator)
        {

            showLineIndicator = false;
            lr.positionCount = 0;
            Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touch.position);
           
            Vector3 force = drageStartPos - dragReleasePos;
            Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;



            GameObject.Find("PenHitter(Clone)").GetComponent<Collider>().enabled = true;


            GameObject.Find("PenHitter(Clone)").GetComponent<Rigidbody>().AddForce(clampedForce,ForceMode.Impulse);
        }
            
       
        

    }
}
