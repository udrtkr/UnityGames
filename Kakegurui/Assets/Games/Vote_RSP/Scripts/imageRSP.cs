using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imageRSP : MonoBehaviour
{
    private Image image;
    private Color color; // 이미지 투명도 조절 위해
    private void Awake()
    {
        Reset();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Reset()
    {
        image = GetComponent<Image>();
        color = image.color;
        color.a = 0;
        GetComponent<Image>().color = color; // 처음 컬러 a 투명도 0으로 리셋
    }

    // Update is called once per frame
    void Update()
    {
        if(color.a < 1) // active 시 투명하게 시작하여 점점 그림 보이게
        {
            color.a += 0.0025f;
            GetComponent<Image>().color = color;
        }
    }

}
