using UnityEngine;

public class CameraController : MonoBehaviour
{
    //A camera controller for an rts type that allows WASD panning
    // nice thread for detailed camera for rts: https://forum.unity.com/threads/rts-camera-script.72045/


    #region Variables
    private bool doMovement = true;

    [Header("Y")]
    public float maxY = 0;
    public float minY = 0;


    [Header("X")]
    public float minX = 0;
    public float maxX = 0;

    [Header("Z")]
    public float minZ = 0;
    public float maxZ = 0;

    [Header("Pan settings")]
    public float panSpeed = 30f;
    public float panBorderThickness = 13f;

    [Header("Camera Settings")]
    public float scrollSpeed = 20;
    public float zoomSpeed = 2;


    private Vector3 pos;

    /*
    new Camera camera;
    float startFOV = 60;
    float minFOV = 10f;
    float maxFOV = 60f;
    */
    #endregion Variables

    void Start()
    {
        //camera = GetComponent<Camera>();
        pos = transform.position;
        //GetComponent<Camera>().fieldOfView = startFOV;
    }

    void Update()
    {
        if (GameManager.ifGameEnded)
        {
            this.enabled = false;
            return;
        }

        //Making sure camera doesnt run too far from screen
        if (Input.GetKeyDown(KeyCode.F)) doMovement = !doMovement;

        if (doMovement == false)
        {
            return;
        }

        #region panning code

        //Panning code
        if (Input.GetAxis("Vertical") > 0 || Input.mousePosition.y >= Screen.height - panBorderThickness)
        //mouseposition returns..guess what? 0,0 is bottom right corner
        //Switched to GetAxis so we can change the keybindings, and it works with arrow keys too. Added > 0 to make it compatible with || operator
        {
            pos.z += panSpeed * Time.deltaTime;
            //time.deltatime because we want tiem of movement independent of framerate
        }
        if (Input.GetAxis("Vertical") < 0 || Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.GetAxis("Horizontal") < 0 || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;

        }
        if (Input.GetAxis("Horizontal") > 0 || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }


        #endregion panning code

        // Zoom (wheel)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;

        //Clamp values
        //pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        //pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;
    }
}
