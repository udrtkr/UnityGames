using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppHandMove : MonoBehaviour
{
    Transform tr;
    Vector3 rotori;
    public bool cutOK;
    private void Awake()
    {
        tr = GetComponent<Transform>();
        rotori = transform.eulerAngles;
    }

    public void HandMove(Vector3 ropePos)
    {
        StartCoroutine(E_HandMove(ropePos));
    }

    IEnumerator E_HandMove(Vector3 ropePos)
    {
        Vector3 dir = (ropePos - transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(dir);
        Vector3 dr = (rot.eulerAngles - tr.eulerAngles).normalized;

        while ((rot.eulerAngles - tr.eulerAngles).magnitude < 1f)
        {
            tr.eulerAngles += dr * Time.deltaTime * 3f;
            yield return null;
        }
        cutOK = true;
    }

    public void ResetHand()
    {
        cutOK = false;
        transform.eulerAngles = rotori;
    }

}
