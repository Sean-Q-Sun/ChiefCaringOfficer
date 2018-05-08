using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{

    private GameManager GM; // Our Game Manager
    public GameObject cameraStartPos;   // The start position for the camera in the world

    // Use this for initialization
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        GM.mainCamera.transform.position = cameraStartPos.transform.position;
        GM.mainCamera.transform.rotation = cameraStartPos.transform.rotation;
    }

    /// This next section deals with drawing UI element
    /// Code modified from http://hyunkell.com/blog/rts-style-unit-selection-in-unity-5/
    #region Draw UI Elements

    /// Define a white texture. Easier to define it here for performance reasons
    static Texture2D _whiteTexture;
    public static Texture2D WhiteTexture
    {
        get
        {
            if (_whiteTexture == null)
            {
                _whiteTexture = new Texture2D(1, 1);
                _whiteTexture.SetPixel(0, 0, Color.white);
                _whiteTexture.Apply();
            }
            return _whiteTexture;
        }
    }

    /// Draw a rectangle to our screen
    /// Can only be called during OnGUI()
    public static void DrawScreenRect(Rect rect, Color color)
    {
        GUI.color = color;
        GUI.DrawTexture(rect, WhiteTexture);
        GUI.color = Color.white;
    }

    public static void DrawScreenRectBorder(Rect rect, float thickness, Color color)
    {
        // Top
        UIHandler.DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
        // Left
        UIHandler.DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
        // Right
        UIHandler.DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
        // Bottom
        UIHandler.DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);

    }

    private void OnGUI()
    {
        UIHandler.DrawScreenRect(new Rect(32, 32, 256, 128), Color.green);

        UIHandler.DrawScreenRect(new Rect(320, 32, 256, 128), Color.blue);
        UIHandler.DrawScreenRectBorder(new Rect(320, 32, 256, 128), 2, new Color(0.8f, 0.8f, 0.95f));
    }

    // Get the location our rectangle should be relative to the mouses' screen space location
    public static Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
    {
        // Move origin from bottom left (screen space) to top left (rectangle space)
        screenPosition1.y = Screen.height - screenPosition1.y;
        screenPosition2.y = Screen.height - screenPosition2.y;
        // Calculate corners
        Vector3 topLeft = Vector3.Min(screenPosition1, screenPosition2);
        Vector3 bottomRight = Vector3.Max(screenPosition1, screenPosition2);
        // Create Rect
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }
    #endregion

    /// <summary>
    /// Translates screen space into viewport space, given two screen positions, and creates an AABB
    /// </summary>
    /// <param name="camera">The camera we want to translate to</param>
    /// <param name="screenPosition1">The first position</param>
    /// <param name="screenPosition2">The second position</param>
    /// <returns>The Bounds for the given space, which represents an Axis-Aligned Bounding Box (AABB)</returns>
    public static Bounds GetViewportBounds(Camera camera, Vector3 screenPosition1, Vector3 screenPosition2)
    {
        // Translate our screen space into viewport space, and keep the same points
        Vector3 v1 = Camera.main.ScreenToViewportPoint(screenPosition1);
        Vector3 v2 = Camera.main.ScreenToViewportPoint(screenPosition2);
        Vector3 min = Vector3.Min(v1, v2);
        Vector3 max = Vector3.Max(v1, v2);
        min.z = camera.nearClipPlane;
        max.z = camera.farClipPlane;

        Bounds bounds = new Bounds();
        bounds.SetMinMax(min, max);
        return bounds;
    }
}
