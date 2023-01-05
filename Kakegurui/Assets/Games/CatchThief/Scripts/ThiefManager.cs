using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefManager : MonoBehaviour
{
    private static ThiefManager _instance;
    public static ThiefManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ThiefManager>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<ThiefManager>();
                }
            }

            return _instance;
        }

    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    GameObject ThiefCardsManager
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
