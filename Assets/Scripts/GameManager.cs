using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	//Instance
	public static GameManager I = null;

	//Fields
	public GameStatus status; 


	void Awake () 
	{
		if (I != null){ //Manager already exists! just destroy this new one
			GameObject.Destroy(this);
		}else{
			I = this;
			DontDestroyOnLoad(gameObject);
		}
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
