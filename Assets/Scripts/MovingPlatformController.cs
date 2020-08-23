using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    [HideInInspector]
    public bool inPoint1;
    public bool inPoint2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag == "Point")
        {
            if (collision.name == "Point1")
            {
                inPoint1 = true;
                inPoint2 = false;
            }

            if (collision.name == "Point2")
            {
                inPoint1 = false;
                inPoint2 = true;
            }
        }
    }


}
