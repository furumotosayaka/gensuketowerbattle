using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamecontroller : MonoBehaviour {

    public GameObject[] daikon = new GameObject[3];
    private bool canDrop = true;
    private List<GameObject> daikons = new List<GameObject>();

    private GameObject waitingObject;


	// Use this for initialization
	void Start () {
        Debug.Log("Start");
        waitingObject = Instantiate(daikon[Random.Range(0, 3)], new Vector3(0, 5, 0), new Quaternion());
        waitingObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0) && canDrop)
        {

            Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition).x);

            Vector3 nextPosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 5, 0);
            waitingObject.GetComponent<Transform>().position = nextPosition;
        
        }
        if (Input.GetKeyDown(KeyCode.Space) && canDrop){
            waitingObject.GetComponent<Transform>().eulerAngles = new Vector3(0, 0, (waitingObject.GetComponent<Transform>().eulerAngles.z + 30)%360);
        }

        if (Input.GetMouseButtonUp(0) && canDrop) {

            waitingObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            canDrop = false;
            daikons.Add(waitingObject);
        }
        if (canDrop == false)
        {
            int count = 0;
            foreach (GameObject e in daikons)
            {
                if (e.GetComponent<Rigidbody2D>().IsSleeping())
                {
                    count++;
                }

            }
            if (count == daikons.Count)
            {
                canDrop = true;
                waitingObject = Instantiate(daikon[Random.Range(0, 3)], new Vector3(0, 5, 0), new Quaternion());
                waitingObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                DataManeger.GetInstance().score = count;
                Debug.Log("score" + DataManeger.GetInstance().score);
            }
        }
	}
}
