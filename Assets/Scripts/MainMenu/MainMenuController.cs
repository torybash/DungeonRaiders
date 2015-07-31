using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {




/***From UI***/

	public void StartNewGame()
	{
		GameManager.I.StartNewGame();
	}

	public void LoadGame()
	{
		GameManager.I.LoadGame();
	}
}
