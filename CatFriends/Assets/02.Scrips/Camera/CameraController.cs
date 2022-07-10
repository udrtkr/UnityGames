using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Cam
{
    public GameObject playerObject;

    private Transform trCam;
    private float y_posOrigin = 3.1f;
    private float y_posZoom = 1.3f;
    private float distanceZ_PlayerAndCam = -7f;
    private float distanceZ_PlayerAndCam_Zoom = -4f;
    private float moveSpeed = 0.3f;

    private void Start()
    {
        trCam = GetComponent<Transform>();
    }
    
    protected override void Update()
    {
        base.Update();
        CameraMove();
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
