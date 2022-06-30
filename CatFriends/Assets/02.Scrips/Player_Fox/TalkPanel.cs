using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkPanel : MonoBehaviour
{
    public FriendInfo friendInfo;
    [SerializeField] private Text Name;
    [SerializeField] private Text Story;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void SetName() => Name.text = friendInfo.frirendName;

}
