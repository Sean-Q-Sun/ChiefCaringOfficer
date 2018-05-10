using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private UIHandler UIH; // Our UI Handler that will deal with the drawing of UI elements when the player is giving input
    private GameManager GM; // Our Game Manager, which we will assign values to
    private InputStyle inputStyle; // The class that handles input while we are in style mode
    private InputNormal inputNormal; // The class that handles input while we are in normal mode

    private bool isSelecting = false; // Are we currently dragging/clicking to select units?
    private Vector3 mousePosition1; // The initial location of our mouse down

    private enum Mode
    {
        Normal,
        Style,
    };
    Mode inputMode;

    // Use this for initialization
    void Start()
    {
        UIH = GameObject.FindGameObjectWithTag("UIHandler").GetComponent<UIHandler>();
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        inputStyle = this.gameObject.GetComponent<InputStyle>();
        inputNormal = this.gameObject.GetComponent<InputNormal>();

        // TODO change to Normal Mode at first
        // Initiall set our input mode to Selection
        inputMode = Mode.Style;
    }

    // Update is called once per frame
    void Update()
    {
        // If we press the left mouse button, save mouse location
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition1 = Input.mousePosition;
            // If we are in style mode
            if (inputMode == Mode.Style)
            {
                /*Ray ray = Camera.main.ScreenPointToRay(mousePosition1);
                Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, 0));
                float distance;
                xy.Raycast(ray, out distance);*/
                Vector3 v3 = Input.mousePosition;
                v3.z = 20.0f;
                v3 = Camera.main.ScreenToWorldPoint(v3);


                /*LayerMask groundMask = new LayerMask();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray.origin, out hit, Mathf.Infinity, groundMask);*/

                // TODO remove this: Instantiate a Chair at the target location
                inputStyle.MouseClick(v3);
                Debug.Log("Spawned Chair");
            }


            isSelecting = true;
        }
        // If we let go of the left mouse button, end selection
        if (Input.GetMouseButtonUp(0))
        {
            isSelecting = false;
        }
        

        // If we are scrolling with the moues wheel, change the camera's orthographic position
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            ZoomCamera(new Vector3(0, 0, 0));
        }
    }

    private void OnGUI()
    {
        if (isSelecting)
        {
            // Create a rect from both mouse positions
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

    /// <summary>
    /// Zooms the camera towards a certain point
    /// </summary>
    /// <returns></returns>
    private void ZoomCamera(Vector3 zoomTowards)
    {
        // Get how much our mouse wheel has moved by
        float amount = Input.GetAxis("Mouse ScrollWheel");
        // Calculate how much we will have to move towards the zoomTowards position
        float multiplier = (1.0f / GM.mainCamera.orthographicSize * amount);

        // move camera
        transform.position += (zoomTowards - transform.position) * multiplier;

        // zoom camera
        GM.mainCamera.orthographicSize -= amount;

        // Limit zoom
        GM.mainCamera.orthographicSize = Mathf.Clamp(GM.mainCamera.orthographicSize, 1f, 20f);
    }
}
