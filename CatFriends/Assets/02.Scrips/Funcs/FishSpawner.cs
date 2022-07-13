using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    // Fishing Rod ���� ����� �Լ� ����
    // ����� �ɷ��� �� ������ ����� ��������

    private static FishSpawner _instance;
    public static FishSpawner instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<FishSpawner>("FishSpawner"));
            return _instance;
        }
    }
    [SerializeField] private List<GameObject> Fishes = new List<GameObject>();
    //private float a = -2f; // ����� ���ӵ�
    //private float v = 5f; // ����� �ʱ� �ӵ�


    public GameObject SpawnRandomFish()
    {
        int randomPercent = Random.Range(1, 101); // 1-100 ���� ����
        FishPercent fishPercent;
        List<GameObject> ranFishes = new List<GameObject>();
        int getRanFishindex;
        // ���� ������ ������ ���� fish�� enum percent ����
        if (randomPercent <= 3)
            fishPercent = FishPercent.per3;
        else if (randomPercent <= 10)
            fishPercent = FishPercent.per7;
        else if (randomPercent <= 40)
            fishPercent = FishPercent.per30;
        else
            fishPercent = FishPercent.per60;

        ranFishes = instance.Fishes.FindAll(x => x.GetComponent<Fish>().percent == fishPercent);
        getRanFishindex = Random.Range(0, ranFishes.Count);

        return Instantiate(ranFishes[getRanFishindex], transform.position, Quaternion.identity);
    }

    

    private void Start()
    {

    }

    public void Finish()
    {
        Destroy(gameObject);
    }
    }

    public enum FishPercent
{
    per60,
    per30,
    per7,
    per3,
}