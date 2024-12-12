using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontroller : MonoBehaviour
{
    // �ڳ������������������Ҫ���������ײ������
    public Transform tourCamera;
    #region ����ƶ�����
    public float moveSpeed = 10.0f;
    public float rotateSpeed = 150.0f;
    public float shiftRate = 2.0f;// ��סShift����
    public float minDistance = 0.5f;// ����벻�ɴ����ı������С���루С�ڵ���0ʱ�ɴ�͸�κα��棩
    #endregion
    #region �˶��ٶȺ���ÿ��������ٶȷ���
    private Vector3 direction = Vector3.zero;
    private Vector3 speedForward;
    private Vector3 speedBack;
    private Vector3 speedLeft;
    private Vector3 speedRight;
    private Vector3 speedUp;
    private Vector3 speedDown;
    #endregion
    void Start()
    {
        if (tourCamera == null) tourCamera = gameObject.transform;
    }
    void Update()
    {
        GetDirection();
        // ����Ƿ��벻�ɴ�͸�������
        RaycastHit hit;
        while (Physics.Raycast(tourCamera.position, direction, out hit, minDistance))
        {
            // ��ȥ��ֱ�ڲ��ɴ�͸������˶��ٶȷ���
            float angel = Vector3.Angle(direction, hit.normal);
            float magnitude = Vector3.Magnitude(direction) * Mathf.Cos(Mathf.Deg2Rad * (180 - angel));
            direction += hit.normal * magnitude;
        }
        if (tourCamera.localPosition.y > 3.3f)
        {
            tourCamera.localPosition = new Vector3(tourCamera.localPosition.x, 3.3f, tourCamera.localPosition.z);
        }
        if (tourCamera.localPosition.y < 2.8f)
        {
            tourCamera.localPosition = new Vector3(tourCamera.localPosition.x, 2.8f, tourCamera.localPosition.z);
        }
        tourCamera.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }
    private void GetDirection()
    {
        #region �����ƶ�
        if (Input.GetKeyDown(KeyCode.LeftShift)) moveSpeed *= shiftRate;
        if (Input.GetKeyUp(KeyCode.LeftShift)) moveSpeed /= shiftRate;
        #endregion
        #region �����ƶ�
        // ��λ
        speedForward = Vector3.zero;
        speedBack = Vector3.zero;
        speedLeft = Vector3.zero;
        speedRight = Vector3.zero;
        speedUp = Vector3.zero;
        speedDown = Vector3.zero;
        // ��ȡ��������
        if (Input.GetKey(KeyCode.W)) speedForward = tourCamera.forward;
        if (Input.GetKey(KeyCode.S)) speedBack = -tourCamera.forward;
        if (Input.GetKey(KeyCode.A)) speedLeft = -tourCamera.right;
        if (Input.GetKey(KeyCode.D)) speedRight = tourCamera.right;
        if (Input.GetKey(KeyCode.E)) speedUp = Vector3.up;
        if (Input.GetKey(KeyCode.Q)) speedDown = Vector3.down;
        direction = speedForward + speedBack + speedLeft + speedRight + speedUp + speedDown;
        #endregion
        #region �����ת
        if (Input.GetMouseButton(1))
        {
            // ת�������
            tourCamera.RotateAround(tourCamera.position, Vector3.up, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime);
            tourCamera.RotateAround(tourCamera.position, tourCamera.right, -Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime);
            // ת�˶��ٶȷ���
            direction = V3RotateAround(direction, Vector3.up, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime);
            direction = V3RotateAround(direction, tourCamera.right, -Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime);
        }
        #endregion
    }
    /// <summary>
    /// ����һ��Vector3����ת������תָ���ǶȺ����õ���������
    /// </summary>
    /// <param name="source">��תǰ��ԴVector3</param>
    /// <param name="axis">��ת��</param>
    /// <param name="angle">��ת�Ƕ�</param>
    /// <returns>��ת��õ�����Vector3</returns>
    public Vector3 V3RotateAround(Vector3 source, Vector3 axis, float angle)
    {
        Quaternion q = Quaternion.AngleAxis(angle, axis);// ��תϵ��
        return q * source;// ����Ŀ���
    }
}