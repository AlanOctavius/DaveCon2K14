using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour {
	 

	[HideInInspector]
	public bool jump = false;

	public float moveForce = 365f;			
	public float maxSpeed = 5f;			
	public float jumpForce = 1000f;			
	
	private List<Transform> groundDetectorList;		
	private bool grounded = false;


	public float h;
	// Use this for initialization
	void Start () {
	
	}


	void Awake()
	{
		// Setting up references.
		groundDetectorList = new List<Transform>();
		groundDetectorList.Add(transform.Find("groundedLeft"));
		groundDetectorList.Add(transform.Find("groundedMiddle"));
		groundDetectorList.Add(transform.Find("groundedRight"));
	}

	// Update is called once per frame
	void Update () {
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = false;
		foreach (Transform groundCheck in groundDetectorList){
			grounded = grounded || Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		}
		
		// If the jump button is pressed and the player is grounded then the player should jump.
		if(Input.GetButtonDown("Jump") && grounded)
			jump = true;
	}
	void FixedUpdate ()
	{
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");
		
		if(h * rigidbody2D.velocity.x < maxSpeed) 			
			rigidbody2D.AddForce(Vector2.right * h * moveForce); 		 		
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		
		// If the player should jump...
		if(jump)
		{	
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
	}
}
