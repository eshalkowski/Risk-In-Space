using UnityEngine;
using System.Collections;

public class PulseLight : MonoBehaviour {
	public Light halo;
	public float lightMax;
	public float lightMin;
	public bool pulseLight = false;
	public float uptime =0;
	public float downtime =0;
	public bool decreasing= false;
	public float lightStandard;
	private GameObject clickedObject;
	public GameObject attachedPlanet;
	public float rangeChange = 3;
	// Use this for initialization
	void Start () {
		lightMax = halo.range+(halo.range*.25f);
		lightMin = halo.range-(halo.range* .25f);
		lightStandard = halo.range;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			clickedObject = GetClickedGameObject();
			
		}
		if(clickedObject == attachedPlanet)
				pulsing();
		else
		{
			halo.range = lightStandard;
		}
		
	
	}
	public void pulsing()
	{
		
		if(uptime < .5)
		{
			
				halo.range += rangeChange;
			uptime += Time.deltaTime;
		}
		else
		{
			decreasing=true;
		}
		if( uptime >.5 && downtime <.5)
		{
			halo.range -= rangeChange;
			downtime += Time.deltaTime;
		}
		if(downtime > .5)
		{
			uptime =0;
			downtime =0;
		}
		
	}
	GameObject GetClickedGameObject()
	{
   		 // Builds a ray from camera point of view to the mouse position
   		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    	RaycastHit hit;
  		 // Casts the ray and get the first game object hit
    	if (Physics.Raycast(ray, out hit, Mathf.Infinity))
       		return hit.transform.gameObject;
   		else
    		return null;
		
	}
}
