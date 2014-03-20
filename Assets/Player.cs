using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed;
	public float jump_power;

	bool can_jump = true;
	Animator anim;
	float h_axis;
	float v_axis;
	int frame_cooldown = 0;
	int direction = 1;
	public GameObject blast;
	Vector2 c_stick;
	float trigger = -2;
	bool can_dodge = true;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (frame_cooldown == 0) {
			take_input ();
			move ();
			if ((Input.GetButtonDown("Jump")) && can_jump == true) {
				can_jump = false;
				anim.SetBool("Landed", false);
				rigidbody2D.AddForce(Vector2.up * 6000);
			}
		} else {
			frame_cooldown--;
		}
		anim.SetFloat ("Speed", Mathf.Abs(h_axis));
		anim.SetFloat ("Y_vel", Mathf.Abs(rigidbody2D.velocity.y));
	}

	void take_input() {
		/*trigger = Mathf.Abs(Input.GetAxis ("LT")) + Mathf.Input.GetAxis ("RT");
		Debug.Log (trigger);
		if (trigger > -2 && can_dodge) {
			anim.SetBool("Dodge", true);
			rigidbody2D.velocity = new Vector2(0, 0);
			h_axis = 0;
			frame_cooldown = 10;
			can_dodge = false;
		} else if (trigger > -2) {
			rigidbody2D.velocity = new Vector2(0, 0);
			h_axis = 0;
		} else {
			anim.SetBool("Dodge", false);
		}*/
		c_stick = new Vector3 (Input.GetAxis ("Control_X"), Input.GetAxis ("Control_Y"));
		//check for horizontal movement
		if (Input.GetButtonDown ("B")) {
			anim.SetBool("special", true);
		} else if (Input.GetButton("B")) {
			rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y / 2);
			h_axis = 0;
		}else if (Input.GetButtonUp("B")) {
			frame_cooldown = 30;
			anim.SetBool("special", false);
			float rot = 0;
			if (direction == -1) 
				rot = 90;
			Instantiate(blast, transform.position + new Vector3(7f * direction, -3.5f, 0f), 
			            new Quaternion(transform.rotation.x, rot, transform.rotation.z, transform.rotation.w));
		} else if (Input.GetButtonDown("X")) {
			frame_cooldown = 20;
			if (v_axis > 0.5) {
				anim.SetInteger("y_input", 1);
				anim.SetBool("attacking", true);
			}
			else if ((h_axis < -0.5 && direction == 1) || (h_axis > 0.5 && direction == -1)) {
				anim.SetBool("attacking", true);
				anim.SetInteger("x_input", -1);
			}
			else if ((h_axis > 0.5 && direction == 1) || (h_axis < -0.5 && direction == -1)) {
				anim.SetBool("attacking", true);
				anim.SetInteger("x_input", 1);
			}
			else {
				anim.SetBool("attacking", true);
				anim.SetInteger("y_input", 0);
				anim.SetInteger("x_input", 0);
			}

		
		} else if (c_stick != Vector2.zero) 
		{
			if (c_stick.y < -0.5) {
				frame_cooldown = 20;
				anim.SetInteger("y_input", 1);
				anim.SetBool("attacking", true);
			}
			else if ((c_stick.x < -0.5 && direction == 1) || (c_stick.x > 0.5 && direction == -1)) {
				frame_cooldown = 20;
				anim.SetBool("attacking", true);
				anim.SetInteger("x_input", -1);
			}
			else if ((c_stick.x > 0.5 && direction == 1) || (c_stick.x < -0.5 && direction == -1)) {
				frame_cooldown = 20;
				anim.SetBool("attacking", true);
				anim.SetInteger("x_input", 1);
			}

		} else if (frame_cooldown == 0) {
			anim.SetBool("attacking", false);
			anim.SetInteger("y_input", 0); 	
			anim.SetInteger("x_input", 0);
			h_axis = Input.GetAxis ("Horizontal");
			v_axis = Input.GetAxis ("Vertical");
		}
	}

	void move() {
		if (can_jump) {
			if (h_axis > 0 && direction == -1) {
				flip ();
			} else if (h_axis < 0 && direction == 1) {
				flip ();
			}
		}
		float h_vel = h_axis * 75;
		if (!can_jump) {
			h_vel /= 2;
		}
		rigidbody2D.velocity = new Vector2(h_vel, rigidbody2D.velocity.y);
	}

	void flip() {
		direction *= -1;
		Vector3 scale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
		transform.localScale = scale;
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Stage") {
			can_jump = true;
			can_dodge = true;
			anim.SetBool("Landed", true);
			frame_cooldown = 6;
		}
		if (col.gameObject.tag == "Blast") {
			rigidbody2D.AddForce(Vector2.up * 9000);

		}
	}
}
