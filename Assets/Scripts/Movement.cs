using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float velocity;
    public float rotation;

    public bool isMoving;

    public PlaceObjects placeObjects;

	// Use this for initialization
	void Start () {
        isMoving = false;
	}
	
	// Update is called once per frame
	void Update () {
        /*if (Input.GetMouseButton(0) && !placeObjects.isSelected) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                destination = hit.point;
                agent.SetDestination(destination);
            }
        }*/

        //if (!placeObjects.isSelected) {
        if (Input.GetKey(KeyCode.W)) {
            transform.Translate(0, 0, velocity * Time.deltaTime);
        } else {
            isMoving = false;
        }

        if (Input.GetKey(KeyCode.S)) {
            transform.Translate(0, 0, -velocity * Time.deltaTime);
        } else {
            isMoving = false;
        }

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(0, -rotation * Time.deltaTime, 0);
        } else {
            isMoving = false;
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(0, rotation * Time.deltaTime, 0);
        } else {
            isMoving = false;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) {
            isMoving = true;
        }
        //}
	}
}
