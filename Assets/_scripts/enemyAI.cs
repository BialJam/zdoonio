using UnityEngine;
using System.Collections;

public class enemyAI : MonoBehaviour {

	public float walkSpeed = 5.0f;
	public float attackDistance = 3.0f;
	public float attackDemage = 10.0f;
	public float attackDelay = 1.0f;
	public float hp = 50.0f;
	public Transform[] transforms;

	private float timer = 0;
	private string currentState;
	private Animator animator;
	private AnimatorStateInfo stateInfo;

	void Start () {
		animator = transforms[0].GetComponent<Animator>();
		currentState = "";
	}

	void takeHit(float damage) 
	{
		hp -= damage;
		if (hp <= 0) {
			animationSet ("Die");
		} else {
			animationSet ("Gd");
		}
	}

	void OnTriggerStay(Collider other)
	{
		if ((other.tag.Equals ("Player") || other.tag.Equals("Ally")) && hp > 0) {
			Quaternion targetRotation = Quaternion.LookRotation (other.transform.position - transform.position);
			float oryginalX = transform.rotation.x;
			float oryginalZ = transform.rotation.z;

			Quaternion finalRotation = Quaternion.Slerp (transform.rotation, targetRotation, 5.0f * Time.deltaTime);
			finalRotation.x = oryginalX;
			finalRotation.z = oryginalZ;
			transform.rotation = finalRotation;

			float distance = Vector3.Distance (transform.position, other.transform.position);
			if (distance > attackDistance && !stateInfo.IsName ("Base Layer.Gd")) {
				animationSet ("Run");
				transform.Translate (Vector3.forward * walkSpeed * Time.deltaTime);
			} else if(distance <= attackDistance) {
				if (timer <= 0) {
					animationSet ("Atack_2");
					other.SendMessage ("takeHit", attackDemage);
					timer = attackDelay;
				}
			}

			if (timer > 0) {
				timer -= Time.deltaTime;
			}
		} 
	}

	void animationSet(string action)
	{
		if (action == "Idle") {
			animator.SetBool ("Run", false);
			animator.SetBool ("Die", false);
			animator.SetBool ("Gd", false);
			animator.SetBool ("Atack_2", false);
			animator.SetBool ("Idle", true);	
		}

		if (action == "Run") {
			animator.SetBool ("Run", true);
			animator.SetBool ("Die", false);
			animator.SetBool ("Gd", false);
			animator.SetBool ("Atack_2", false);
			animator.SetBool ("Idle", false);	
		}

		if (action == "Die") {
			animator.SetBool ("Run", false);
			animator.SetBool ("Die", true);
			animator.SetBool ("Gd", false);
			animator.SetBool ("Atack_2", false);
			animator.SetBool ("Idle", false);	
		}

		if (action == "Atack_2") {
			animator.SetBool ("Run", false);
			animator.SetBool ("Die", false);
			animator.SetBool ("Gd", false);
			animator.SetBool ("Atack_2", true);
			animator.SetBool ("Idle", false);	
		}

		if (action == "Gd") {
			animator.SetBool ("Run", false);
			animator.SetBool ("Die", false);
			animator.SetBool ("Gd", true);
			animator.SetBool ("Atack_2", false);
			animator.SetBool ("Idle", false);	
		}



	}
	/*
	void OnTriggerExit(Collider other) 
	{
		if (other.tag.Equals ("Player")) {
			animationSet ("Idle");
		}
	} */
	/*
	private void animationSet(string animator)
	{
		if (!stateInfo.IsName(animator)) {
			animator.SetBool("Run", false);
			animator.SetBool("Die", false);
			animator.SetBool("Gd", false);
			animator.SetBool("Attack_2", false);
		} else {
			animator.SetBool(animator, false);
		}
	} */
	/*
	private void animationSet(string animationToPlay) 
	{
		stateInfo = animator.GetCurrentAnimatorStateInfo(0);
		animationReset();
		if (animationToPlay != "Run") {
			Debug.Log (stateInfo.IsName ("Base Layer.Gd"));
		}

		if (currentState == "") {
			currentState = animationToPlay;
			if(currentState != "Run") {
				Debug.Log (currentState);
			}
				

			if (stateInfo.IsName ("Base Layer.Run") && currentState != "Run") {
				animator.SetBool ("RunToIdle", true);
			}

			if (stateInfo.IsName ("Base Layer.Die") && currentState != "Die") {
				animator.SetBool ("DieToIdle", true);
			}

			string state = "Idle" + currentState.Substring(0, 1).ToUpper() + currentState.Substring(1);
			animator.SetBool(state, true);
			currentState = "";
		}
	}*/

}