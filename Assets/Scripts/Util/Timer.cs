using UnityEngine;
using System;
using System.Collections;

public class Timer : MonoBehaviour {
	// EXAMPLE: Timer.ExecuteNextFrame(() => { folder.rotation = Quaternion.Euler(0,0,45);});
	
	//private static Timer i;
	private static Timer instance;
	private static Timer i
	{
		get
		{
			if(instance != null)
			{
				return instance;
			}
			
			var t = Transform.FindObjectOfType<Timer>();
			
			if (t != null)
			{
				return t;
			}
			
			var go = new GameObject("_Timer", typeof(Timer));
			return go.GetComponent<Timer>();
		}
		set 
		{ 
			instance = value;
		}
	}
	
	public static void DelayedExecute(Action function, float time)
	{
		i.DoDelayedExecuteCoroutine(function, time);
	}
	
	public static void ExecuteNextFrame(Action function)
	{
		i.DoExecuteNextFrameCoroutine(function);
	}
	
	void Awake() {
		i = this;
	}
	
	public void DoDelayedExecuteCoroutine(Action function, float time) {
		StartCoroutine(DelayedExecuteCoroutine(function,time));
	}
	
	public IEnumerator DelayedExecuteCoroutine(Action function, float time) {
		yield return new WaitForSeconds(time);
		function();
	}
	
	public void DoExecuteNextFrameCoroutine(Action function) {
		StartCoroutine(ExecuteNextFrameCoroutine(function));
	}
	
	public IEnumerator ExecuteNextFrameCoroutine(Action function) {
		yield return new WaitForEndOfFrame();
		function();
	}
	
}
