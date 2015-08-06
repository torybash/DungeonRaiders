using UnityEngine;
using System.Collections;

public class HeroInfoPanelController : MonoBehaviour {

	private PreperationController prepCtrl;
	
	void Awake()
	{
		prepCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<PreperationController>();
	}
}
