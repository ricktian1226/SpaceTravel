using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

    //public Camera camera;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetZoomInSize()
    {
        Debug.LogFormat("SetZoomInSize to 5f");
        //camera.orthographicSize = 5f;
        Camera.main.orthographicSize = 5f;
    }

    public void SetZoomOutSize()
    {
        Debug.LogFormat("SetZoomOutSize to 10f");
        //camera.orthographicSize = 10f;
        Camera.main.orthographicSize = 10f;
    }
}
