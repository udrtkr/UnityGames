using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUp()
    {
        if(this.gameObject.activeSelf)
            UIVoteRSP.Instance.SetMirror(); // ��ư���� acitive true�� ���¿��� ���� ���
    }
}
