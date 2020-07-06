using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporting : MonoBehaviour
{

    public GameObject point1;
    public GameObject point2;

    // Update is called once per frame
    void Update()
    {
        if (point1.GetComponent<TeleportPointing>().trigger)
        {
            point1.GetComponent<TeleportPointing>().characterCollider.gameObject.transform.position = point2.gameObject.transform.position;
        }
        if (point2.GetComponent<TeleportPointing>().trigger)
        {
            point2.GetComponent<TeleportPointing>().characterCollider.gameObject.transform.position = point1.gameObject.transform.position;
        }
    }

    
}
