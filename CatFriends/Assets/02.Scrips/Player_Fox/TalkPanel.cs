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


    private void SetTalkPanel(FriendType friendType)
    {
        // ¾Æ´Ï ¶… ¸Å´ÏÀú¿¡¼­ °Á ÇÁ·»µå Á¤º¸´Â ÇÑ¹ø¸¸ °¡Àú¿À°Ô ÇØº¸Àð±Ã
    }
    public void SetName() => Name.text = friendInfo.frirendName;

}
