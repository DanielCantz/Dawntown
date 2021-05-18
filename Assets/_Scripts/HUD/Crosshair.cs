// Convert the 2D position of the mouse into a
// 3D position.  Display these on the game window.

using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private Camera cam;
    private Vector3 point;

    public Vector3 Point { get => point; }

    void Start()
    {
        cam = Camera.main;
    }

    void OnGUI()
    {
        point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;
        
        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y - 2, 5));

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.EndArea();
    }


}