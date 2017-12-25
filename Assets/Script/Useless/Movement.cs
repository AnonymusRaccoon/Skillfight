using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	private Rigidbody rb;
	public int speed;
	public int groundspeed = 60;
	public int airspeed = 40;
	public float maxSpeed = 25;
	public bool IsGrounded = false;
	public float jumpSpeed = 10;
	public float FallSpeed = 7;
	private float jumpLimit = 0;


	void Start () {

		rb = GetComponent<Rigidbody> ();

	}

	void OnCollisionStay (Collision collisionInfo) {

		IsGrounded = true;
		speed = groundspeed;

	}

	void OnCollisionExit (Collision collisionInfo) {

		IsGrounded = false;
		speed = airspeed;
		StartCoroutine (JumpFall ());

	}

	public virtual bool jump {

		get {
			return Input.GetButton ("Jump");
		}

	}

	public virtual float horizontal {

		get {
			return Input.GetAxis ("Horizontal");
		}

	}

	public virtual float vertical {

		get {
			return Input.GetAxis ("Vertical");
		}

	}

	void FixedUpdate () {

		rb.AddForce (transform.forward * vertical * speed);
		rb.AddForce (transform.right * horizontal * speed);
		if (rb.velocity.magnitude > maxSpeed) {
			
			rb.velocity = rb.velocity.normalized * maxSpeed;

		} if (jumpLimit < 10)
			jumpLimit++;

		if (jump && IsGrounded && jumpLimit >= 10) {

			rb.AddForce (Vector3.up * jumpSpeed, ForceMode.Impulse);
			jumpLimit = 0;
			StartCoroutine(JumpFall());

		}
	}
	
	private IEnumerator JumpFall () {
	
		yield return new WaitForSeconds (0.2f);
		rb.velocity = new Vector3 (rb.velocity.x, rb.velocity.y, 0);
		yield return new WaitForSeconds (0.05f);
		rb.AddForce (-Vector3.up * FallSpeed, ForceMode.Acceleration);
		
	}
}