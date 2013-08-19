using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	public int mainContainerWidth = 250;
	public int mainContainerHeight = 450;
	public int buttonWidth = 200;
	public int buttonHeight = 50;
	private int xContPos; 
	private int yContPos; 
	void OnGUI(){
		GUI.Box(new Rect((xContPos),yContPos,mainContainerWidth,mainContainerHeight), "Risk In Space");	
		if (GUI.Button (new Rect(xContPos + ((mainContainerWidth - buttonWidth)/2),
			yContPos + 50, buttonWidth, buttonHeight), "New Game"))
		{
			Application.LoadLevel("GameSetup");
		}
		if (GUI.Button (new Rect(xContPos + ((mainContainerWidth - buttonWidth)/2),
			yContPos + 105, buttonWidth, buttonHeight), "Exit"))
		{
			Application.Quit();
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		xContPos = (Screen.width/2)-(mainContainerWidth/2);
		yContPos = (Screen.height/2)-(mainContainerHeight/2);
	}
}
