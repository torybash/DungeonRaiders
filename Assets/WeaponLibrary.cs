using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponLibrary : MonoBehaviour {

	[SerializeField] List<Weapon> weaponDefinitions;
	private Dictionary<string, Weapon> weaponDefinitionsDict;


	void Awake(){

		weaponDefinitionsDict = new Dictionary<string, Weapon>();
		foreach (Weapon weaponDef in weaponDefinitions) {
			weaponDefinitionsDict.Add(weaponDef.name, weaponDef);
		}
	}


	public Weapon GetWeapon(string weaponName)
	{
		if (!weaponDefinitionsDict.ContainsKey(weaponName)) Debug.LogError("GetWeaponAnimator - weaponName " + weaponName + " does not exists");
		return weaponDefinitionsDict[weaponName];
	}


}



[System.Serializable]
public class Weapon{
	public string name;
	public WeaponType weaponType;
	public int minDamage;
	public int maxDamage;
	public float size;
	public float range;
	public float critChance;
	public AnimatorOverrideController animatorController;
}