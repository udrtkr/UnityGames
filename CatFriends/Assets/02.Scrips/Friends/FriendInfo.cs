using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data_Friend", menuName = "ScriptableObject/CreateFriendInfo")]
public class FriendInfo : ScriptableObject
{
    public FriendType frirendType;
    public string frirendName;
    public string frirendDescription;
    public string frirendTitle;


}
public enum FriendType
{
    CatAluc,
    CatWhite,
    Tiger
}
