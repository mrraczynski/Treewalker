using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float maxSpeed = 6f;
	public float jumpForce = 30f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	//public float verticalSpeed = 20;
	[HideInInspector]
	public bool lookingRight = true;
	private bool doubleJump = false;
	public GameObject Boost;
	public GameObject Cloud;
	public float boostingTime = 10;
	public float boostingCoefficient = 2;
	public bool isInteraction = false;
	public GameObject checkpointText;
	[Range(0, .3f)] [SerializeField] private float smooth = .05f;
	[Header("Enemy Interaction")]
	public SpriteRenderer playerSprite;
	public float strikeForce = 10f;
	public float flashingTime = 0.08f;
	private bool strike = false;
	private bool flashing = false;
	private float strikeVelocity = 0;
	private float xVector = 0;
	[Header("Health Interaction")]
	public int health = 3;
	private int startHealth;

	private float oldGravScale;
	private float oldDrag;
	private float oldMass;
	private float oldAngDrag;
	private float flyDist = 10;
	private Vector3 vel = Vector3.zero;
	private float hor;
	//public GameObject camera;
	private Vector3 chekpoint;

	private Animator cloudanim;
	


	private int score;


	private Rigidbody2D rb2d;
	private Animator anim;
	private bool isGrounded = false;
	private bool isFlying = false;

	//функция возвращения к последнему чекпоинту
	public void OnLatestCheckpoint()
    {
		gameObject.transform.position = chekpoint;
    }

    public int GetScore()
    {
		return score;
    }
	public int GetHealth()
	{
		return health;
	}
	public void GravityModify(float gravScale, float drag)
    {
		gameObject.GetComponent<PlayerController>().SetGrounded(false);
		//hits[0].collider.gameObject.GetComponent<PlayerController>().SetDoubleJump(true);
		gameObject.GetComponent<PlayerController>().SetFlyDist(flyDist);
		//playerHit = true;
		//Debug.Log(hits[0].collider.tag);

		gameObject.GetComponent<Rigidbody2D>().gravityScale = gravScale;
		gameObject.GetComponent<Rigidbody2D>().drag = drag;
	}

	public void SetFlyDist(float x)
    {
		flyDist = x;
    }

	public void SetGrounded (bool x)
    {
		isGrounded = x;
    }

	public void SetDoubleJump(bool x)
	{
		doubleJump = x;
	}

	public void AddScore()
	{
		score++;
	}


	// Use this for initialization
	void Start () {
		checkpointText.SetActive(false);
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		oldMass = gameObject.GetComponent<Rigidbody2D>().mass;
		oldGravScale = gameObject.GetComponent<Rigidbody2D>().gravityScale;
		oldDrag = gameObject.GetComponent<Rigidbody2D>().drag;
		oldAngDrag = gameObject.GetComponent<Rigidbody2D>().angularDrag;
		//cloudanim = GetComponent<Animator>();
		chekpoint = gameObject.transform.position;
		Cloud = GameObject.Find("Cloud");
		startHealth = health;
		//cloudanim = GameObject.Find("Cloud(Clone)").GetComponent<Animator>();
		//rb2d.gravityScale = -2;
		//rb2d.drag = 1;
	}


	void OnCollisionEnter2D(Collision2D collision2D) {
		
		/*if (collision2D.relativeVelocity.magnitude > 20){
			Boost = Instantiate(Resources.Load("Prefabs/Cloud"), transform.position, transform.rotation) as GameObject;
		//	cloudanim.Play("cloud");	

		}*/

		if(collision2D.gameObject.CompareTag("MovingPlatform"))
        {
			transform.parent = collision2D.transform;
        }

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.CompareTag("Checkpoint"))
		{
			chekpoint = gameObject.transform.position;
			Destroy(collision.gameObject);
			StartCoroutine(CheckpointDisplay());
		}
		if (collision.gameObject.CompareTag("Enemy"))
		{
			if(health > 1)
            {
				health--;
            }
            else
            {
				health = startHealth;
				OnLatestCheckpoint();
				return;
			}
			strike = true;
			strikeVelocity = strikeForce;
			//rb2d.AddForce(new Vector2(-1f, 0) * jumpForce);
			//rb2d.velocity = Vector2.up * strikeForce;
			if(gameObject.transform.position.x <= collision.transform.position.x)
            {
				xVector = -1;
			}
			else
            {
				xVector = 1;
			}
		}
	}

    private void OnCollisionExit2D(Collision2D collision)
    {
		if (collision.gameObject.CompareTag("MovingPlatform"))
		{
			transform.parent = null;
		}
	}



    // Update is called once per frame
    void Update () {
		//Debug.Log(chekpoint);
		//пока отключим двойной прыжок
		
		//Debug.Log(gameObject.GetComponent<Rigidbody2D>().gravityScale);
		if (Input.GetKeyDown(KeyCode.B))
        {
			StartCoroutine(PlayerBoosting());
        }		
	

		isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.05F, whatIsGround);
		if (Input.GetButtonDown("Jump") && (isGrounded || !doubleJump))
		{
			rb2d.velocity = Vector2.up * jumpForce;
			//rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
			if (!doubleJump && !isGrounded)
			{
				doubleJump = true;
				Boost = Instantiate(Resources.Load("Prefabs/Cloud"), transform.position, transform.rotation) as GameObject;
				//	cloudanim.Play("cloud");		
			}
		}
		//Debug.Log(isGrounded);
		hor = Input.GetAxis("Horizontal");
		if (isGrounded)
			doubleJump = false;
	}


	void FixedUpdate()
	{
		RaycastHit2D[] hits = new RaycastHit2D[1];
		gameObject.GetComponent<Collider2D>().Raycast(Vector2.down, hits, flyDist);
		if (!strike)
		{
			rb2d.velocity = new Vector2(hor * maxSpeed, rb2d.velocity.y);
		}
		//если столкнулись с врагом - отлетаем
		if (strike)
		{
			//запустим мигание спрайта
			if (!flashing)
			{
				flashing = true;
				StartCoroutine(SpriteFlashing());
			}
			//оттолкнём игрока
			rb2d.velocity = new Vector2(xVector * strikeVelocity, rb2d.velocity.y);
			if (strikeVelocity == 0f)
            {
				strike = false;
			}
			strikeVelocity = strikeVelocity - 1f;
			//rb2d.AddForce(new Vector2(-1f, 0) * jumpForce);
			//rb2d.velocity = new Vector2(-1 * strikeForce, strikeForce);
			//StartCoroutine(StrikeForceDuration());
		}
		if (hits[0].collider)
		{

			//Debug.Log("here");
			if (hits[0].collider.tag != "FlyingPlatform")
			{
				gameObject.GetComponent<Rigidbody2D>().gravityScale = oldGravScale;
				gameObject.GetComponent<Rigidbody2D>().drag = oldDrag;
				flyDist = 10;
				
			}
	
		}
		else
		{
			gameObject.GetComponent<Rigidbody2D>().gravityScale = oldGravScale;
			gameObject.GetComponent<Rigidbody2D>().drag = oldDrag;
			flyDist = 10;
		}

		/*if (Input.GetButtonDown("Vertical") && !isGrounded)
		{
			rb2d.AddForce(new Vector2(0, -jumpForce));
			Boost = Instantiate(Resources.Load("Prefabs/Cloud"), transform.position, transform.rotation) as GameObject;
			//cloudanim.Play("cloud");
		}*/

		//anim.SetFloat ("Speed", Mathf.Abs (hor));

		//Vector3 targetVelocity = new Vector2(hor * maxSpeed, rb2d.velocity.y);
		// And then smoothing it out and applying it to the character
		//rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, targetVelocity, ref vel, smooth);
		

		//anim.SetBool ("IsGrounded", isGrounded);

		if ((hor > 0 && !lookingRight)||(hor < 0 && lookingRight))
			Flip ();
		 
		//anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
	}


	
	public void Flip()
	{
		lookingRight = !lookingRight;
		Vector3 myScale = transform.localScale;
		myScale.x *= -1;
		transform.localScale = myScale;
	}

	IEnumerator PlayerBoosting ()
    {
		float oldMaxSpeed = maxSpeed;
		float oldJumpForce = jumpForce;

		maxSpeed = maxSpeed * boostingCoefficient;
		jumpForce = jumpForce * boostingCoefficient;

		yield return new WaitForSeconds(boostingTime);

		maxSpeed = oldMaxSpeed;
		jumpForce = oldJumpForce;
		//gameObject.GetComponentInChildren<SpriteRenderer>().material.SetColor("_Color", new Color(0.5f, 0.5f, 0.5f));
		//Debug.Log(gameObject.GetComponentInChildren<SpriteRenderer>().material.color.r);
	}

	IEnumerator CheckpointDisplay()
	{
		checkpointText.SetActive(true);
		yield return new WaitForSeconds(3);
		checkpointText.SetActive(false);
	}
	//длительность неконтролируемого отлёта после удара
	IEnumerator StrikeForceDuration()
	{
		yield return new WaitForSeconds(.3f);
		strike = false;
	}

	//мигание спрайта при столкновении с врагом
	IEnumerator SpriteFlashing()
	{
		while(strike)
		{
			playerSprite.enabled = !playerSprite.enabled;
			yield return new WaitForSeconds(flashingTime);
		}
		playerSprite.enabled = true;
		flashing = false;
	}

}
