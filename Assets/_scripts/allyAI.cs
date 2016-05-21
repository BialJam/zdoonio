using UnityEngine;
using System.Collections;

public class allyAI : MonoBehaviour {

	public float walkSpeed = 5.0f;
	public float attackDistance = 3.0f;
	public float attackDemage = 10.0f;
	public float attackDelay = 2.0f;
	public float hp = 50.0f;
	public GameObject animo;
	public GameObject zombie;
	public AudioClip oneSlice;

	private float timer = 0;
	private string currentState;
	private Animator animator;
	private AnimatorStateInfo stateInfo;
	private bool allyDie;
	private float destroyWaitCounter;
	private AudioSource cutAudio;

	void Start () {
		currentState = "";
		destroyWaitCounter = 0.0f;
		allyDie = false;
		cutAudio = GetComponent<AudioSource>();
	}

	void Update (){
		if (allyDie == true) {
			destroyWaitCounter += Time.deltaTime;
			if (destroyWaitCounter >= 5.0f) {
				Instantiate (zombie, transform.position, transform.rotation);
				Destroy (gameObject);
			}
		}
	
	}
		


	void takeHit(float damage) 
	{
		hp -= damage;
		if (hp <= 0) {
			animo.GetComponent<Animation> ().Play ("die");
			allyDie = true;
		} else {
			animo.GetComponent<Animation> ().Play ("resist");
			cutAudio.PlayOneShot (oneSlice, 1F);
		}
	}


	void OnTriggerStay(Collider other)
	{
		//Debug.Log ("Przeciwnik wszedł w pole");
		if (other.tag == "Enemy" && hp > 0) {
			//Debug.Log ("Przeciwnik wszedł w pole1");
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

