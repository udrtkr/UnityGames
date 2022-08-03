using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroomingHand : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private float xPointSpeed = 5; // ���콺 x �� �̵� �ӵ�
    [SerializeField] private float yPointSpeed = 5; // ���콺 y �� �̵� �ӵ�
    private float x, y, z; // ���콺 ��ġ
    private Transform tr;

    private void Awake()
    {
        tr = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player");
    }
    private void Start()
    {
        x = tr.position.x;
        y = tr.position.y;
        z = tr.position.z;
    }

    private void Update()
    {
        x += Input.GetAxis("Mouse X") * xPointSpeed * Time.deltaTime;
        y += Input.GetAxis("Mouse Y") * yPointSpeed * Time.deltaTime;

        Vector3 pos = Camera.main.WorldToViewportPoint(new Vector3(x, y, z));
        if(pos.x < 0f) pos.x = 0f;
        if(pos.x > 1f) pos.x = 1f;
        if(pos.y < 0f) pos.y = 0f;
        if(pos.y > 1f) pos.y = 1f;

        tr.position = Camera.main.ViewportToWorldPoint(pos);

        //tr.position = new Vector3(x, y, z);
    }
}
