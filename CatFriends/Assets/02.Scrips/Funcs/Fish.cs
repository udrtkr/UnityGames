using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public FishType fishType;
    public string fishName;
    public FishPercent percent; // 1-100 까지 표현인데 구간 나누자 타입별로 퍼센트 마다 딕셔너리 만들어서 키 퍼센트 밸류 물고기 배열루 해서 랜덤값 받아서 ㅎ
                        // 60퍼:v1,v2  30퍼:v3  7퍼:v4  3퍼 샤크
    public int sellPrice;

}
public enum FishType
{
    FishV1,
    FishV2,
    FishV3,
    FishV4,
    SharkV1,
}

public enum FishPercent
{
    per60,
    per30,
    per7,
    per3,
}

