using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaffold_Up : MonoBehaviour
{
    private float downPosY = 1.216f;
    public float a = 9.81f;
    public float v = 0f;

    public void ScaffoldUp_Down()
    {
        StartCoroutine(E_ScarroldUp_Down());
    }
    IEnumerator E_ScarroldUp_Down()
    {
        while(transform.position.y > downPosY)
        {
            transform.position -= new Vector3(0, (v + a*Time.deltaTime)*Time.deltaTime, 0);
            yield return new WaitForSeconds(0.001f);
        }
        // 칼 닿았을 때 인스턴스에 트루 전달
        // 그래서 칼 떨어졌을 때 손가락 잇으면 잘림
        // 아니면 걍 여기서 처리
        yield return new WaitForSeconds(3);
    }
}
