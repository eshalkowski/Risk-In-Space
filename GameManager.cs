/*Add to empty GameObject... In charge of main gui
and keeping track of current state of the game

 To Implement:
 
Needs to find out how many players from main menu and update
numPlayers

Needs to Instantiate the number of players needed and load into 
"players" list

Player script will contain methods for each round this will
allow for easy implementation of computer players later using
the same method names for AI

 Player.C# will contain the following:
  List<GameObject> planets // to hold owned planets
  List<GameObject> systems // hold controlled systems
   Have values for systems located in "header" script
  int numPlanetsOwned // holds ttal num of planets owned
  methods:
  
  initialization()
  reinforcementPhase() // will calculate reinforcments and

   guide the user through the procedure

  battlePhase()
  upgradePhase()
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
 
	
	public List<SolarSystem> solarSystems;
	
	public int numPlayers;
	public int playerturn = 0;
 	//public GameObject[] players = new GameObject[numPlayers]();
 	public List<GameObject> players;
 	//public GameObject player2;
 	public int state;
	private GameObject clickedObject;
	private GameObject selectedPlanet;
	private GameObject targetPlanet;
	private bool roundComplete;
	private bool confirmPlanet = false;
	private bool battled = false;
	//public List<Color> colors;
	public GUISkin skin;
	private int numPlanets = 5;
	private bool firstRienforcement;
	public int numChosenPlanets = 0;
	private bool initialReinforcements;
	int playersdone=0;
 
 	void OnGUI(){
		
		//Static GUI Goes HERE ******************
		
		GUI.Label(new Rect(5, Screen.height - 95,200,25), players[playerturn].name);

		//***************************************
  		if(state == 0){
   			GUI.Box(new Rect(0,Screen.height - 100,Screen.width,100), "Choose Territory");
			if(confirmPlanet ==true)
			{
   				if (GUI.Button (new Rect (Screen.width/2-50,Screen.height - 50, 100, 50), "SelectPlanet")) {
					
    				
						addPlanet(selectedPlanet);
						confirmPlanet =false;
						
						players[playerturn].GetComponent<PlayerScript>().displayInfo =false;
						selectedPlanet.GetComponent<Planet>().numOfUnits = 3;
						playerturn = (playerturn+1) % numPlayers;
						numChosenPlanets++;
						updateSolarSystems();
					
					
				}
   			}
  		}
  		if(state == 1){
   			GUI.Box(new Rect(0,Screen.height - 100,Screen.width,100), "Reinforcement Phase");
  	 		/*if (GUI.Button (new Rect (Screen.width/2-50,Screen.height - 50, 100, 50), "Done")) {
    			playerturn++;
				if(playerturn>numPlayers)
				{
					state++;
					playerturn=1;
				}
   			}*/
 		}
  	
  		if(state == 2){
   			GUI.Box(new Rect(0,Screen.height - 100,Screen.width,100), "Battle Phase");
			if(!confirmPlanet)
				GUI.Box(new Rect(0,Screen.height - 50,Screen.width,100), "Select a Planet");
			else
			{
				GUI.Box(new Rect(0,Screen.height - 50,Screen.width,100), "Select Target Planet");
				if (GUI.Button (new Rect (Screen.width/2-50,Screen.height - 25, 100, 25), "Clear selection"))
				{
					selectedPlanet = null;
					confirmPlanet = false;
					battled=false;
				}
			}
			if(battled)
			{
				if (selectedPlanet.GetComponent<Planet>().numOfUnits>1&&
					(GUI.Button (new Rect (Screen.width/2-95,Screen.height - 75, 100, 25), "Battle with 1")))
				{
					if(battle(selectedPlanet,targetPlanet,1))
					{
						addPlanet(targetPlanet);
						//players[playerturn].GetComponent<PlayerScript>().addPlanet(targetPlanet, players[playerturn]);
						
						
					}
				}
				if (selectedPlanet.GetComponent<Planet>().numOfUnits>2&&
					(GUI.Button (new Rect (Screen.width/2+10,Screen.height - 75, 100, 25), "Battle with 2")))
				{
					if(battle(selectedPlanet,targetPlanet,2))
					{
						addPlanet(targetPlanet);
						//players[playerturn].GetComponent<PlayerScript>().addPlanet(targetPlanet, players[playerturn]);
						//targetPlanet.GetComponent<Planet>().owningPlayer = players[playerturn];
						
					}
				}
				if ((selectedPlanet.GetComponent<Planet>().numOfUnits>3 &&
					GUI.Button (new Rect (Screen.width/2+115,Screen.height - 75, 100, 25), "Battle with 3")))
				{
					if(battle(selectedPlanet,targetPlanet,3))
					{
						addPlanet(targetPlanet);
						//players[playerturn].GetComponent<PlayerScript>().addPlanet(targetPlanet,players[playerturn]);
						//targetPlanet.GetComponent<Planet>().owningPlayer = players[playerturn];
						
					}
				}
				if (selectedPlanet.GetComponent<Planet>().numOfUnits>1&&
					(GUI.Button (new Rect (Screen.width/2+220,Screen.height - 75, 100, 25), "Battle with All")))
				{
					if(battle(selectedPlanet,targetPlanet,selectedPlanet.GetComponent<Planet>().numOfUnits-1))
					{
						addPlanet(targetPlanet);
						//players[playerturn].GetComponent<PlayerScript>().addPlanet(targetPlanet,players[playerturn]);
						//targetPlanet.GetComponent<Planet>().owningPlayer = players[playerturn];
						
					}
				}
				
			}
			
   			if (GUI.Button (new Rect (Screen.width/2-200,Screen.height - 75, 50, 25), "Done")) {
				
				players[playerturn].GetComponent<PlayerScript>().displayInfo =false;
    			playerturn++;
				selectedPlanet = null;
				confirmPlanet = false;
				battled=false;
				
				if(playerturn<numPlayers)
				{	
					
					updateSolarSystems();
					battled = false;
				}
				else{					
					playerturn = 0;
					updateSolarSystems();
					battled=false;
					state++;
				}
   			}
  		}
  
  		if(state == 3){
   			GUI.Box(new Rect(0,Screen.height - 100,Screen.width,100), "Upgrade Phase");
   			if (GUI.Button (new Rect (Screen.width/2-50,Screen.height - 50, 100, 50), "Done")) {
    			players[playerturn].GetComponent<PlayerScript>().displayInfo =false;
				playersdone++;
				if(playersdone<numPlayers)
				{
					
					playerturn ++;
				}
				else
				{
					state=1;
					playerturn=0;
					playersdone=0;
				}
   			}
  		}
	 }
	//this is ran before any start function is ran
	void Awake()
	{
		
		
  		
		
	}
 	// Use this for initialization
	void Start () {
		players = new List<GameObject>();
		players.AddRange(GameObject.Find("Player1").GetComponent<PlayerScript>().Players);
		numPlayers = players.Count;
  		state = 0;
	  	//players = new GameObject[numPlayers];
  		
 	}
 
	// Update is called once per frame
 	void Update () {
		if(state == 0)
		{
			initialization();
		}
		if(state ==1)
		{
			
			initialReinforcement();
			
		}
		if(state == 2)
		{
			battlePhase();
		}
		
		players[playerturn].GetComponent<PlayerScript>().displayInfo =true;
		
  	
 	}
	//allows the current player to select a planet
	void initialization(){
		
		
		if(Input.GetMouseButtonDown(0))
		{
			clickedObject = GetClickedGameObject();
			
			if(clickedObject!=null && clickedObject.CompareTag("Planet")){
				if(!(clickedObject.GetComponent<Planet>().isOwned())){
					selectedPlanet = clickedObject;
				
					confirmPlanet =true;
				    
					
				}
				else{
					//Display Error Planet already owned	
					confirmPlanet = false;
				}
			}
		}
		if(numChosenPlanets >= numPlanets&& confirmPlanet ==false){
			roundComplete = true;	
			state++;
			playerturn=0;
			firstRienforcement= true;
		}
		
		
		
	}
	//adds planet to the current players list of planets owned
	void addPlanet(GameObject planet)
	{
		
					GameObject curPlayer = players[playerturn];
					
					curPlayer.GetComponent<PlayerScript>().addPlanet(planet,curPlayer);
					
					
					
					
					
	}
	//when a planet is clicked adds one unit to the planet for the current player
	void initialReinforcement()
	{
		int playersDone =0;
		//ran for initial rienforcements
		if(firstRienforcement ==true)
		{
			for(int i =0; i < players.Count; i++)
			{
				players[i].GetComponent<PlayerScript>().calculateUnits();
				if(initialReinforcements)
					players[i].GetComponent<PlayerScript>().numOfUnitsToDeploy += 5;
			}
			firstRienforcement=false;
		}
		
		if(Input.GetMouseButtonDown(0))
		{
			clickedObject = GetClickedGameObject();
			if(clickedObject!=null && clickedObject.CompareTag("Planet")){
				if((clickedObject.GetComponent<Planet>().owningPlayer== players[playerturn])){
				
					GameObject curPlayer = players[playerturn];
					players[playerturn].GetComponent<PlayerScript>().numOfUnitsToDeploy--;
					players[playerturn].GetComponent<PlayerScript>().displayInfo =false;
					playerturn = (playerturn+1) % numPlayers;
					updateSolarSystems();
					clickedObject.GetComponent<Planet>().numOfUnits += 1;
					
					
				}
				else{
					//Display Error Planet owned by other player	
				}
			}
		}
		//logic for ending the phase
		for(int i =0; i < players.Count; i++)
		{
			if(players[i].GetComponent<PlayerScript>().numOfUnitsToDeploy ==0)
			{
				playersDone++;
			}
			if(playersDone >= players.Count)
			{
				state++;
				selectedPlanet = null;
				firstRienforcement=true;
				initialReinforcements = false;
			}
		}
		
	}
	/*sets the currentplayer of all the solarSystems to the currentPlayer 
	 * needs to be called anytime a player is changed*/
	void updateSolarSystems()
	{
		for(int i =0; i < solarSystems.Count; i++)
		{
			solarSystems[i].curPlayer = players[playerturn];
		}
	}
	void battlePhase()
	{
		
		if(Input.GetMouseButtonDown(0)&& selectedPlanet ==null )
		{
			clickedObject = GetClickedGameObject();
			if(clickedObject!=null && clickedObject.CompareTag("Planet")){
				if((clickedObject.GetComponent<Planet>().owningPlayer== players[playerturn])){
								
					selectedPlanet = clickedObject;
					confirmPlanet = true;
					
						
				}
				else{
				//Display Error Planet owned by other player	
				}
			}
		}
		if(Input.GetMouseButtonDown(0)&&selectedPlanet!= null)
		{
			clickedObject = GetClickedGameObject();
			if(clickedObject!=null && clickedObject.CompareTag("Planet")&&clickedObject != selectedPlanet){
				if((clickedObject.GetComponent<Planet>().owningPlayer!= players[playerturn])){
									
					targetPlanet = clickedObject;
					
					battled = true;
				}
				else{
					//Display Error Planet owned by other player	
				}
			}
		}
		
	}
	bool battle(GameObject playerPlanet, GameObject targetPlanet, int attackingUnits)
	{		
		int attackRoll;
		int defenseRoll;
		while(attackingUnits>0)
		{
			attackRoll = Random.Range(1,6);
			defenseRoll = Random.Range(1,6);
			if(attackRoll >defenseRoll)
			{
				targetPlanet.GetComponent<Planet>().numOfUnits --;
				if(targetPlanet.GetComponent<Planet>().numOfUnits ==0)
				{
					targetPlanet.GetComponent<Planet>().numOfUnits = attackingUnits;
					playerPlanet.GetComponent<Planet>().numOfUnits -= attackingUnits;
					return true;
				}
				
			}
			else{
				playerPlanet.GetComponent<Planet>().numOfUnits--;
				attackingUnits--;
			}
			
		}
		return false;
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
