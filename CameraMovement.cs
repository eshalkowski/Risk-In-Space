/*alls GUI components need the variables from the various scripts for each of the planets*/
//defines the logic for Camera movement

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CameraMovement : MonoBehaviour {
	public Camera MainCamera;
	public float speed = 5;
	public float scrollSpeed = 5;
	public int speedMultiplier =10;
	private int screenWidth;
	private int screenHeight;
	public int boundary =50;
	private int clicks;
	private GameObject clicked;
	private GameObject nextClicked;
	RaycastHit hit;
	private bool zoomed;
	public float timer=0;
	private float timeToHover=.5f;
	public bool displayInfo;
	private float zoomedFieldOfView=10;
	public List<GameObject> solarSystems;
	public int maxInMovement = 200;
	public int maxOutMovement = 1000;
	public Vector3 lastCameraPos;
	
	// Use this for initialization
	void Start () {	
		//solarSystems = new List<GameObject>();
		for(int i =0; i < solarSystems.Count; i++)
		{
			solarSystems[i].renderer.enabled= false;
			solarSystems[i].collider.enabled= false;
		}
			
		
		screenWidth = Screen.width;
    	screenHeight = Screen.height;
		clicks =0;
		zoomed = false;
	}
	void OnGUI()
	{
		if(displayInfo)
		{
			displayBasicInfo();
		}
		if(zoomed)
		{
			displayDetailedInfo();
		}
	}
	// Update is called once per frame
	void Update () {
		speed = MainCamera.orthographicSize*speedMultiplier;
		//keyboard input
		if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))
		{
			if(zoomed)
				exitZoom();
			MainCamera.transform.position += new Vector3((speed*-Time.deltaTime),0,0);
		}
		if(Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))
		{
			if(zoomed)
				exitZoom();
			MainCamera.transform.position += new Vector3((speed*Time.deltaTime),0,0);
		}
		if(Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow))
		{
			if(zoomed)
				exitZoom();
			MainCamera.transform.position += new Vector3(0,0,speed*-Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow))
		{
			if(zoomed)
				exitZoom();
			
			MainCamera.transform.position += new Vector3(0,0,speed*Time.deltaTime);
		}
		//zoom
		if(Input.GetAxis("Mouse ScrollWheel")!=0)
		{
			if(zoomed)
				exitZoom();		
			//Limit movement
			 
			if(MainCamera.orthographicSize<=maxInMovement){
				if(Input.GetAxis("Mouse ScrollWheel") < 0)
					MainCamera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel")*scrollSpeed;
			}
			else if(MainCamera.orthographicSize>=maxOutMovement){
				if(Input.GetAxis("Mouse ScrollWheel") > 0)
					MainCamera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel")*scrollSpeed;
			}
			else{
				MainCamera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel")*scrollSpeed;
				// set last quarter of movement to solar system view
				if(MainCamera.orthographicSize >= maxOutMovement - maxOutMovement/4)
				{
					for(int i =0; i < solarSystems.Count; i++)
					{
						solarSystems[i].renderer.enabled=true;
						solarSystems[i].collider.enabled=true;
					}
				}
				else
				{
					//Debug.Log("HERE");
					if(solarSystems[0].renderer.enabled == true)
					{
						for(int i =0; i < solarSystems.Count; i++)
						{
							solarSystems[i].renderer.enabled = false;
							solarSystems[i].collider.enabled = false;
						}
					}
				}
			}
				
			
		}
		
		//Edge scrolling
		/*if (Input.mousePosition.x > screenWidth - boundary)
   		{
    		MainCamera.transform.position += new Vector3((speed*Time.deltaTime),0,0); // move on +X axis
    	}
     
    	if (Input.mousePosition.x < 0 + boundary)
    	{
    		MainCamera.transform.position += new Vector3((speed*-Time.deltaTime),0,0); // move on -X axis
    	}
     
   		if (Input.mousePosition.y > screenHeight - boundary)
    	{
    		MainCamera.transform.position += new Vector3(0,0,speed*Time.deltaTime); // move on +Z axis
    	}
     
    	if (Input.mousePosition.y < 0 + boundary)
    	{
    		MainCamera.transform.position += new Vector3(0,0,speed*-Time.deltaTime); // move on -Z axis
    	}*/
		zoomToPlanet();
		showPlanetInfo();
		
	}
	//exits the zoomed planet mode
	void exitZoom(){
		//changes the camera back to orthographic view
		MainCamera.isOrthoGraphic = true;
		if(clicked!=null)
		{
			MainCamera.orthographicSize = clicked.transform.localScale.x*clicked.transform.parent.localScale.x;
			MainCamera.transform.position = new Vector3(clicked.transform.position.x,100,clicked.transform.position.z);
		}
		else
		{
			MainCamera.transform.position = lastCameraPos;
			MainCamera.orthographicSize = maxInMovement;
		}
		MainCamera.transform.eulerAngles =new Vector3(90,0,0);
		zoomed = false;
		clicks = 0;
		
		//if solar system plane is visible turn it invisible
		if(solarSystems[0].renderer.enabled == true)
		{
			for(int i =0; i < solarSystems.Count; i++)
			{
				solarSystems[i].renderer.enabled= false;
				solarSystems[i].collider.enabled= false;
			}
		}
	}
	//enables the planet info GUI to be shown
	void showPlanetInfo()
	{
		nextClicked =GetClickedGameObject();
		if(nextClicked != null && nextClicked.tag == "Planet" && !zoomed)
		{
			
			timer += Time.deltaTime;
			if(timer >= timeToHover)
			{
				displayInfo =true;
			}
		}
		else
		{
			displayInfo=false;
			timer =0;
		}
		
	}
	//shows the planets detailed info 
	void displayDetailedInfo()
	{
		GUI.BeginGroup(new Rect(screenWidth/2+100, screenHeight/2-100,300,400));
		if(clicked!=null && clicked.GetComponent<Planet>().owningPlayer != null)
		{
			GUI.Label(new Rect(0,20,100,100), "Owner: "+clicked.GetComponent<Planet>().owningPlayer.gameObject.name);
		}
		else
		{
			GUI.Label(new Rect(0,20,100,100), "Owner: None");
		}
		if(clicked!=null)
		{
			GUI.Label(new Rect(0,35,100,100), "Unit Count: "+clicked.GetComponent<Planet>().numOfUnits);
			GUI.Label (new Rect(0,50,200,400),"Solar System: "+clicked.GetComponent<Planet>().solarSystem.gameObject.name);
			GUI.Label(new Rect(0,65,200,400), "Solar System Control: "
				+clicked.GetComponent<Planet>().solarSystem.GetComponent<SolarSystem>().calculateOwnedPlanets()
				+"/"+clicked.GetComponent<Planet>().solarSystem.GetComponent<SolarSystem>().planets.Count);
		}
		GUI.EndGroup();
	}
	//zooms to a planet when it is double clicked
	void zoomToPlanet(){
		
		if(Input.GetMouseButtonDown(0))
		{	
			if(clicks==0)
			{
				clicked = GetClickedGameObject();
				clicks++;
			}
			else{
				
				//checks if the same gameobject is clicked twice in a row
				if(clicked!=null &&(clicked == GetClickedGameObject()))
				{
					lastCameraPos = MainCamera.transform.position;
					Debug.Log(clicked.transform.position.x);
					//changes the camera to perspective and sets up the angle to see the planet
					MainCamera.transform.position = new Vector3(clicked.transform.position.x+
						(clicked.transform.localScale.x*(clicked.transform.parent.localScale.x/2)) ,0,
						clicked.transform.position.z-clicked.transform.localScale.x*clicked.transform.parent.localScale.x*10);
					MainCamera.transform.eulerAngles =new Vector3(0,0,0);
					MainCamera.isOrthoGraphic = false;
					
					zoomedFieldOfView = 10;
					MainCamera.fieldOfView = zoomedFieldOfView;
					zoomed = true;
					
				}
				//if the same game object was not clicked twice 
				else
				{
					clicks =0;
				}
			}
		}
	}
	//displays the gui for the basic info of a planet
	void displayBasicInfo()
	{
		if(nextClicked.tag == "Planet" && nextClicked.GetComponent<Planet>().owningPlayer != null)
		{
			GUI.Box(new Rect(Input.mousePosition.x,Screen.height-Input.mousePosition.y,100,50), "Units: "
				+nextClicked.GetComponent<Planet>().numOfUnits+" \nOwner: "+nextClicked.GetComponent<Planet>().owningPlayer.gameObject.name);
		}
		else
		{
			GUI.Box(new Rect(Input.mousePosition.x,Screen.height-Input.mousePosition.y,100,50), "Units: "
				+nextClicked.GetComponent<Planet>().numOfUnits+" \nOwner: None");
		}
		//new Rect(
	}
	//returns the gameobject under the mouse
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
