using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject platform;
    public GameObject point1;
    public GameObject point2;
    public float speed = 0.1f;
    public float pause = 2;
    private bool pauseCheck;
    private bool switching = false;
    private float waitUntilTime;
    //[HideInInspector]
    //protected bool direction;

    // Start is called before the first frame update
    void Start()
    {
        pauseCheck = false;
    }
    /*IEnumerator Pause()
    {        
        pauseCheck = true;
        yield return new WaitForSeconds(pause);
        pauseCheck = false;
    }*/
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
        }

        if (platform.GetComponent<MovingPlatformController>().inPoint1)
        {
            direction = true;
        }
        else if (platform.GetComponent<MovingPlatformController>().inPoint1)
        {
            direction = false;
        }*/

    }

    private void FixedUpdate()
    {
        //Rigidbody2D platformRb = platform.GetComponent<Rigidbody2D>();
        /*if (direction)
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
        }*/
        /*if (platform.transform.position == point1.transform.position || platform.transform.position == point2.transform.position)
        {
            if (!pauseCheck)
            {
                //Debug.Log(pauseCheck);
                //Debug.Log(((platform.transform.position == point1.transform.position || platform.transform.position == point2.transform.position) && !pauseCheck));
                StartCoroutine(Pause());
                platform.transform.position = Vector3.Lerp(point1.transform.position, point2.transform.position, 10//Mathf.PingPong(Time.time * speed, 1.0f)
                    );
            }
        }
        if (!pauseCheck)
        {
            Debug.Log(pauseCheck);
            platform.transform.position = Vector3.Lerp(point1.transform.position, point2.transform.position, 10//Mathf.PingPong(Time.time * speed, 1.0f)
                );
        }*/
        
        if (platform.transform.position == point1.transform.position || platform.transform.position == point2.transform.position)
        {
            if (pause != 0 && !pauseCheck)
            {
                //Debug.Log(pauseCheck);
                pauseCheck = true;
                waitUntilTime = Time.time + pause;
               // Debug.Log(Time.time);
                //Debug.Log(waitUntilTime);
            }
            else
            {
                if (Time.time < waitUntilTime)
                {
                    return;
                }
                switching = !switching;
                pauseCheck = false;
            }
        }
        if (switching)
        {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, point1.transform.position, speed * Time.fixedDeltaTime);
        }  
        else
        {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, point2.transform.position, speed*Time.fixedDeltaTime);
        }
    }
}
