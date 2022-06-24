using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject playerObject;

    private Transform trCam;
    private float y_posOrigin = 3.1f;
    private float distanceZ_PlayerAndCam = -7f;
    private float distanceZ_PlayerAndCam_Grooming = 4f;
    private Camera cam;
    private Color dayColor;
    private Color nightColor;

    private void Awake()
    {
        trCam = GetComponent<Transform>();
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        trCam.position = new Vector3(playerObject.transform.position.x, y_posOrigin, playerObject.transform.position.z + distanceZ_PlayerAndCam);
    }

    
}
