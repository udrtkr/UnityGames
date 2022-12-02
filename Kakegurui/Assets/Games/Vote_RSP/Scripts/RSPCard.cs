using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSPCard : MonoBehaviour
{
    private float moveSpeed = 0.025f;
    private Vector3 eulerTableCard = new Vector3 (180,-90,0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void MoveToVoteBox(Vector3 voteBox) // voteBox�� ī�� �̵��ϴ� �޼���
    {
        Vector3 dir = (voteBox - transform.position).normalized;
        StartCoroutine(E_MoveToVoteBox(dir, voteBox));
    }

    IEnumerator E_MoveToVoteBox(Vector3 diretion, Vector3 voteBox)
    {
        while((transform.position-voteBox).magnitude > 0.03)
        {
            transform.position += diretion * moveSpeed;
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;   
    }

    public void MoveToTable(Vector3 tablenum) // voteBox���� ���̺�� ī�� �����ϴ� �޼���
    {
        transform.eulerAngles = eulerTableCard;
        Vector3 dir = (tablenum - transform.position).normalized;
        StartCoroutine(E_MoveToTable(dir, tablenum));
    }
    IEnumerator E_MoveToTable(Vector3 direction , Vector3 tablenum)
    {
        while((transform.position - tablenum).magnitude > 0.05)
        {
            transform.position += direction * moveSpeed;
            yield return new WaitForSeconds(0.01f);
        }

        yield return null;
    }
    private void OnMouseEnter()
    {
        if (VoteRSP_Manager.Instance.SelectOK) // ī�� ���� ������ ��
        {
            // ī�������Ʈ Ƽ���� && �Ŵ����� ī�� ���� num++
            // ���� ���� num > 2 �̸� SelectOK = false
        }
    }
}
