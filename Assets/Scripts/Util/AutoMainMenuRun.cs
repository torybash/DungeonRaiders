using UnityEngine;
using System.Collections;

public class AutoMainMenuRun : MonoBehaviour {

	void Awake () {
		if (GameManager.I == null) Application.LoadLevel("MainMenU");
	}

}
