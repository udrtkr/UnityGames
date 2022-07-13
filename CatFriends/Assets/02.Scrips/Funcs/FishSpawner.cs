using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    // Fishing Rod 에서 사용할 함수 만듦
    // 물고기 걸렸을 때 스폰할 물고기 랜덤으루

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
    //private float a = -2f; // 물고기 가속도
    //private float v = 5f; // 물고기 초기 속도


    public GameObject SpawnRandomFish()
    {
        int randomPercent = Random.Range(1, 101); // 1-100 랜덤 생성
        FishPercent fishPercent;
        List<GameObject> ranFishes = new List<GameObject>();
        int getRanFishindex;
        // 숫자 나오는 범위에 따라 fish의 enum percent 지정
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