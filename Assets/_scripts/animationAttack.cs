using UnityEngine;
using System.Collections;

public class animationAttack : MonoBehaviour {
	public Animation attackAnim;
	// Use this for initialization
	void Start () {
		attackAnim = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			attackAnim.Play ("slice", PlayMode.StopAll);
		}
	}
}
