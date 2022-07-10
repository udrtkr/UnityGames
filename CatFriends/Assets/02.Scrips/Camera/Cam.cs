using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    private Camera cam;
    private Color dayColor = new Color(134f / 255f, 238f / 255f, 255f / 255f, 0f);
    private Color nightColor = new Color(52f / 255f, 38f / 255f, 99f / 255f, 255f / 255f);
    [SerializeField]private float duration = 3f;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        cam.backgroundColor = dayColor;
    }
    protected virtual void Update()
    {
        ChangeDayColor(Time.time / 5f);
    }

    private void ChangeDayColor(float time)
    {
        float t = Mathf.PingPong(time, duration) / duration;
        cam.backgroundColor = Color.Lerp(dayColor, nightColor, t);
    }
}
