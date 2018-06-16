using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManeger : MonoBehaviour {

    private static DataManeger _singleInstanece = new DataManeger();
    public int score = 0;
   
    public static DataManeger GetInstance()
    {
        return _singleInstanece;
    }

    private DataManeger() { }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
