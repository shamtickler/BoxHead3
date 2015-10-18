using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {

    [SerializeField]
    GameObject followObject;
    [SerializeField]
    Vector3 offset;
    [SerializeField]
    float damping = 1;
	
	// Update is called once per frame
	void Update () {

        //Follow "followObject"
        transform.position = Vector3.Lerp(transform.position, followObject.transform.position + offset, damping*Time.deltaTime);

	}
}
