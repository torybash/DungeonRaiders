using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public MapController mapCtrl;
	public WispController wispCtrl;
	public UnitController unitCtrl;
	public CameraController camCtrl;
	public GameUIController gameUICtrl;
	public SpriteLibrary spLib;

	public GameState gameState;


	void Awake(){
		mapCtrl = GetComponent<MapController>();
		wispCtrl = GetComponent<WispController>();
		unitCtrl = GetComponent<UnitController>();
		camCtrl = GetComponent<CameraController>();
		gameUICtrl = GetComponent<GameUIController>();
		spLib = GetComponent<SpriteLibrary>();
	}


	void Start () 
	{
		InitRound();
	
	}



	private void InitRound()
	{
		//Get map 
		mapCtrl.StartMap(GameManager.I.status.mapId);
	}


	/*********************/
	/***** GAME LOOP *****/
	/*********************/

	void Update()
	{
		switch (gameState) {
		case GameState.RUNNING:
			wispCtrl.GUpdate();
			camCtrl.GUpdate();
			break;
			
		case GameState.ROUND_ENDED:
			
			break;
		}
	}

	void FixedUpdate()
	{
		switch (gameState) {
		case GameState.RUNNING:
			unitCtrl.GFixedUpdate();
			camCtrl.GFixedUpdate();
			break;

		case GameState.ROUND_ENDED:

			break;
		}

	}
	
}


public enum GameState{
	RUNNING,
	ROUND_ENDED
}