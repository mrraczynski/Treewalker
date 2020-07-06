using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingScript : MonoBehaviour
{
    //public int playerLayer = 9;
    //public float flyForce = 1f;
    public float gravScale = -0.1f;
    private float drag = 1.0f;
    public float flyDist = 10f;
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
        gameObject.GetComponent<Collider2D>().Cast(new Vector2(.0f, 1.0f), hits, flyDist, true);
        if (hits[0])
        {
            Debug.Log(hits[0].collider.tag);
            if (hits[0].collider.tag == "Player")
            {
                hits[0].collider.gameObject.GetComponent<PlayerController>().SetGrounded(false);
                hits[0].collider.gameObject.GetComponent<PlayerController>().SetDoubleJump(true);
                hits[0].collider.gameObject.GetComponent<PlayerController>().SetFlyDist(flyDist);
                hits[0].rigidbody.gravityScale = gravScale;
                hits[0].rigidbody.drag = drag;
                //hits[0].rigidbody.AddForce(new Vector2(.0f, flyForce), ForceMode2D.Force);

            }

        }


}
}
