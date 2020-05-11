using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{

    public GameObject interactionText;
    public bool isDraggable = false;
    private bool interaction;
    private GameObject collisionGameObject;
    public int objMass = 1;
    public float force = 1;
    public Collider2D triggerCollider;

    public bool isInteraction()
    {
        return interaction;
    }
        

    private void Start()
    {
        interactionText.SetActive(false);
        gameObject.GetComponent<Rigidbody2D>().mass = objMass;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().isInteraction = true;
            interactionText.SetActive(true);
            interaction = true;
            collisionGameObject = collision.gameObject;
            //Instantiate(interactionText, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2, gameObject.transform.position.z), interactionText.transform.rotation);
        }
      
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().isInteraction = false;
            interactionText.SetActive(false);
            interaction = false;
            collisionGameObject = null;
            //Destroy(interactionText);
        }
    }

  /*  private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().isInteraction = true;
            interactionText.SetActive(true);
            interaction = true;
            collisionGameObject = collision.gameObject;
            //Instantiate(interactionText, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2, gameObject.transform.position.z), interactionText.transform.rotation);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().isInteraction = false;
            interactionText.SetActive(false);
            interaction = false;
            collisionGameObject = null;
            //Destroy(interactionText);
        }
    }*/

    private void FixedUpdate()
    {
        
        //Debug.Log(Input.GetButton("Drag"));
        Rigidbody2D rgb = gameObject.GetComponent<Rigidbody2D>();
        if (collisionGameObject)
        {
            if (isDraggable) 
            {
                if (Input.GetButton("Drag"))
                {
                    //Rigidbody2D rgb = gameObject.GetComponent<Rigidbody2D>();
                    rgb.bodyType = RigidbodyType2D.Dynamic;

                    //gameObject.GetComponent<BoxCollider2D>().isTrigger = false;

                    //rgb.AddForce(new Vector2(collisionGameObject.GetComponent<Rigidbody2D>().velocity.x,.0f)*force,ForceMode2D.Impulse);

                    rgb.velocity = collisionGameObject.GetComponent<Rigidbody2D>().velocity.normalized*force;
                    collisionGameObject.GetComponent<Rigidbody2D>().velocity = collisionGameObject.GetComponent<Rigidbody2D>().velocity.normalized * force;
                    /*Vector2 D = collisionGameObject.transform.position - gameObject.transform.position; // line from crate to player
                    float dist = D.magnitude;
                    Vector2 pullDir = D.normalized; // short blue arrow from crate to player
                    if (dist < 50)   // lose tracking if too far
                    { // don't pull if too close
                      // this is the same math to apply fake gravity. 10 = normal gravity

                        // for fun, pull a little bit more if further away:
                        // (so, random, optional junk):
                        //float pullForDist = (dist - 3) / 2.0f;
                        //if (pullForDist > 20) pullForDist = 20;
                        //force += pullForDist;
                        // Now apply to pull force, using standard meters/sec converted
                        //    into meters/frame:
                        rgb.velocity += pullDir * force;

                    }*/
                }
                else
                {
                    rgb.velocity = new Vector2(.0f,.0f);
                    rgb.bodyType = RigidbodyType2D.Kinematic;
                }
            }
        }
        if (gameObject.GetComponent<Rigidbody2D>().velocity.x == 0 && gameObject.GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            //gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }

    }

}
