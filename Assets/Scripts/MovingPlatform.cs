using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject platform;
    public GameObject point1;
    public GameObject point2;
    public float speed;
    [HideInInspector]
    protected bool direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (gameObject.transform.position == point1.transform.position)
        {
            inPoint1 = true;
            inPoint2 = false;
        }
        if (gameObject.transform.position == point2.transform.position)
        {
            inPoint2 = true;
            inPoint1 = false;
        }*/

        if (platform.GetComponent<MovingPlatformController>().inPoint1)
        {
            direction = true;
        }
        else if (platform.GetComponent<MovingPlatformController>().inPoint1)
        {
            direction = false;
        }

    }

    private void FixedUpdate()
    {
        Rigidbody2D platformRb = platform.GetComponent<Rigidbody2D>();
        if (direction)
        {
            Debug.Log("first "+direction + " " + platformRb.velocity);
            //Debug.Log("point2 "+Mathf.Clamp(point2.transform.localPosition.x, -1f, 1f) + " " + Mathf.Clamp(point2.transform.localPosition.y, -1f, 1f));
            platformRb.velocity = new Vector2(Mathf.Clamp(Mathf.Abs(platform.transform.localPosition.x) - Mathf.Abs(point2.transform.localPosition.x), -1f, 1f) * speed, 
                Mathf.Clamp(Mathf.Abs(platform.transform.localPosition.y) - Mathf.Abs(point2.transform.localPosition.y), -1f, 1f) * speed);
            Debug.Log(Mathf.Abs(platform.transform.localPosition.y) + " " + Mathf.Abs(point2.transform.localPosition.y));
        }
        else if (!direction)
        {
            Debug.Log("second "+direction + " " + platformRb.velocity);
            //Debug.Log("point1 " + Mathf.Clamp(point1.transform.localPosition.x, -1f, 1f) + " " + Mathf.Clamp(point1.transform.localPosition.y, -1f, 1f));
            platformRb.velocity = new Vector2(Mathf.Clamp(Mathf.Abs(platform.transform.localPosition.x) - Mathf.Abs(point1.transform.localPosition.x), -1f, 1f) * speed,
                Mathf.Clamp(Mathf.Abs(platform.transform.localPosition.y) - Mathf.Abs(point1.transform.localPosition.y), -1f, 1f) * speed);
            Debug.Log(Mathf.Abs(platform.transform.localPosition.y) + " " + Mathf.Abs(point1.transform.localPosition.y));
        }
        
    }
}
