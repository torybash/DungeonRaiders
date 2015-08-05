using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MapIndicator : MonoBehaviour {

	public List<MapIndicator> indicatorList;


	public List<GameObject> lineObjs = new List<GameObject>();

	//Refs
	[SerializeField] Image hoverSpriteRenderer;

	//Project assets
	[SerializeField] Sprite openLevelSprite;
	[SerializeField] Sprite completedLevelSprite;



	private WorldMapController worldMapCtrl;


	public void Init(WorldMapController worldMapCtrl){
		//Hide
		GetComponent<Image>().enabled = false;
		hoverSpriteRenderer.enabled = false;

		this.worldMapCtrl = worldMapCtrl;

	}

	public void SetCompleted()
	{

		GetComponent<Image>().enabled = true;
		GetComponent<Image>().sprite = completedLevelSprite;


		foreach (var item in lineObjs) GameObject.DestroyImmediate(item);
		lineObjs.Clear();

		foreach (var other in indicatorList) {
			other.SetOpen();

			GameObject lineObj = Instantiate((GameObject)Resources.Load("MapPathPrefab"));
			
			lineObj.GetComponent<LineRenderer>().SetVertexCount(2);
			lineObj.GetComponent<LineRenderer>().SetPosition(0, transform.position);
			lineObj.GetComponent<LineRenderer>().SetPosition(1, other.transform.position);
			
			lineObjs.Add(lineObj);
		}

	}

	public void SetOpen(){
		GetComponent<Image>().enabled = true;
		GetComponent<Image>().sprite = openLevelSprite;
	}





	/***FROM EVENT TRIGGERS***/
	public void OnPointerEnter(){
		hoverSpriteRenderer.enabled = true;
	}

	public void OnPointerExit(){
		hoverSpriteRenderer.enabled = false;
	}

	public void OnClicked(){
		worldMapCtrl.IndicatorClicked(this);
	}
}
