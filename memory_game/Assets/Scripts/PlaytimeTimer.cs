using UnityEngine;

public class PlaytimeTimer : MonoBehaviour {

    public float startTime;
    public string timerString;
    private bool finnished = false;
    public bool start = false;


    static int timerW = 90;
    static int timerH = 600;
    float halfScreenW = Screen.width * 0.5f;
    float halfScreenH = Screen.height * 0.5f;
    float halfTimerW = timerW * 0.5f;
    float halfTimerH = timerH * 0.5f;
    private GUIStyle guiStyle = new GUIStyle();

    void Update()
    {
        if (finnished)
            return;

        if (start)
        {
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString("00");
            string seconds = (t % 60).ToString("00");
            string milliseconds = ((t * 1000) % 99).ToString("00");

            timerString = minutes + ":" + seconds + ":" + milliseconds;
        }
        if(!start)
        timerString = "00:00:00";
    }

    public void Finnish()
    {
        finnished = true;
    }

    public void StartTimer()
    {
        start = true;
        startTime = Time.time;
    }

    public void DisplayTime()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        guiStyle.fontSize = 32;
        guiStyle.normal.textColor = Color.black;
        GUI.Label(new Rect(halfScreenW - halfTimerW,
            halfScreenH - halfTimerH, timerW, timerH), timerString, guiStyle);
        GUILayout.EndArea();
    }
}
