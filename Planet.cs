using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Planet : MonoBehaviour {
	public List<GameObject> connectedPlanets;
	public int numOfUnits{get;set;}
	public GameObject owningPlayer{get;set;}	
	public GameObject solarSystem;
	public Light halo;
	public bool pulseLight =false;
	private int planetLayer=1<<8;
	public float orbitSpeed = .1f;
	public GameObject sun;
	public bool orbit=false;
	public float timer =0;
	private float timestop = 1;
	
	void Awake()
	{
		connectedPlanets = new List<GameObject>();
		setNeighbor();
	}
	void Start () {
		orbitSpeed = Random.Range(1,5) + Random.value;
		owningPlayer = null;
		numOfUnits =0;		
		
	}
	void Update()
	{
		//decides whether to rotate the planet
		if(orbit&&timer<timestop)
		{
			//rotates the planet
			transform.RotateAround(sun.transform.position,Vector3.up,orbitSpeed);
			timer += Time.deltaTime;
		}	
		if(timer>timestop)
		{
			setNeighbor();
			timer =0;
			orbit = false;
			
		}
		//Needs to be moved to more efficent spot
		//if(owningPlayer !=null);
			
			//renderer.material.color = owningPlayer.GetComponent<PlayerScript>().color;
		
	}
	//returns if the planet is owned or not
	public bool isOwned(){
		return owningPlayer != null;	
	}
	//updates the color of this planets halo to match it's owning players color
	public void updateHalo(){
		halo.color = owningPlayer.GetComponent<PlayerScript>().color;
	}
	//returns the coordinates of this planet
	public Vector3 getCoordinates()
	{
		return this.transform.position;
	}
	//creates the list of connected planets based off of how far they are from this planet
	public void setNeighbor()
	{
		connectedPlanets.Clear();
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 200,planetLayer);
		
		for(int i =0; i< hitColliders.Length;i++)
		{
			if(!(hitColliders[i].gameObject.name == this.name))
			{
				connectedPlanets.Add(hitColliders[i].gameObject);
			}
			//Debug.Log(connectedPlanets[i]);
		}
	}
	//sets orbit to true to make this plaent orbit around it's sun
	public void Orbit()
	{
		orbit = true;
	}
}
