using UnityEngine;
using System.Collections;

public class applicationManager : MonoBehaviour {



	public void Start()
	{
		
	}


	public void Quit () {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
		

	public void NewGame () {
		Application.LoadLevel(1);
	}

}
