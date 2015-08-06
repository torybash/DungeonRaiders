using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	//Instance
	public static GameManager I = null;

	//Fields
	public GameStatus status; 


	//Refs
	public WeaponLibrary weaponLib;

	void Awake () 
	{
		if (I != null){ //Manager already exists! just destroy this new one
			GameObject.Destroy(this);
		}else{
			Init();
		}
	}

	private void Init()
	{
		I = this;
		DontDestroyOnLoad(gameObject);

		weaponLib = GetComponent<WeaponLibrary>();
		Strings.Init();
	}
	


	public void StartNewGame()
	{
		//TODO: Go to "story" first

		status = new GameStatus();


		StartCoroutine(LoadAsync("Preperation"));

	}

	public void LoadGame()
	{

	}


	public void StartLevel(int levelId)
	{
		status.currLevelID = levelId;
		StartCoroutine(LoadAsync("Game"));
	}




	private IEnumerator LoadAsync(string lvlName) {
		AsyncOperation async = Application.LoadLevelAsync(lvlName);
		while (!async.isDone){
			print ("loading - async.progress: " + async.progress);
			yield return null;
		}
		yield return async;
		Debug.Log("Loading complete");
	}
}
