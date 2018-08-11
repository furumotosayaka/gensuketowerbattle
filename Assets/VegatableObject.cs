using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegatableObject : MonoBehaviour {

    public int kagaId;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "yasai")
        {
            return;
        }

        Gamecontroller gameController = GameObject.Find("GameController").GetComponent<Gamecontroller>();

        if (this.kagaId == collision.gameObject.GetComponent<VegatableObject>().kagaId)
        {
            GameObject id1 = this.gameObject;
            GameObject id2 = collision.gameObject;
            gameController.RegistAttachedObject(id1, id2);
        } 

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "yasai")
        {
            return;
        }


        Gamecontroller gameController = GameObject.Find("GameController").GetComponent<Gamecontroller>();

        if (this.kagaId == collision.gameObject.GetComponent<VegatableObject>().kagaId)
        {
            GameObject id1 = this.gameObject;
            GameObject id2 = collision.gameObject;
            gameController.RemoveAttachedObject(id1, id2);
        }
    }

}
