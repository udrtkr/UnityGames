using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject playerObject;

    private Transform trCam;
    private float y_posOrigin = 3.1f;
    private float y_posZoom = 1.3f;
    private float distanceZ_PlayerAndCam = -7f;
    private float distanceZ_PlayerAndCam_Zoom = -4f;
    private Camera cam;
    private float moveSpeed = 0.3f;
    private Color dayColor = new Color(134f/255f, 238f/255f, 255f/255f, 0f);
    private Color nightColor = new Color(52f/255f, 38f/255f, 99f/255f, 255f/255f);
    public float duration = 3000f;


    private void Awake()
    {
        trCam = GetComponent<Transform>();
        cam = GetComponent<Camera>();
       
    }

    private void Start()
    {
        cam.backgroundColor = dayColor;
    }

    private void Update()
    {
        CameraMove();
        // friendUI에서 talk 중일때 각도 조절
        /*
        if(FriendsUI.instance.isTalk)
            trCam.position = new Vector3(playerObject.transform.position.x, y_posZoom, playerObject.transform.position.z + distanceZ_PlayerAndCam_Zoom);
        else
            trCam.position = new Vector3(playerObject.transform.position.x, y_posOrigin, playerObject.transform.position.z + distanceZ_PlayerAndCam);
        */
        ChangeDayColor(Time.time / 5f);
    }

    private void ChangeDayColor(float time)
    {
        float t = Mathf.PingPong(time, duration) / duration;
        cam.backgroundColor = Color.Lerp(dayColor, nightColor, t);
    }

    private void CameraMove()
    {
        Vector3 currentPos = trCam.position;
        Vector3 originPos = new Vector3(playerObject.transform.position.x, y_posOrigin, playerObject.transform.position.z + distanceZ_PlayerAndCam);
        Vector3 zoomPos = new Vector3(playerObject.transform.position.x, y_posZoom, playerObject.transform.position.z + distanceZ_PlayerAndCam_Zoom);

        if (currentPos != zoomPos && FriendsUI.instance.isTalk)
            trCam.position = Vector3.MoveTowards(currentPos, zoomPos, moveSpeed);
        else if (currentPos == zoomPos && FriendsUI.instance.isTalk)
            trCam.position = zoomPos;
        else if (currentPos != originPos && !FriendsUI.instance.isTalk)
            trCam.position = Vector3.MoveTowards(currentPos, originPos, moveSpeed);
        else if (currentPos == originPos && !FriendsUI.instance.isTalk)
            trCam.position = originPos;
    }
    
}
