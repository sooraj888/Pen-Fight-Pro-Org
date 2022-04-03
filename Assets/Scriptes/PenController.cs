using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenController : MonoBehaviour
{
    public float power ;
    public float maxDrag ;
    public Rigidbody rb;
    public LineRenderer lr;

    Vector3 drageStartPos;
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
                    if(touchedObject.transform.name=="Pen")
                    {
                        showLineIndicator = true;
                        Debug.Log("hit to pen");
                        if (touch.phase == TouchPhase.Began)
                        {
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
            Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
            draggingPos.y = 1f;
            lr.positionCount = 2;
            lr.SetPosition(1, draggingPos);
        }
    }
    void DrageEnd()
    {
       if(showLineIndicator)
        {
            showLineIndicator = false;
            lr.positionCount = 0;
            Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touch.position);
            /* drageStartPos.y = -0.5f;*/
            Vector3 force = drageStartPos - dragReleasePos;
            Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;
            rb.AddForceAtPosition(clampedForce, new Vector3(20, 0, 0), ForceMode.Impulse);
            
        }
            
       
        

    }
}
