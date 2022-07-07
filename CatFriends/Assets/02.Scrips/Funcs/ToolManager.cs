using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public static ToolManager instance;
    [SerializeField] List<GameObject> tools = new List<GameObject>();

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }

    public GameObject SpawnTool(FuncToolType funcToolType, Vector3 pos, Quaternion rot)
    {
        return Instantiate(tools.Find(x => x.GetComponent<FuncTool>().funcToolType == funcToolType) , pos,
                                rot);
    }


}
