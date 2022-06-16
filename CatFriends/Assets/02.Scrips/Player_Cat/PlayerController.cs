using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float runSpeed = 4f;
    private Vector3 _move;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void SetMove(float h, float v, float speed)
    {
        _move.x = h * speed;
        _move.y = v * speed;
    }

    private void Update()
    {
        float horiz = Input.GetAxis("Horizontal");
        float verti = Input.GetAxis("Vertacal");
    }

    private void FixedUpdate()
    {
        rb.position += _move * Time.fixedDeltaTime;
    }
}
