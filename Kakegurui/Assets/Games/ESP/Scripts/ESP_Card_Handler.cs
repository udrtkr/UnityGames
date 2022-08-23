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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ī�޶󿡼� ������ ����
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);

            int layerMask = 1 << LayerMask.NameToLayer("Table"); // ���̺� ���̾� ���� ��ġ �̵� ����
            if (Physics.Raycast(ray, out hitLayerMask, Mathf.Infinity, layerMask))
            {
                float y = this.transform.position.y; /* ���� ���� */
                this.transform.position = new Vector3(hitLayerMask.point.x, y, hitLayerMask.point.z);
            }
        }
    }
}
