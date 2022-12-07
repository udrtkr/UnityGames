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
            UIVoteRSP.Instance.SetMirror(); // 버튼으로 acitive true된 상태에서 누를 경우
    }
}
