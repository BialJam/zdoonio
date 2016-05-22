using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {

	private Camera[] cameras;

	void Start()
	{
		cameras = new Camera[Camera.allCameras.Length];
		int i = 0;
		foreach(Camera c in Camera.allCameras) {
			cameras[i] = c;
			i++;
		}
		SelectCamera (0);
	}

	public void SelectCamera(int index) 
	{
		for (int i = 0 ; i < cameras.Length ; i++) { 
			if (i == index) {
				cameras [i].enabled = true;
			} else { 
				cameras [i].enabled = false; 
			}
		}

	}

}