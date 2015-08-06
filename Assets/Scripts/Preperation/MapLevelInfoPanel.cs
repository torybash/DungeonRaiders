using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapLevelInfoPanel : MonoBehaviour {

	//UI refs
	[SerializeField] Text textLevelName;
	[SerializeField] Text textLevelNumber;
	[SerializeField] Text textLevelDescription;


	//Controller ref
	private PreperationController prepCtrl;

	//Variables
	private WorldLevel currLevel;

	void Awake()
	{
		prepCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<PreperationController>();
	}

	public void InitPanel(WorldLevel worldLevel)
	{
		textLevelName.text = worldLevel.levelName;
		textLevelNumber.text = "Level: " + worldLevel.levelID + 1;
		textLevelDescription.text = worldLevel.levelDescription;

		currLevel = worldLevel;
	}



	/***FROM UI***/
	public void StartRound()
	{
		prepCtrl.StartRound(currLevel.levelID);
	}
}
