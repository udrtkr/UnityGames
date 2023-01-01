using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// �� ���� Ư¡�� ���� ��ũ��Ʈ
// �ݶ��̴� ������Ʈ �����ͼ� ó��
public class Doorway : MonoBehaviour
{
    public string gameName;
    public string sceneName;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // UI_Door �������� �̵���Ŵ
            UI_Door.Instance.SetPosition(transform.position);
            // UI_Door ���� ���θŴ����� ������ �޼��� ������ Ŭ������ ����, UI_Door�� ���� ���� �� Ŭ���� �� ����
            UI_Door.Instance.SetGameNameAndSceneName(gameName, sceneName);
            // ���� ������ �� ������� UI�� �ߴ� ���� �̸�, �� �̸� ����, ���� �̸��� ��Ÿ����
        }
        Debug.Log("����");
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            UI_Door.Instance.SetPosition(new Vector3(999,999,999)); // �� ���� ������ ������ �Ⱥ��̰� �ָ� ��������
            UI_Door.Instance.SetGameNameAndSceneName("", ""); // �ʱ�ȭ
        }
    }
}
