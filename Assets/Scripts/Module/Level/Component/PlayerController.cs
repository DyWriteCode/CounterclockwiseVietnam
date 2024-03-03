using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 选关界面人物控制器
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public Animator ani;

    private void Start()
    {
        ani = GetComponent<Animator>();
        GameApp.CameraManager.SetPos(transform.position); // 设置摄像机位置
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (h == 0)
        {
            ani.Play("idle");
        }
        else
        {
            if ((h > 0 && transform.localScale.x < 0) || (h < 0 && transform.localScale.x > 0))
            {
                Flip();
            }
            // -32 to 24
            Vector3 pos = transform.position + Vector3.right * h * moveSpeed * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, -32, 24);
            transform.position = pos; // 限制移动范围
            GameApp.CameraManager.SetPos(transform.position); // 设置摄像机位置, 跟随人物
            ani.Play("move");
        }
    }

    // 人物转向
    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale; 
    }
}
