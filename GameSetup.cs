using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSetup : MonoBehaviour {
	public int mainContainerWidth;
	public int mainContainerHeight;
	public int buttonWidth = 200;
	public int buttonHeight = 50;
	public List <GUIStyle> colors;
	
	private int xContPos; 
	private int yContPos; 
	private int numPlayers = 0;
	private string[] toolbarStrings = {"2", "3", "4", "5", "6"};
	private Vector2 scrollPosition = Vector2.zero;
	private bool generateNames = true;
	private int selectedIndex = 0;
	private List<Color>  availableColors;
	private List<Color>  playerColors;
	//IList color = new IList;
	void OnGUI(){
		GUI.Box(new Rect((xContPos),yContPos,mainContainerWidth,mainContainerHeight), "Game Setup");	
		if (GUI.Button (new Rect(xContPos + ((mainContainerWidth - buttonWidth)/2),
			yContPos + 300, buttonWidth, buttonHeight), "Main Menu"))
		{
			
			Application.LoadLevel("MainMenu");
		}
		
		if (GUI.Button (new Rect(xContPos + ((mainContainerWidth - buttonWidth)/2),
			yContPos + 350, buttonWidth, buttonHeight), "Start Game"))
		{
			//start game with selected preferences
		}
		GUI.Box(new Rect(xContPos + ((mainContainerWidth - 250)/2),yContPos + 50,250,90), "Number of Players");
		//GUI.Label(new Rect (25, 25, 250, 30),"Number of Players");
		//0 = 2, 1 = 3, etc...
		selectedIndex = GUI.Toolbar (new Rect (xContPos + ((mainContainerWidth - 250)/2), yContPos + 85, 250, 30), selectedIndex, toolbarStrings);
		if (selectedIndex != numPlayers){
			numPlayers = selectedIndex;
			generateNames = true;
			resetColors(); 
		}
		// An absolute-positioned example: We make a scrollview that has a really large client
		// rect and put it in a small rect on the screen.
		scrollPosition = GUI.BeginScrollView (new Rect ((xContPos + (mainContainerWidth - 325)/2)
			,yContPos + 150,325,100),
			scrollPosition, new Rect (0, 0, 300, 250));
		int yPos = 5;
		// Content for scroll view
		for (int x = 0; x <= numPlayers+1; x++){
			
			int playerNum = x+1;
			GUI.Label(new Rect(5,yPos,100,25), ("Player" + playerNum) );
			if(generateNames)
			{		
				
				playerColors.Add(availableColors[0]);
				availableColors.RemoveAt(0);
				
			}
			GUI.backgroundColor = playerColors[x];
			Debug.Log (playerColors[x]);
			if (GUI.Button(new Rect(110,yPos,25,25),"")){
				//onhover toolbar popout
			}
			yPos += 30;
			
			
		}
		generateNames = false;
		
		GUI.EndScrollView ();
	}
	//startgame if()
	//{
		//do all final setup and pass and call next scene	
//	}
	// Use this for initialization
	void Start () {
		
	}
	void Awake(){
		availableColors = new List<Color>();
		playerColors = new List<Color>();
		resetColors(); 
	}
	// Update is called once per frame
	void Update () {
		mainContainerWidth = Screen.width;
		mainContainerHeight = Screen.height;
		xContPos = (Screen.width/2)-(mainContainerWidth/2);
		yContPos = (Screen.height/2)-(mainContainerHeight/2);
	}
	
	void populatePlayerColors(){
		
	}
	
	void resetColors(){
		availableColors.Clear ();
		playerColors.Clear();
		playerColors.Capacity = 10;
		availableColors.Capacity = 10;
		availableColors.Add(new Color(0,0,255));
		availableColors.Add(new Color(0,255,0));
		availableColors.Add(new Color(255,0,0));
		availableColors.Add(new Color(255,255,0));
		availableColors.Add(new Color(255,0,255));
		availableColors.Add(new Color(0,255,255));
		
	}
}
