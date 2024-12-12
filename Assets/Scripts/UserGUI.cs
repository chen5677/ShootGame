using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGui : MonoBehaviour
{
    // Start is called before the first frame update
    public float Score = 0;
    public archercontroller archerControllerScript;
    private string hitMessage = "";
    void Start()
    {
        archerControllerScript = GameObject.FindObjectOfType<archercontroller>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = Color.red;

        // 在屏幕右上角显示分数
        GUI.Label(new Rect(Screen.width - 150, 20, 150, 30), "Score：" + Score, style);

        // 显示射击次数
        style.fontSize = 18;
        style.normal.textColor = Color.blue;
        GUI.Label(new Rect(Screen.width - 150, Screen.height - 50, 150, 30), "Area 2 Shots: " + archerControllerScript.area2Shots, style);
        GUI.Label(new Rect(Screen.width - 150, Screen.height - 80, 150, 30), "Area 1 Shots: " + archerControllerScript.area1Shots, style);

        GUI.Label(new Rect(50, Screen.height - 50, 150, 30), hitMessage, style);
    }
    public void SetHitMessage(string message)
    {
        hitMessage = message;
    }

    public void ClearHitMessage()
    {
        hitMessage = "";
    }
}