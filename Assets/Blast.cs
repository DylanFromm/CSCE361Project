using UnityEngine;
using System.Collections;

public class Blast : MonoBehaviour {

	float time = 0;
	int direction = 1;

	public int number;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.rotation.y > 0)
			direction = -1;
		transform.position = new Vector3 (direction * 100 * Time.deltaTime + transform.position.x, transform.position.y, transform.position.z);
		time += Time.deltaTime;
		if (time > 1) {
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.name.Contains("Player")) {
			if (number != PlayerController.number) {
				col.gameObject.SendMessage("damage", 5);
				col.gameObject.rigidbody2D.AddForce (5000 * Vector2.right * direction);
				Destroy (gameObject);
			}
		} else {
			Destroy (gameObject);
		}
	}

}
