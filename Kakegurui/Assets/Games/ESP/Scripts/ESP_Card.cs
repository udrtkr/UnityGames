using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESP_Card : MonoBehaviour
{
    public ESPWho who;
    public int CardID;
    public ESPcardType cardType;
}
public enum ESPcardType
{
    Star,
    Circle,
    Wave,
    Rect,
    Cross
}

public enum ESPWho
{
    Player,
    Opp,
    Dealer
}
