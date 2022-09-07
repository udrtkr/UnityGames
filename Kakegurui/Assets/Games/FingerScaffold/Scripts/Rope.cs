using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Color OriginColor;
    public Color ChoiceColor;

    private Renderer rend;
    public int ropeNum = -1;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        OriginColor = rend.material.color;
    }

    private void OnMouseEnter()
    {
        if (FingerScaffoldManager.instance.playerturn)
            rend.material.color = ChoiceColor;
    }
    private void OnMouseExit()
    {
        if (FingerScaffoldManager.instance.playerturn)
            rend.material.color = OriginColor;
    }

    private void OnMouseUp()
    {
        if (FingerScaffoldManager.instance.playerturn)
        {
            this.gameObject.SetActive(false);
            FingerScaffoldManager.instance.ropeNum = ropeNum;
            FingerScaffoldManager.instance.ScaffoldUp_Down();
            // FingerScaffoldManager.instance.playerturn = false;
        }
    }
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
