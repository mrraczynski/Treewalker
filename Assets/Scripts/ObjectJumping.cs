using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectJumping : MonoBehaviour
{
    public float jumpForce = 1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        /*ContactPoint2D[] contacts = new ContactPoint2D[collision.contactCount];
        if (contacts[0].collider.tag == "Player")
        {
            if(contacts[0].collider.bounds.)
            gameObject.GetComponent<BoxCollider2D>().bounds.center;
            
        }*/
        RaycastHit2D[] hits = new RaycastHit2D[1];
        collision.gameObject.GetComponent<Collider2D>().Raycast(new Vector2(.0f, -1.0f), hits, 1.0f);
        if (hits[0])
        {
            //Debug.Log(hits[0].collider.tag);
            if (hits[0].collider.tag == "JumpingPlatform")
            {
                collision.gameObject.GetComponent<PlayerController>().SetGrounded (false);
                //collision.gameObject.GetComponent<PlayerController>().SetDoubleJump (true);
                collision.rigidbody.AddForce(new Vector2(.0f, jumpForce), ForceMode2D.Impulse);

            }

        }
    }

}
