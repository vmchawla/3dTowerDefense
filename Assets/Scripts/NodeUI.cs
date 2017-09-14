using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUI : MonoBehaviour {

    private Node target;
    private GameObject ui;

	void Start () {

        ui = GameObject.Find("NodeUI/Canvas");
        ui.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetTarget(Node node)
    {
        target = node;
        transform.position = target.transform.position;
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }
}
