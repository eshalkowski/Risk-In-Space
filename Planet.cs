using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {
	public GameObject[] connectedPlanets;
	public int numOfUnits{get;set;}
	public GameObject owningPlayer{get;set;}	
	public GameObject solarSystem;

	void Start () {
		owningPlayer = null;
		numOfUnits =0;
	
		for(int i =0; i < connectedPlanets.Length; i++)
		{
			Debug.DrawLine(this.transform.position,connectedPlanets[i].transform.position,Color.red,Mathf.Infinity);
		}
	}
	void Update()
	{
		//Needs to be moved to more efficent spot
		if(owningPlayer !=null)
			renderer.material.color = owningPlayer.GetComponent<PlayerScript>().color;
		
	}
	
	public bool isOwned(){
		return owningPlayer != null;	
	}
	
	public void updateHalo(){
		
	}
}
