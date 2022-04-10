using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    public string[] moveInfoString = {"1:0/2", "-1:0/2"};
    public float speed = 10;

    public Queue<string> moveInfoStringQueue;
    public bool isLooping = true;

    private Queue<MovingInfo> moveInfoQueue;
    private MovingInfo bufferMovingInfo;
    private MovingInfo currentMovingInfo;
    private Rigidbody2D rb2d;

    //public List<MovingInfo> MOVE_INFO_LIST;

    private void Awake()
    {
        moveInfoStringQueue = new Queue<string>(moveInfoString);
        moveInfoQueue = new Queue<MovingInfo>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(GenerateMovement());
    }

    // Update is called once per frame
    void Update()
    {
        if (moveInfoStringQueue.Count > 0)
        {
            ParseMovingInfo(moveInfoStringQueue.Dequeue(), out bufferMovingInfo);
            moveInfoQueue.Enqueue(bufferMovingInfo);
        }
    }

    private void FixedUpdate()
    {
        Debug.Log(currentMovingInfo.direction);
        rb2d.velocity = currentMovingInfo.direction * speed;
    }

    void ParseMovingInfo(string moveInfoString, out MovingInfo inf)
    {
        string[] moveInfo = moveInfoString.Split('/');
        float directionX, directionY, duration;
        string[] direction = moveInfo[0].Split(':');
        if (!float.TryParse(direction[0], out directionX) || !float.TryParse(direction[1], out directionY) || !float.TryParse(moveInfo[1], out duration))
        {
            Debug.LogError("Mover.ParseMovingInfo(): Could not parse MovingInfo: " + moveInfoString);
            inf = new MovingInfo(new Vector2(0, 0), 0);
            return;
        }
        inf = new MovingInfo(new Vector2(directionX, directionY), duration);
    }

    IEnumerator GenerateMovement()
    {            
        if(isLooping)            
        {
            Queue<MovingInfo> moveInfoQueueCopy = new Queue<MovingInfo>();
            while (true)
            {
                if (moveInfoQueue.Count > 0)
                {
                    currentMovingInfo = moveInfoQueue.Dequeue();
                    moveInfoQueueCopy.Enqueue(currentMovingInfo);
                    yield return new WaitForSeconds(currentMovingInfo.duration);
                }
                else
                {
                    moveInfoQueue = moveInfoQueueCopy;
                    moveInfoQueueCopy = new Queue<MovingInfo>();
                    yield return null;
                }
            }            
        }
    }
}

public struct MovingInfo
{
    public Vector2 direction;
    public float duration;

    public MovingInfo(Vector2 direction, float duration)
    {
        this.direction = direction;
        this.duration = duration;
    }
}