using UnityEngine;
using System.Collections;

public class enemyAI : MonoBehaviour {

	public float walkSpeed = 5.0f;
	public float attackDistance = 3.0f;
	public float attackDemage = 10.0f;
	public float attackDelay = 1.0f;
	public float hp = 100.0f;

	private float timer = 0;

	void takeHit(float damage) 
	{
		hp -= 10;
		if(hp <= 0) {
			Destroy(gameObject);
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.tag.Equals("Player")) {
			Quaternion targetRotation = Quaternion.LookRotation(other.transform.position - transform.position);
			float oryginalX = transform.rotation.x;
			float oryginalZ = transform.rotation.z;

			Quaternion finalRotation = Quaternion.Slerp(transform.rotation, targetRotation, 5.0f * Time.deltaTime);
			finalRotation.x = oryginalX;
			finalRotation.z = oryginalZ;
			transform.rotation = finalRotation;

			float distance = Vector3.Distance(transform.position, other.transform.position);
			if(distance > attackDistance) {
				transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
			} else {
				if(timer <= 0) {
					other.SendMessage("takeHit", attackDemage);
					timer = attackDelay;
				}
			}

			if(timer > 0) {
				timer -= Time.deltaTime;
			}
		}
	}
}