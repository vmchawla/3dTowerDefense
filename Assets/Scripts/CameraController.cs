using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private float panSpeed = 30f;
    [SerializeField] private float panBorderThickness = 10f;
    [SerializeField] private float scrollSpeed = 5f;
    [SerializeField] private float minY = 10f;
    [SerializeField] private float maxY = 80f;


    private bool isCameraMoveEnabled = true;
    

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isCameraMoveEnabled = !isCameraMoveEnabled;
        }

        if (isCameraMoveEnabled)
        {
            if ((Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness) && transform.position.z <= -15f)
            {
                transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
            } else if ((Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness) && transform.position.z >= -75f)
            {
                transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
            } else if ((Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness) && transform.position.x >= 15f)
            {
                transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
            } else if ((Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness) && transform.position.x <=60f)
            {
                transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
            }
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;

        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
		
	}
}
