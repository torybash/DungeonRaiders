using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameUIController : MonoBehaviour {

	//UI refs
	[SerializeField] Transform canvas;


	//Prefabs
	[SerializeField] Transform healthBarPrefab;

	//Refs
	private Dictionary<int, Transform> healthBarDict = new Dictionary<int, Transform>();

	public void UpdateHealthBar(UnitCommander unitCmd)
	{
		int instId = unitCmd.GetInstanceID();

		Transform healthBarT = null;
		if (!healthBarDict.ContainsKey(instId)){
			healthBarT = Instantiate(healthBarPrefab);
			healthBarT.SetParent(canvas);
			healthBarDict.Add(instId, healthBarT);
		}else{
			healthBarT = healthBarDict[instId];
		}

		Vector2 aboveHeadPosition = Camera.main.WorldToScreenPoint(unitCmd.transform.position);
		aboveHeadPosition.y += 25;

		healthBarT.position = aboveHeadPosition;

		float health = unitCmd.health / (float) unitCmd.maxHealth;
		healthBarT.GetComponent<HealthBar>().UpdateBar(health, HealthToColor(health));

	}


	private Color HealthToColor(float health){
		Color color = new Color();

		color.g = health;
		color.r = 1 - health;
		color.b = 0;
		color.a = 1;

		return color;
	}
}
