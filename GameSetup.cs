using UnityEngine;
using System.Collections;

public class GameSetup : MonoBehaviour {
	public int mainContainerWidth;
	public int mainContainerHeight;
	public int buttonWidth = 200;
	public int buttonHeight = 50;
	private int xContPos; 
	private int yContPos; 
	private int numPlayers = 0;
	private string[] toolbarStrings = {"2", "3", "4", "5", "6", "7", "8"};
	private Vector2 scrollPosition = Vector2.zero;
	
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
		numPlayers = GUI.Toolbar (new Rect (xContPos + ((mainContainerWidth - 250)/2), yContPos + 85, 250, 30), numPlayers, toolbarStrings);
		
		// An absolute-positioned example: We make a scrollview that has a really large client
		// rect and put it in a small rect on the screen.
		scrollPosition = GUI.BeginScrollView (new Rect ((xContPos + (mainContainerWidth - 325)/2)
			,yContPos + 150,325,100),
			scrollPosition, new Rect (0, 0, 300, 250));
		int yPos = 5;
		// Content for scroll view
		for (int x = 0; x <= numPlayers+1; x++){
			
			int playerNum = x+1;
			//string curPlayer = ("Player" + playerNum);
			GUI.Label(new Rect(5,yPos,100,25), ("Player" + playerNum) );
			yPos += 30;
		}
		yPos = 5;
		// End the scroll view that we began above.
		GUI.EndScrollView ();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		mainContainerWidth = Screen.width;
		mainContainerHeight = Screen.height;
		xContPos = (Screen.width/2)-(mainContainerWidth/2);
		yContPos = (Screen.height/2)-(mainContainerHeight/2);
	}
}
