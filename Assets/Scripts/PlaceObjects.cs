using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceObjects : MonoBehaviour {

    public GameObject[] constructions;
    public GameObject[] place;
    public string[] nomes;

    private GameObject selected;
    private GameObject selectedToPlace;

    public Text text;

    public Movement movement;

    private bool isSelected;
    private bool canPlace;

    public Quaternion rotation;

    private int id;

    // Use this for initialization
    void Start () {
        isSelected = false;
        canPlace = false;
        id = -1;
        text.text = "";
        rotation = Quaternion.Euler(-90, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            id++;
            
            if (id > constructions.Length - 1) {
                id = -1;
                allFalse();
            } else {
                if (isSelected && selected != constructions[id]) {
                    selected.SetActive(false);
                }

                isSelected = true;
                selectedToPlace = constructions[id];
                selected = place[id];
                text.text = nomes[id];
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            id--;
            
            if (id < -1) {
                id = constructions.Length - 1;
            }

            if (id == -1) {
                allFalse();
            } else {
                if (isSelected && selected != constructions[id]) {
                    selected.SetActive(false);
                }

                isSelected = true;
                selectedToPlace = constructions[id];
                selected = place[id];
                text.text = nomes[id];
            }
        }

        if (isSelected) {
            selected.SetActive(false);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.tag == "Construction" || movement.isMoving || hit.transform.tag == "Player") {
                    canPlace = false;
                    
                    selected = place[id + id];
                    selected.transform.rotation = rotation;

                    selected.SetActive(true);
                    selected.transform.position = hit.point;
                } else {
                    canPlace = true;

                    selected = place[(id + id) + 1];
                    selected.transform.rotation = rotation;

                    selected.SetActive(true);
                    selected.transform.position = hit.point;
                }

                if (Input.GetKey(KeyCode.Q)) {
                    selected.transform.Rotate(0, 0, 30 * Time.deltaTime);
                    rotation = selected.transform.rotation;
                }

                if (Input.GetKey(KeyCode.E)) {
                    selected.transform.Rotate(0, 0, -30 * Time.deltaTime);
                    rotation = selected.transform.rotation;
                }
            }
        }
        
        if (Input.GetMouseButtonDown(0) && isSelected && canPlace && !movement.isMoving) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                rotation = selected.transform.rotation;

                Instantiate(selectedToPlace, hit.point, rotation);

                id = -1;
                allFalse();
            }
        }
	}

    void allFalse() {
        selected.SetActive(false);
        selected = null;
        selectedToPlace = null;
        isSelected = false;
        canPlace = false;
        text.text = "";
    }
}
