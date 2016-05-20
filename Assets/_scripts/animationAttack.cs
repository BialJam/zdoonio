using UnityEngine;
using System.Collections;

public class animationAttack : MonoBehaviour {
	public Animation attackAnim;
	public AudioClip oneSlice;

	private AudioSource cutAudio;
	// Use this for initialization
	void Start () {
		attackAnim = GetComponent<Animation>();
		cutAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			attackAnim.Play ("slice", PlayMode.StopAll);
			cutAudio.PlayOneShot (oneSlice, 1F);

		}
	}
}
