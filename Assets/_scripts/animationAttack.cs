using UnityEngine;
using System.Collections;

public class animationAttack : MonoBehaviour {
	public Animation attackAnim;
	public AudioClip oneSlice;
	public float hitDelay = 1.5f;

	private float hitDelayCounter = 0.0f;
	private AudioSource cutAudio;
	// Use this for initialization
	void Start () {
		attackAnim = GetComponent<Animation>();
		cutAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(hitDelayCounter > 0) {
			hitDelayCounter -= Time.deltaTime;
		}

		if (Input.GetButtonDown ("Fire1")) {
			attackAnim.Play ("slice", PlayMode.StopAll);
			cutAudio.PlayOneShot (oneSlice, 1F);
			hitDelayCounter = 0.0f;
		}
	}
}
