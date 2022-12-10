using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallCamHandler : MonoBehaviour
{
    [SerializeField]
    GameObject Player;

    [SerializeField]
    Vector3 posWithPlayer = new Vector3(0, 2.75f, -3.33f);

    float maxX = 11f; // ī�޶� �� �� �ִ� �ִ� x �Ÿ�
    float minX = -10f; // ī�޶� �� �� �ִ� �ּ� x �Ÿ� -> ���� �� ���̴� ����
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
        if(Player.transform.position.x >= maxX) // �÷��̾� ��ġ�� �ִ�, �ּ� ķ x�Ÿ� ����� ī�޶�� �״��
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
