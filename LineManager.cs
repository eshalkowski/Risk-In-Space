using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineManager : MonoBehaviour {
	public List<SolarSystem> solarSystems;
	public GameObject LinePrefab;
	public List<GameObject> lines;
  	public Vector3 coordinates;
	private bool initialization;
	private Color fromPlanetColor;
	private Color toPlanetColor;
	private bool lineFound = false;
	private float timer =0;
	// Use this for initialization
	void Start () {
		lines = new List<GameObject>();
		createLines();
						
				
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void updateColor(GameObject fromPlanet)
	{
		Planet planetScript = fromPlanet.GetComponent<Planet>();
		bool found =false;
		GameObject lineToChange = null;
		for(int x =0; x < planetScript.connectedPlanets.Count;x++)
		{
			Vector3 coordinates = fromPlanet.GetComponent<Planet>().getCoordinates()
				+ planetScript.connectedPlanets[x].GetComponent<Planet>().getCoordinates();
			for(int i =0; i <lines.Count && !found; i++)
			{
				if(lines[i].name == ""+coordinates)
				{
					lineToChange = lines[i];
					found =true;
				}
			}
			lineToChange.GetComponent<LineRenderer>().SetPosition(0,
				fromPlanet.GetComponent<Planet>().getCoordinates());
		
			lineToChange.GetComponent<LineRenderer>().SetPosition(1,
				planetScript.connectedPlanets[x].GetComponent<Planet>().getCoordinates());
			
			lineToChange.GetComponent<LineRenderer>().SetColors(planetScript.halo.color,
				planetScript.connectedPlanets[x].GetComponent<Planet>().halo.color);
			found = false;
			
		}
	}
	public void removeLines()
	{
		for(int i =0; i< lines.Count;i++)
		{
			GameObject.Destroy(lines[i]);
		}		
		lines.Clear();
	}
	
	public void createLines()
	{
		
		for(int x=0; x<solarSystems.Count;x++)
		{
			for(int i =0; i< solarSystems[x].planets.Count; i++)
			{
				for(int y =0; y < solarSystems[x].planets[i].connectedPlanets.Count; y++)
				{
					//<HERE> find a way to check for the same line twice
					for(int z=0; z<lines.Count && !lineFound;z++)
					{
						if(lines[z].name ==""+(solarSystems[x].planets[i].connectedPlanets[y].GetComponent<Planet>().getCoordinates()+solarSystems[x].planets[i].getCoordinates()))
						{
							lineFound =true;
						}
					}
					if(!lineFound)
					{
						GameObject newLine = (GameObject)GameObject.Instantiate(LinePrefab);
						
					
						newLine.name = ""+(solarSystems[x].planets[i].connectedPlanets[y].GetComponent<Planet>().getCoordinates()+solarSystems[x].planets[i].getCoordinates());
					
						Debug.Log(solarSystems[x].planets[i].name + " " + 
							solarSystems[x].planets[i].connectedPlanets[y].name);
					
						newLine.GetComponent<LineRenderer>().SetPosition(
							0,solarSystems[x].planets[i].getCoordinates());
					
						newLine.GetComponent<LineRenderer>().SetPosition(
							1,solarSystems[x].planets[i].connectedPlanets[y].GetComponent<Planet>()
							.getCoordinates());
					
						newLine.GetComponent<LineRenderer>().SetWidth(5f,5f);
					
						newLine.GetComponent<LineRenderer>().SetColors(solarSystems[x].planets[i].halo.color,
							solarSystems[x].planets[i].connectedPlanets[y].GetComponent<Planet>().halo.color);
					
						lines.Add(newLine);
					/*lines[y].GetComponent<LineRenderer>().SetColors(solarSystems[x].planets[i].
						owningPlayer.GetComponent<PlayerScript>().color,
						solarSystems[x].planets[i].connectedPlanets[y].GetComponent<Planet>()
						.owningPlayer.GetComponent<PlayerScript>().color);*/
						Camera.mainCamera.DoClear();
					}
					lineFound=false;
				}
			}
		}
	}
}
