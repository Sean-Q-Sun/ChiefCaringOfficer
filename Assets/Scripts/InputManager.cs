using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private UIHandler UIH; // Our UI Handler that will deal with the drawing of UI elements when the player is giving input
    private GameManager GM; // Our Game Manager, which we will assign values to

    private bool isSelecting = false; // Are we currently dragging/clicking to select units?
    private Vector3 mousePosition1; // The initial location of our mouse down

    // Use this for initialization
    void Start()
    {
        UIH = GameObject.FindGameObjectWithTag("UIHandler").GetComponent<UIHandler>();
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // If we press the left mouse button, save mouse location and begin selection
        if (Input.GetMouseButtonDown(0))
        {
            isSelecting = true;
            mousePosition1 = Input.mousePosition;
        }
        // If we let go of the left mouse button, end selection
        if (Input.GetMouseButtonUp(0))
        {
            isSelecting = false;
        }
    }

    private void OnGUI()
    {
        if (isSelecting)
        {
            // Create a rect from both moues positions
            Rect rect = UIHandler.GetScreenRect(mousePosition1, Input.mousePosition);
            UIHandler.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            UIHandler.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));

        }
    }



    private bool PieceSelection()
    {
        if (!isSelecting)
            return false;
        // loop through our list of pieces and check to see if they are within our bounds
        //for (int i = 0; i < GM.listOfUnits; i++) {
        Bounds viewportBounds = UIHandler.GetViewportBounds(GM.mainCamera, mousePosition1, Input.mousePosition);
        return true;
        //return viewportBounds.Contains(GM.mainCamera.WorldToViewportPoint(UnitSelection.transform.position))
        //}
    }
}
