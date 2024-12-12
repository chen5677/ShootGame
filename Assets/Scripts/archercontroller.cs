using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class archercontroller : MonoBehaviour
{
    Animator animator;
    bool isPulling = false;
    float pullDuration = 0f;
    public float maxPullDuration = 2f; // �����ʱ��
    public float arrowSpeed = 1f; // ��ʸ�ٶ�
    public Transform firePoint;
    public Camera maincam;
    public int area1Shots = 10;
    public int area2Shots = 10;

    void Start()
    {
        // ��ȡ���ϵ�Animator���
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Isinarea())
        {
            // �����¿ո��ʱ����״̬�л�
            if (Input.GetKeyDown(KeyCode.Space))
            {
                pullDuration = 0;
                animator.SetBool("Fire", false);
                isPulling = true;
                animator.SetBool("isPulling", true);
            }

            // �������¿ո��ʱ��¼����ʱ��
            if (isPulling)
            {
                pullDuration += Time.deltaTime;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                isPulling = false;
                animator.SetBool("isPulling", false);
            }

            // �����������ʱ�������
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 currentPosition = transform.position;
                Vector3 area1 = new Vector3(135f, 0f, 88f);
                Vector3 area2 = new Vector3(140f, 0f, 105f);
                float distance1 = Vector3.Distance(currentPosition, area1); // ����Ŀ�������λ��֮��ľ���
                float distance2 = Vector3.Distance(currentPosition, area2);
                float radius = 10f; // Բ�İ뾶
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

        // ���ù�������תΪˮƽ����
        arrow.transform.rotation = firePoint.transform.rotation;

        Rigidbody rd = arrow.GetComponent<Rigidbody>();
        if (rd != null)
        {
            // ������� Rigidbody
            Vector3 forwardDirection = arrow.transform.forward;
            rd.AddForce(forwardDirection * arrowSpeed, ForceMode.Impulse);
        }
    }

    bool Isinarea()
    {
        Vector3 currentPosition = transform.position;
        Vector3 area1 = new Vector3(135f, 0f, 88f);
        Vector3 area2 = new Vector3(140f, 0f, 105f);
        float distance1 = Vector3.Distance(currentPosition, area1); // ����Ŀ�������λ��֮��ľ���
        float distance2 = Vector3.Distance(currentPosition, area2);
        float radius = 10f; // Բ�İ뾶

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