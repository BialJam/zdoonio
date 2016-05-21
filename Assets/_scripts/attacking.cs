using UnityEngine;
using System.Collections;

public class attacking : MonoBehaviour {


	public float hitDelay = 1.5f;
	public float damage = 5.0f;


	private float hitDelayCounter = 0.0f;
	private Vector3 fwd;
	private RaycastHit hit;
	private float range = 3.0f;




	// Use this for initialization
	void Start () {



	}

	// Update is called once per frame
	void Update () {
	 
		fwd = transform.TransformDirection(Vector3.forward);

		if(hitDelayCounter > 0) {
			hitDelayCounter -= Time.deltaTime;
		}
			
		if (Input.GetButtonDown ("Fire1")) {
			  
				hitDelayCounter = hitDelay;
				if (Physics.Raycast (transform.position, fwd, out hit)) {
					if (hit.transform.tag == "Enemy" && hit.distance < range) {
						Debug.Log ("Trafiony przeciwnik");
					hit.transform.gameObject.SendMessage ("takeHit", damage);

					} else if (hit.distance < range) {
						Debug.Log ("Trafiona Sciana");
					}
				}
			}
		hitDelayCounter = 0.0f;
		}
	}
