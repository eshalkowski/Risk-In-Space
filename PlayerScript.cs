using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {
	public List<GameObject> Players;
	private List<GameObject> ownedPlanets;
	private List<GameObject> systems;
	public int state{get;set;}
	public int numOfUnitsToDeploy{get;set;}
	public bool isPlayer{get; set;}
	public bool displayInfo{get; set;}
	public Color color{get; set;}
	//public bool hasBattled{get; set;}
	
	void OnGUI()
	{
		if(displayInfo ==true)
		{
			GUI.BeginGroup(new Rect(Screen.width/2-475,Screen.height/2-150,500,500));
			GUI.Label(new Rect(0,10,500,20), this.gameObject.name);
			GUI.Label(new Rect(0,25,500,20), "Planets Owned: " + this.ownedPlanets.Count);
			GUI.Label(new Rect(0,40,500,20), "Rienforcements: " + this.numOfUnitsToDeploy);
			GUI.EndGroup();
		}
	}
	void Awake()
	{
		DontDestroyOnLoad(this);
		Players = new List<GameObject>();
	}
	
	// Use this for initialization
	void Start () {
		ownedPlanets = new List<GameObject>();
		systems = new List<GameObject>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void addPlanet(GameObject planet, GameObject player)
	{
		ownedPlanets.Add(planet);
		planet.GetComponent<Planet>().owningPlayer = player; 
					planet.GetComponent<Planet>().updateHalo();
	}
	
	public void removePlanet(GameObject planet)
	{
		ownedPlanets.Remove(planet);
	}
	public void addSystem(GameObject solarSystem)
	{
		systems.Add(solarSystem);
	}
	public void removeSystem(GameObject solarSystem)
	{
		ownedPlanets.Remove(solarSystem);
	}
	public int calculateUnits()
	{
		numOfUnitsToDeploy=0;
		if(ownedPlanets.Count >9)
		{
			numOfUnitsToDeploy = ownedPlanets.Count/3;
		}
		else
		{
			numOfUnitsToDeploy = 3;
		}
		for(int i = 0; i < systems.Count;i++)
		{
			numOfUnitsToDeploy+= systems[i].GetComponent<SolarSystem>().numOfBonusTroops;
		}
		return numOfUnitsToDeploy;
	}
}
