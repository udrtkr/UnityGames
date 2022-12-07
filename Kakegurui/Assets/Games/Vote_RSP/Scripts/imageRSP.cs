using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imageRSP : MonoBehaviour
{
    private Image image;
    private Color color; // �̹��� ���� ���� ����
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
        GetComponent<Image>().color = color; // ó�� �÷� a ���� 0���� ����
    }

    // Update is called once per frame
    void Update()
    {
        if(color.a < 1) // active �� �����ϰ� �����Ͽ� ���� �׸� ���̰�
        {
            color.a += 0.0025f;
            GetComponent<Image>().color = color;
        }
    }

}
