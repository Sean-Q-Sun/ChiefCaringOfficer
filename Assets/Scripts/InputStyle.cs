using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputStyle : MonoBehaviour {

    GameManager GM; // Any time we want to access our Culture Catalog, we need to go through the GameManager
    public GameObject objectToPlace; // The current gameObject we are trying to place

    private Quaternion placeRotation;    // The current rotation of the object we want to place

	// Use this for initialization
	void Start () {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update () {
		// If the player presses the "R" key, rotate our currently selected object
        if (Input.GetKeyUp(KeyCode.R))
        {
            RotateObjectToPlace();
        }
	}

    public void MouseClick(Vector3 mousePosition)
    {
        // Whatever our currently selected object is, we need to instantiate it 
        GameObject newObject = GameObject.Instantiate<GameObject>(objectToPlace);

        // Set its position and rotation
        newObject.transform.rotation = placeRotation;
        newObject.transform.position = mousePosition; 

    }

    /// <summary>
    /// Rotates the selected object 90 degrees
    /// </summary>
    private void RotateObjectToPlace()
    {
        placeRotation.SetEulerRotation(placeRotation.x, placeRotation.y + 90f, placeRotation.z);
    }
}
