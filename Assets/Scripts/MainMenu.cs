using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    float menuWidth = 170.0f;
    float menuHeight = 100.0f;
    float buttonWidth = 150.0f;
    float buttonHeight = 20.0f;
    float menuOffset = 10.0f;
    public GUIStyle customStyle;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 500, 200), "Star...Box?", customStyle);
        GUI.Box(new Rect((Screen.width - menuWidth) / 2, (Screen.height - menuHeight) / 2, menuWidth, menuHeight), "Main Menu");
        if (GUI.Button(new Rect((Screen.width - menuWidth) / 2 + 10, (Screen.height - menuHeight) / 2 + 30, buttonWidth, buttonHeight), "Start Level 1"))
        {
            Application.LoadLevel(1);
        }
        if (GUI.Button(new Rect((Screen.width - menuWidth) / 2 + 10, (Screen.height - menuHeight) / 2 + 60, buttonWidth, buttonHeight), "Exit"))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}
