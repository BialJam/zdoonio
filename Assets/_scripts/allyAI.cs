using UnityEngine;
using System.Collections;

public class allyAI : MonoBehaviour {

	public float walkSpeed = 5.0f;
	public float attackDistance = 3.0f;
	public float attackDemage = 10.0f;
	public float attackDelay = 1.0f;
	public float hp = 50.0f;
	public GameObject animo;

	private float timer = 0;
	private string currentState;
	private Animator animator;
	private AnimatorStateInfo stateInfo;

	void Start () {
		currentState = "";
	}

	void takeHit(float damage) 
	{
		hp -= damage;
		if (hp <= 0) {
			animo.GetComponent<Animation> ().Play ("die");
		} else {
			animo.GetComponent<Animation> ().Play ("resist");
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals ("Enemy") && hp > 0) {
			Quaternion targetRotation = Quaternion.LookRotation (other.transform.position - transform.position);
			float oryginalX = transform.rotation.x;
			float oryginalZ = transform.rotation.z;

			Quaternion finalRotation = Quaternion.Slerp (transform.rotation, targetRotation, 5.0f * Time.deltaTime);
			finalRotation.x = oryginalX;
			finalRotation.z = oryginalZ;
			transform.rotation = finalRotation;

			float distance = Vector3.Distance (transform.position, other.transform.position);
			if (distance > attackDistance && !stateInfo.IsName ("Base Layer.resist")) {
				animo.GetComponent<Animation> ().Play ("run");
				transform.Translate (Vector3.forward * walkSpeed * Time.deltaTime);
			} else if(distance <= attackDistance) {
				if (timer <= 0) {
					animo.GetComponent<Animation> ().Play ("attack");
					other.SendMessage ("takeHit", attackDemage);
					timer = attackDelay;
				}
			}

			if (timer > 0) {
				timer -= Time.deltaTime;
			}
		} 
	}

}