using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

	[SerializeField] Image healthBarFill;

	public void UpdateBar(float fill, Color color){
		healthBarFill.fillAmount = fill; 
		healthBarFill.color = color;
	}
}
