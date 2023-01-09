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

        if (Cam == null)
            Cam = GameObject.Find("Main Camera");
        if (ThiefCardsManager == null)
            ThiefCardsManager = GameObject.Find("ThiefCardsManager");
    }

    [SerializeField]
    GameObject Cam;
    Dictionary<string, Vector3> StartCamTransform = new Dictionary<string, Vector3>() { {"position" , new Vector3(0, 1.058f, -2.171f) }, {"eulerAngles", new Vector3(5.271f, 0, 0) } };
    Dictionary<string, Vector3> PlayCamTransform = new Dictionary<string, Vector3>() { { "position", new Vector3(1.12f, 1.42f, 0) }, { "eulerAngles", new Vector3(6.94f, 270, 0) } };

    [SerializeField]
    GameObject ThiefCardsManager;

    private void Reset()
    {
        Cam.transform.position = StartCamTransform["position"];
        Cam.transform.eulerAngles = StartCamTransform["eulerAngles"];

        ThiefCardsManager.GetComponent<ThiefCardsManager>().Reset();
        ThiefUI.Instance.Reset();
    }

    public void ThiefStart()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
