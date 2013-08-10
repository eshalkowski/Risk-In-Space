using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SolarSystem : MonoBehaviour {
	public int state {get; set;}
	public List<Planet> planets;
	public int numOfBonusTroops{get;set;}
	public int playerControlledPlanets{get;set;}
	public int playerTurn{get;set;}
	public GameObject curPlayer{get;set;}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	} 
	// NEED TO FIGURE OUT HOW TO INSTANTIATE PLANETS TO CREATE SOLAR SYSTEMS OR
	//ADD EXISTING PLANETS TO THE CLOSEST SOLAR SYSTEM PLANE
	public int calculateOwnedPlanets()
	{
		int controledPlanets = 0;
		for(int i = 0; i < planets.Count; i++)
		{
			if(planets[i].isOwned() && planets[i].owningPlayer.name == curPlayer.name)
			{
				controledPlanets++;
			}
		}
		return controledPlanets;
	}
}
