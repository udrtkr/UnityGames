using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallCamHandler : MonoBehaviour
{
    [SerializeField]
    GameObject Player;

    [SerializeField]
    Vector3 posWithPlayer = new Vector3(0, 2.75f, -3.33f);

    float maxX = 11f; // 카메라가 갈 수 있는 최대 x 거리
    float minX = -10f; // 카메라가 갈 수 있는 최소 x 거리 -> 복도 끝 보이는 지점
    private void Awake()
    {
        if(Player == null)
        {
            Player = GameObject.Find("Player");
        }

        transform.position = Player.transform.position + posWithPlayer;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CamMove();
    }

    private void CamMove()
    {
        if(Player.transform.position.x >= maxX) // 플레이어 위치가 최대, 최소 캠 x거리 벗어나면 카메라는 그대로
        {
            transform.position = new Vector3(maxX, posWithPlayer.y, posWithPlayer.z);
        }
        else if(Player.transform.position.x <= minX)
        {
            transform.position = new Vector3(minX, posWithPlayer.y, posWithPlayer.z);
        }
        else
            transform.position = Player.transform.position + posWithPlayer;
    }
}
