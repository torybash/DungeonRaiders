using UnityEngine;
using System.Collections;

public class HeroPanelController : MonoBehaviour {

	private PreperationController prepCtrl;

	void Awake()
	{
		prepCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<PreperationController>();
	}

	/***FROM UI***/
	public void AddHero()
	{
		prepCtrl.AddHero();
	}
}
