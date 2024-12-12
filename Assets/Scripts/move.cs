using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 5f; // ���ӵ�ƽ���ٶ�
    public float moveDistance = 5f; // ����ƽ�Ƶľ���

    private Vector3 initialPosition; // ��ʼλ��
    private bool movingForward = true; // �����Ƿ������ƶ�

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
        // ������һ֡��λ��
        Vector3 nextPosition = transform.position + Vector3.forward * (movingForward ? 1 : -1) * moveSpeed * Time.deltaTime;

        // �ж��Ƿ�ﵽƽ�Ƶľ���
        float distanceToInitial = Vector3.Distance(nextPosition, initialPosition);
        if (distanceToInitial >= moveDistance)
        {
            // ����ﵽ���룬�ı䷽��
            movingForward = !movingForward;
        }

        // ����λ��
        transform.position = nextPosition;
    }
}