using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamecontroller : MonoBehaviour {

    public GameObject[] daikon = new GameObject[3];
    private bool canDrop = true;
    private List<GameObject> daikons = new List<GameObject>();

    private GameObject waitingObject;

    Dictionary<GameObject, List<GameObject>> hashMap = new Dictionary<GameObject, List<GameObject>>();
    public GameObject[] dishes = new GameObject[3];

    public UnityEngine.UI.Text vegLableText;
    public UnityEngine.UI.Text shopTitleText;
    public UnityEngine.UI.Text shopDescText;
    public UnityEngine.UI.Button URLButton;
    public UnityEngine.UI.Button returnButton;
    private string[] vegNames = { "源助大根","五郎島金時","金時草"};
    private string[] shopTitles = { "大根の店" ,"芋の店","金時草の店"};
    private string[] shopDescriptions = { "これは大根の説明です。","芋の説明","金時草の説明" };
    private string[] shopURLs = { "http://www.kanazawa-kagayasai.com/" , "http://www.kanazawa-kagayasai.com/recipe/list.php?kagayasai=1", "http://www.kanazawa-kagayasai.com/recipe/list.php?kagayasai=4" };

    private bool isClear = false;

    // Use this for initialization
    void Start () {
        Debug.Log("Start");
        waitingObject = Instantiate(daikon[Random.Range(0, 3)], new Vector3(0, 5, 0), new Quaternion());
        waitingObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        vegLableText.text = vegNames[waitingObject.GetComponent<VegatableObject>().kagaId];
        shopTitleText.gameObject.SetActive(false);
        shopDescText.gameObject.SetActive(false);
        URLButton.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if (!isClear)
        {
            Vector3 pos = new Vector3(waitingObject.GetComponent<Transform>().position.x + (float)2.0,
                          waitingObject.GetComponent<Transform>().position.y - (float)3.0,
                          waitingObject.GetComponent<Transform>().position.z);
            vegLableText.gameObject.GetComponent<RectTransform>().position =
                            RectTransformUtility.WorldToScreenPoint(Camera.main, pos);
        }


        if (Input.GetMouseButton(0) && canDrop)
        {

            //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition).x);

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
            if (count == daikons.Count && !isClear )
            {
                if (CheckAttach())
                {
                    isClear = true;
                }
                else
                {
                    canDrop = true;
                    waitingObject = Instantiate(daikon[Random.Range(0, 3)], new Vector3(0, 5, 0), new Quaternion());
                    waitingObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                    vegLableText.text = vegNames[waitingObject.GetComponent<VegatableObject>().kagaId];
                    DataManeger.GetInstance().score = count;

                }

            }
        }
	}
    // 連結オブジェクト情報の登録   
    public void RegistAttachedObject(GameObject id1, GameObject id2)
    {   //id1: 当て"られ"ている方, id2: 当たっている方
        if (!hashMap.ContainsKey(id1))
        {
            hashMap[id1] = new List<GameObject>();
        }
        if (!hashMap[id1].Contains(id2))
        {
            hashMap[id1].Add(id2);
        }
    }

    // 連結オブジェクト情報の削除
    public void RemoveAttachedObject(GameObject id1, GameObject id2)
    {
        if (hashMap.ContainsKey(id1))
        {
            if (hashMap[id1].Contains(id2))
            {
                hashMap[id1].Remove(id2);
            }
        }
    }

    bool CheckAttach()
    {
        foreach (GameObject e in hashMap.Keys)
        {
            Vector3 position = e.GetComponent<Transform>().position;


            List<GameObject> list = LinkingFind(e, new List<GameObject>());
            if (list.Count >= 3)
            {
                foreach (GameObject go in list)
                {
                    Destroy(go);
                    daikons.Remove(go);
                    hashMap.Remove(go);
                }
                position.y += 2;    // 地面に埋まらないように調整（横にも調整が必要）
                int id = e.GetComponent<VegatableObject>().kagaId;
                Instantiate(dishes[id], position, new Quaternion());
                shopTitleText.text = shopTitles[id];
                shopDescText.text = shopDescriptions[id];

                shopTitleText.gameObject.SetActive(true);
                shopDescText.gameObject.SetActive(true);
                URLButton.gameObject.SetActive(true);
                returnButton.gameObject.SetActive(true);
                vegLableText.gameObject.SetActive(false);

                return true;
            }
        }

        return false;
    }

    List<GameObject> LinkingFind(GameObject node, List<GameObject> visitedObjects)
    {

        List<GameObject> retList = new List<GameObject>();

        if (visitedObjects.Contains(node))
        {
            return visitedObjects;
        }
        visitedObjects.Add(node);

        foreach (GameObject e in hashMap[node])
        {
            retList.AddRange(LinkingFind(e, visitedObjects));
        }

        return retList;
    }
}
