using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private GameObject selectedPiece;   // The game piece we have selected (Currently only one at a time)
    public Camera mainCamera;   // The main camera in our scene
    public RaycastHit mousePositionInWorld; // The position of our mouse in the world space

    // public List<Unit> listOfPlayers;
    // public Player activePlayer;   // The player that is currently taking their turn

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Create a raycast and see if we are touching anything
        mousePositionInWorld = new RaycastHit();
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out mousePositionInWorld))
        {
            Debug.Log("We hit " + mousePositionInWorld.collider.gameObject.name);
        }
        else
        {
            Debug.Log("Nothing was hit");
        }

        // If we have clicked, set the raycast's target to be selected
        if (Input.GetMouseButtonUp(0))
        {
            /*if (mousePositionInWorld.collider.gameObject.CompareTag("GamePiece"))
            {
                Debug.Log("here");
                selectedPiece = mousePositionInWorld.collider.gameObject;
                //selectedPiece.GetComponent<Piece>().SelectPiece();
                Debug.Log("Selected Piece");
            }*/
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}