using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickStart()
    {
        SceneManager.LoadScene("game");
    }

    public void OnClicrestart()
    {
        SceneManager.LoadScene("start");
    }

    public void OnClickURL()
    {
        Application.OpenURL("http://www.kanazawa-kagayasai.com/kagayasai/");
    }
}
