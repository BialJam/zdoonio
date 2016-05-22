using UnityEngine;
using System.Collections;

public class Cutscene : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if(other.tag.Equals("Player")) {
			startCutScene();
		}
	}

	void startCutScene()
	{
		GameObject camConObj = GameObject.Find ("CameraController");
//		CameraController camCon = camConObj.GetComponent<CameraController>();
	//	camCon.SelectCamera (2);
	}
}