using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour
{
    public bool isMovable;
    private float moveSpeed = 1.5f;
    AnimatorSetting aniSetting;
    // Start is called before the first frame update
    private void Awake()
    {
        aniSetting = GetComponent<AnimatorSetting>();
    }
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "0_Hall") // 홀에서만 움직일 수 있음
            isMovable = true;
        else
            isMovable = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (isMovable) // 움직일 수 있을 때만 방향키 입력 받음, 도박장 밖의 홀에서는 키보드 움직임으로 애니메이터 제어, 도박장에서는 키보드x 게임 승패에 따라 매니저에 의해 제어
        {
            float horizon = Input.GetAxis("Horizontal");
        }
        */
    }

    private void FixedUpdate()
    {
        if (isMovable) // 움직일 수 있을 때만 방향키 입력 받음, 도박장 밖의 홀에서는 키보드 움직임으로 애니메이터 제어, 도박장에서는 키보드x 게임 승패에 따라 매니저에 의해 제어
        {
            float horizon = Input.GetAxis("Horizontal");
            PlayerMove(horizon, Time.fixedDeltaTime);
        }
    }

    private void PlayerMove(float h, float t) // horizontal, deltaTime
    {
        if(Mathf.Abs(h) >= 0.2) // 최소 입력값, 부드러운 입력에 대한 움직임 위해
        {
            if (!aniSetting.GetBool("isWalk")) // isWalk이 false이면 true로 바꿔줌, 걷는 애니메이션으로 변경
            {
                aniSetting.SetBool("isWalk", true);
                // 방향전환도 한번만 불러옴
                if(h > 0)
                    transform.eulerAngles = new Vector3(0, 90, 0);
                else
                    transform.eulerAngles = new Vector3(0, -90, 0);
            }
            if(h > 0) // 양수, x > 0으로 움직일 때
            {
                transform.position += new Vector3(1, 0, 0) * moveSpeed * t;
            }
            else // 음수, x < 0 으로 움직일 때
            {
                transform.position += new Vector3(-1, 0, 0) * moveSpeed * t;
            }
            
        }
        else // 최소 입력보다 작을 때, 즉 가만히 있을 때, 애니메이션 Idle, 
        {
            if (aniSetting.GetBool("isWalk")) // isWalk이 true이면 false로 바꿔줌, Idle 애니메이션으로 돌아옴
            {
                aniSetting.SetBool("isWalk", false);
                transform.eulerAngles = Vector3.zero;
            }
        }
    }
}
