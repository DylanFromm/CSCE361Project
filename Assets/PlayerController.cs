using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int number;
	
	// gloabal variables
	private int frameBuffer;
	public float speed;
	public float jumpPower;
	public GameObject blast;
	int direction;
	Animator animationController;
	Vector2 input;
	Vector2 inputTemp;
	Vector2 c_stick;

	public float health = 100;

	string state;

	// flag variables
	bool jumping;
	bool canJump;
	bool attacking;
	bool blocking;
	int sp_attacking;
	bool falling;

	public bool hit = false;

	public Transform startRange, endRange;
	

	// required functions
	void Start() {
		frameBuffer = 0;
		direction = 1;
		animationController = GetComponent<Animator>();
		input = new Vector2(0f, 0f);
		inputTemp = new Vector2(0f, 0f);
		c_stick = new Vector2(0f, 0f);
		
		jumping = false;
		canJump = true;
		attacking = false;
		blocking = false;
		sp_attacking = 0;
		falling = false;


		//Instantiate(hitbox, transform.position + new Vector3(7f * direction, -3.5f, 0f), 
		        //    new Quaternion(transform.rotation.x, 1, transform.rotation.z, transform.rotation.w));
	}


			
			
	
	void Update() {
		if (health <= 0) {
			// run death script		
		}
		Debug.DrawLine (startRange.position, endRange.position, Color.red);
		state = "";
		if (frameBuffer == 0) {
			take_input();
			update_physics();
		} else {
			frameBuffer = frameBuffer - 1;
		}
		
		update_state();
		check_collision (state);
		//box.update_hitbox(input, transform, direction, hitbox);
		if (input.x == 0) {
		//	hitbox.GetComponent<hitboxController>().destroy();	
		}
	}
	
	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Stage") {
			canJump = true;
			jumping = false;
			animationController.SetBool("Landed", true);

			frameBuffer = 6;
		}

	}
	
	// helper functions
	
	/**
	 * Takes input from Xbox 360 controller
	 * Updates input vector and input button variables
	 * Requires Unity3D editor to have following Buttons enabled:
	 *		A: joystick button 16
	 *		X: joystick button 18
     *		B: joystick button 17
     *		Y: joystick button 19
     *		triggers: joystick 5th and 6th Axis
     *		movement: Horizontal and Vertical Axis (X and Y Axis)
     *		control stick: Control_X and Control_Y Axis (3rd and 4th Axis)
	 */

	void take_input() {
		input = new Vector2(Input.GetAxis("Horizontal " + number), Input.GetAxis("Vertical " + number));
		c_stick = new Vector2(Input.GetAxis("Control_X"), Input.GetAxis("Control_Y"));
		attacking = Input.GetKeyDown("joystick " + number + " button 16");
		jumping = Input.GetKeyDown("joystick " + number + " button 18") || Input.GetKeyDown("joystick " + number + " button 19");
		if (Input.GetKeyDown("joystick " + number + " button 17")) {
			animationController.SetBool("special", true);
		}
		else if (Input.GetKeyUp("joystick " + number + " button 17")) {
			frameBuffer = 30;
			animationController.SetBool("special", false);
			float rot = 0;
			if (direction == -1) {
				rot = 90;
			}
			GameObject blastObject = (GameObject)Instantiate(blast, transform.position + new Vector3(7 * direction, -3f, 0), 
			            new Quaternion(transform.rotation.x, rot, transform.rotation.z, transform.rotation.w));
			blastObject.GetComponent<Blast>().number = number;
			
		}
		//blocking = Mathf.Abs(Input.GetAxis("LT")) + Mathf.Abs(Input.GetAxis("RT"));
	}

	/*
	void take_input() {
		input = new Vector2(Input.GetAxis("Horizontal " + number), Input.GetAxis("Vertical " + number));
		c_stick = new Vector2(Input.GetAxis("Control_X"), Input.GetAxis("Control_Y"));
		attacking = Input.GetKeyDown("joystick " + number + " button 16");
		jumping = Input.GetKeyDown("joystick " + number + " button 18") || Input.GetKeyDown("joystick " + number + " button 19");
		if (Input.GetKeyDown("joystick " + number + " button 17")) {
			animationController.SetBool("special", true);
		}
		else if (Input.GetKeyUp("joystick " + number + " button 17")) {
			frameBuffer = 30;
			animationController.SetBool("special", false);
			float rot = 0;
			if (direction == -1) {
				rot = 90;
			}
			GameObject blastObject = (GameObject)Instantiate(blast, transform.position + new Vector3(7 * direction, -3f, 0), 
			            new Quaternion(transform.rotation.x, rot, transform.rotation.z, transform.rotation.w));
			blastObject.GetComponent<Blast>().number = number;
			
		}

		//blocking = Mathf.Abs(Input.GetAxis("LT")) + Mathf.Abs(Input.GetAxis("RT"));
	}
	/*
	/**
	 * Runs after take_input() and updates the GameObject's physics components
	 */
	void update_physics() {
		// vertical movement
		if (canJump && jumping) {
			jumping = true;
			canJump = false;
			rigidbody2D.AddForce(Vector2.up * 6000);
			animationController.SetBool("Landed", false);
			state += "jump ";
		}
		
		// horizontal movement
		float true_speed = speed * input.x;
		if (canJump) {
			if (attacking || sp_attacking == 2) {
				true_speed = 0;
			} else if (input.x < 0 && direction == 1) {
				flip();
			} else if (input.x > 0 && direction == -1) {
				flip();
			}
		} else if (jumping) {
			true_speed = true_speed / 2;
		}
		rigidbody2D.velocity = new Vector2(true_speed, rigidbody2D.velocity.y);
		if (true_speed != 0) {
			//hurtbox.transform.Translate(new Vector3(30 * Time.deltaTime, 0, 0));
		}
	}
	
	/**
	 * Updates the state variables 
	 * Sets the animation flags for the Animator compontent
	 */
	void update_state() {
		animationController.SetFloat ("Speed", Mathf.Abs(input.x));
		animationController.SetFloat ("Y_vel", Mathf.Abs(rigidbody2D.velocity.y));
		c_stick = new Vector3 (Input.GetAxis ("Control_X"), Input.GetAxis ("Control_Y"));
		if (frameBuffer == 0) {
			if (Input.GetKeyDown("joystick " + number + " button 16")) {
				state += "attack ";
				frameBuffer = 20;
				if (input.y > 0.5) {
					animationController.SetInteger("y_input", 1);
					animationController.SetBool("attacking", true);
					state += "up ";
				}
				else if ((input.x < -0.5 && direction == 1) || (input.x > 0.5 && direction == -1)) {
					animationController.SetBool("attacking", true);
					animationController.SetInteger("x_input", -1);
					state += "back ";
				}
				else if ((input.x > 0.5 && direction == 1) || (input.x < -0.5 && direction == -1)) {
					animationController.SetBool("attacking", true);
					animationController.SetInteger("x_input", 1);
					state += "forward";
				}
				// down state
				else {
					animationController.SetBool("attacking", true);
					animationController.SetInteger("y_input", 0);
					animationController.SetInteger("x_input", 0);
				}
			} else if (c_stick != Vector2.zero) {
				state = "attack ";
				if (c_stick.y < -0.5) {
					frameBuffer = 20;
					animationController.SetInteger("y_input", 1);
					animationController.SetBool("attacking", true);
				}
				else if ((c_stick.x < -0.5 && direction == 1) || (c_stick.x > 0.5 && direction == -1)) {
					frameBuffer = 20;
					animationController.SetBool("attacking", true);
					animationController.SetInteger("x_input", -1);
				}
				else if ((c_stick.x > 0.5 && direction == 1) || (c_stick.x < -0.5 && direction == -1)) {
					frameBuffer = 20;
					animationController.SetBool("attacking", true);
					animationController.SetInteger("x_input", 1);
				}
				
			} else if (frameBuffer == 0) {
				animationController.SetBool("attacking", false);
				animationController.SetInteger("y_input", 0); 	
				animationController.SetInteger("x_input", 0);
			}
		}
		
	}
	
	void flip() {

		direction *= -1;
		Vector3 scale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
		transform.localScale = scale;
		endRange.SendMessage("setPos", (Vector2)transform.position + new Vector2(direction*12f, 0f));
	}

	public class HitboxSpawner {
		public bool hold;
		public HitboxSpawner() {
			hold = false;
		}

		/*public void update_hitbox(Vector2 inputVar, Transform transform, int direction, GameObject hitbox) {
			if (inputVar.x != 0) {

				hitbox.SetActive(true);
				hold = true;
				Debug.Log("activate " + hitbox.tag);

			} else if (inputVar == Vector2.zero) {
				hold = false;
				hitbox.SetActive(false);
			}
		}*/

	}

	void damage(float amount) {
		health = health - amount;
		if (health < 0) {
			health = 0;
		}
	}

	void check_collision(string state_) {
		Vector2 force = Vector2.zero;
		float damage = 0;
		if (state_ != "") {
			
			//hit = Physics2D.Linecast (startRange.position, endRange.position);
			string otherNumber;
			if (number == 1) {
				otherNumber = "2";
			} else {
				otherNumber = "1";
			}
			
			if (state.Contains("attack")) {
				if (state.Contains("jump")) {
					if (state.Contains("forward")) {
						damage = 5;
						force = new Vector2(2000f, 2000f);
						Vector2 endPoint = (Vector2)transform.position +  new Vector2(-12f * -direction, -8f);
						Vector2 startPoint = (Vector2)transform.position + new Vector2(0f, -2f);
						endRange.SendMessage("setPos", endPoint);
						startRange.SendMessage("setPos", startPoint);
					} 
					else if (state.Contains("back")) {
						damage = 5;
						force = new Vector2(2000f, 2000f);
						Vector2 endPoint = (Vector2)transform.position +  new Vector2(-12f * direction, -8f);
						Vector2 startPoint = (Vector2)transform.position + new Vector2(0f, -2f);
						endRange.SendMessage("setPos", endPoint);
						startRange.SendMessage("setPos", startPoint);
					} 
					else if (state.Contains("up")) {
						damage = 0;
						force = new Vector2(5000f, 500f);
						Vector2 endPoint = (Vector2)transform.position +  new Vector2(8f * direction, 10f);
						Vector2 startPoint = (Vector2)transform.position + new Vector2(-2f, -2f);
						endRange.SendMessage("setPos", endPoint);
						startRange.SendMessage("setPos", startPoint);
					} 
					// down
					else {
						damage = 5;
						force = new Vector2(2000f, 2000f);
						Vector2 endPoint = (Vector2)transform.position +  new Vector2(8f * direction, 5f);
						Vector2 startPoint = (Vector2)transform.position + new Vector2(-2f, -2f);
						endRange.SendMessage("setPos", endPoint);
						startRange.SendMessage("setPos", startPoint);
						
					}
				} else {
					if (state.Contains("forward")) {
						damage = 15;
						force = new Vector2(2000f, 2000f);
						Vector2 endPoint = new Vector2(transform.position.x + 12f * direction, transform.position.y);
						Vector2 startPoint = transform.position;
						endRange.SendMessage("setPos", endPoint);
						startRange.SendMessage("setPos", startPoint);
					} 
					else if (state.Contains("up")) {
						damage = 0;
						force = new Vector2(0f, 0f);
						Vector2 endPoint = new Vector2(0f, 0f);
						Vector2 startPoint = new Vector2(0f, 0f);
						endRange.SendMessage("setPos", endPoint);
						startRange.SendMessage("setPos", startPoint);
					} 
					else {
						damage = 5;
						force = new Vector2(2000f, 2000f);
						Vector2 endPoint = new Vector2(transform.position.x + 12f * direction, transform.position.y);
						Vector2 startPoint = transform.position;
						endRange.SendMessage("setPos", endPoint);
						startRange.SendMessage("setPos", startPoint);
					}
				}
			}
			
			Debug.DrawLine (startRange.position, endRange.position, Color.red);
			RaycastHit2D castHit = Physics2D.Linecast (startRange.position, endRange.position, 1 << LayerMask.NameToLayer("Player 2"));
			if (castHit) {
				if (castHit.collider.gameObject != gameObject) {
					castHit.rigidbody.AddForce (force * direction);
					castHit.rigidbody.gameObject.SendMessage("damage", damage);
				}
			}
		}
	}
}