using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporting : MonoBehaviour
{

    public GameObject anotherPoint;
    public GameObject player;
    [HideInInspector]
    public bool isEnterPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!anotherPoint.GetComponent<Teleporting>().isEnterPoint)
        {
            isEnterPoint = true;
            collision.gameObject.transform.position = new Vector2(anotherPoint.transform.position.x, anotherPoint.transform.position.y);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        anotherPoint.GetComponent<Teleporting>().isEnterPoint = false;
    }
}
