using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private Rigidbody rb;
    public float Score = 0;

    public Camera cam;
    private UserGui ui;
    public float hitDisplayTime = 2f; // 提示显示时间
    public archercontroller archerControllerScript;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ui = cam.GetComponent<UserGui>();
        archerControllerScript = GameObject.FindObjectOfType<archercontroller>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("static target"))
        {
            ShowHitMessage("恭喜击中 加十分！");
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            ui.Score += 10;
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("moving target"))
        {
            ShowHitMessage("恭喜击中 加二十分！");
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            ui.Score += 20;
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            ShowHitMessage("很可惜 没击中");
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            Destroy(gameObject);
        }
    }
    void ShowHitMessage(string message)
    {
        StartCoroutine(DisplayHitMessage(message));
    }

    IEnumerator DisplayHitMessage(string message)
    {
        ui.SetHitMessage(message);
        yield return new WaitForSeconds(hitDisplayTime);
        ui.ClearHitMessage();
    }
}