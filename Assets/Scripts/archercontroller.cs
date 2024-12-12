using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class archercontroller : MonoBehaviour
{
    Animator animator;
    bool isPulling = false;
    float pullDuration = 0f;
    public float maxPullDuration = 2f; // 最长拉弓时间
    public float arrowSpeed = 1f; // 箭矢速度
    public Transform firePoint;
    public Camera maincam;
    public int area1Shots = 10;
    public int area2Shots = 10;

    void Start()
    {
        // 获取弓上的Animator组件
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Isinarea())
        {
            // 当按下空格键时触发状态切换
            if (Input.GetKeyDown(KeyCode.Space))
            {
                pullDuration = 0;
                animator.SetBool("Fire", false);
                isPulling = true;
                animator.SetBool("isPulling", true);
            }

            // 持续按下空格键时记录按下时间
            if (isPulling)
            {
                pullDuration += Time.deltaTime;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                isPulling = false;
                animator.SetBool("isPulling", false);
            }

            // 当点击鼠标左键时触发射击
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 currentPosition = transform.position;
                Vector3 area1 = new Vector3(135f, 0f, 88f);
                Vector3 area2 = new Vector3(140f, 0f, 105f);
                float distance1 = Vector3.Distance(currentPosition, area1); // 计算目标点和玩家位置之间的距离
                float distance2 = Vector3.Distance(currentPosition, area2);
                float radius = 10f; // 圆的半径
                if (distance1 <= radius && area1Shots > 0)
                {
                    area1Shots--;
                    animator.SetBool("Fire", true);
                    Fire();
                }
                else if (distance2 <= radius && area2Shots > 0)
                {
                    area2Shots--;
                    animator.SetBool("Fire", true);
                    Fire();
                }
            }
        }
    }

    public void Fire()
    {
        GameObject arrow = Instantiate<GameObject>(Resources.Load<GameObject>("RyuGiKen/Crossbow/Prefabs/Arrow"));
        arrow.AddComponent<ArrowController>();
        ArrowController arrowController = arrow.GetComponent<ArrowController>();
        arrowController.cam = maincam;
        arrow.transform.position = firePoint.transform.position;

        // 设置弓箭的旋转为水平方向
        arrow.transform.rotation = firePoint.transform.rotation;

        Rigidbody rd = arrow.GetComponent<Rigidbody>();
        if (rd != null)
        {
            // 添加力到 Rigidbody
            Vector3 forwardDirection = arrow.transform.forward;
            rd.AddForce(forwardDirection * arrowSpeed, ForceMode.Impulse);
        }
    }

    bool Isinarea()
    {
        Vector3 currentPosition = transform.position;
        Vector3 area1 = new Vector3(135f, 0f, 88f);
        Vector3 area2 = new Vector3(140f, 0f, 105f);
        float distance1 = Vector3.Distance(currentPosition, area1); // 计算目标点和玩家位置之间的距离
        float distance2 = Vector3.Distance(currentPosition, area2);
        float radius = 10f; // 圆的半径

        if (distance1 <= radius || distance2 <= radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}