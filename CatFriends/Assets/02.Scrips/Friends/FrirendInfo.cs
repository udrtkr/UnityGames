using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data_Friend", menuName = "ScriptableObject/CreateFriendInfo")]
public class FrirendInfo : ScriptableObject
{
    public FrirendType frirendType;
    public string frirendName;
    public string frirendDescription;
    public string frirendTitle;


}
public enum FrirendType
{
    CatAluc,
    CatWhite,
    Tiger
}
