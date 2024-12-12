using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 5f; // 靶子的平移速度
    public float moveDistance = 5f; // 靶子平移的距离

    private Vector3 initialPosition; // 初始位置
    private bool movingForward = true; // 靶子是否向右移动

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        MoveTarget();
    }

    void MoveTarget()
    {
        // 计算下一帧的位置
        Vector3 nextPosition = transform.position + Vector3.forward * (movingForward ? 1 : -1) * moveSpeed * Time.deltaTime;

        // 判断是否达到平移的距离
        float distanceToInitial = Vector3.Distance(nextPosition, initialPosition);
        if (distanceToInitial >= moveDistance)
        {
            // 如果达到距离，改变方向
            movingForward = !movingForward;
        }

        // 更新位置
        transform.position = nextPosition;
    }
}