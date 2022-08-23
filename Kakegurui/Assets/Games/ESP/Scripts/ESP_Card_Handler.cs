using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESP_Card_Handler : MonoBehaviour
{
    RaycastHit hitLayerMask;

    private void OnMouseDrag()
    {
        if(ESPManager.Instance.moveOK)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 카메라에서 레이저 방출
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);

            int layerMask = 1 << LayerMask.NameToLayer("Table"); // 테이블 레이어 검출 위치 이동 위해
            if (Physics.Raycast(ray, out hitLayerMask, Mathf.Infinity, layerMask))
            {
                float y = this.transform.position.y; /* 높이 저장 */
                this.transform.position = new Vector3(hitLayerMask.point.x, y, hitLayerMask.point.z);
            }
        }
    }
}
