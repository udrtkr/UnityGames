using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private FishType fishType;
    [SerializeField] private string fishName;
    public FishPercent percent; // 1-100 ���� ǥ���ε� ���� ������ Ÿ�Ժ��� �ۼ�Ʈ ���� ��ųʸ� ���� Ű �ۼ�Ʈ ��� ������ �迭�� �ؼ� ������ �޾Ƽ� ��
                                // 60��:v1,v2  30��:v3  7��:v4  3�� ��ũ
    public int touchNum;
    [SerializeField] private int sellPrice;

}
public enum FishType
{
    FishV1,
    FishV2,
    FishV3,
    FishV4,
    SharkV1,
}

