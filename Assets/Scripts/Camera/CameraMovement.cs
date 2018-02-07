using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    float speed = 100.0f;
    Vector3 maxPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        maxPosition = transform.position;

        if(Input.GetKey(KeyCode.A))
        {
            maxPosition.x -= speed * Time.deltaTime;
            if (maxPosition.x < 50)
                maxPosition.x = 50;
        }
        if (Input.GetKey(KeyCode.S))
        {
            maxPosition.z -= speed * Time.deltaTime;
            if (maxPosition.z < 50)
                maxPosition.z = 50;
            
        }
        if (Input.GetKey(KeyCode.W))
        {
            maxPosition.z += speed * Time.deltaTime;
            if (maxPosition.z > 950)
                maxPosition.z = 950;
        }
        if (Input.GetKey(KeyCode.D))
        {
            maxPosition.x += speed * Time.deltaTime;
            if (maxPosition.x > 950)
                maxPosition.x = 950;
            
        }

        transform.position = maxPosition;
	
	}
}
