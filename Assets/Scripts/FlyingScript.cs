using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingScript : MonoBehaviour
{
    //public int playerLayer = 9;
    public float gravScale = -0.1f;
    public float drag = 1.0f;
    public float flyDist = 10f;
    public bool force = false;
    public bool forceMod = false;
    public float flyForce = 10f;
    //private bool playerHit = false;

    // Update is called once per frame
    void Update()
    {


        /*ContactPoint2D[] contacts = new ContactPoint2D[collision.contactCount];
        if (contacts[0].collider.tag == "Player")
        {
            if(contacts[0].collider.bounds.)
            gameObject.GetComponent<BoxCollider2D>().bounds.center;
            
        }*/
        RaycastHit2D[] hits = new RaycastHit2D[1];
        gameObject.GetComponent<Collider2D>().Cast(transform.up//new Vector2(.0f, 1.0f) 
            , hits, flyDist, true);
        //Debug.Log(hits[0].transform.tag);
        if (hits[0])
        {

            if (hits[0].collider.tag == "Player")
            {
                if (!force)
                {

                    /*hits[0].collider.gameObject.GetComponent<PlayerController>().SetGrounded(false);
                    //hits[0].collider.gameObject.GetComponent<PlayerController>().SetDoubleJump(true);
                    hits[0].collider.gameObject.GetComponent<PlayerController>().SetFlyDist(flyDist);
                    //playerHit = true;
                    //Debug.Log(hits[0].collider.tag);

                    hits[0].rigidbody.gravityScale = gravScale;
                    hits[0].rigidbody.drag = drag;*/
                    hits[0].collider.gameObject.GetComponent<PlayerController>().GravityModify(gravScale,drag);
                }
                else
                {
                    if (forceMod)
                    {
                        hits[0].rigidbody.AddForce(transform.up * flyForce, ForceMode2D.Force);
                    }
                    else
                    {
                        hits[0].rigidbody.AddForce(transform.up * flyForce, ForceMode2D.Impulse);
                    }
                }

            }

        }


    }

}