using UnityEngine;

/// <summary>
/// Class providing logic for playtime timer.
/// <remarks>
/// Timer used to display player time.
/// </remarks>
/// </summary>
public class PlaytimeTimer : MonoBehaviour {

    public float startTime;
    public string timerString;
    public bool start = false;
    private bool finnished = false;

    public void Finnish()
    {
        finnished = true;
    }

    public void StartTimer()
    {
        start = true;
        startTime = Time.time;
    }

    /// <summary>
    /// Function called every frame.
    /// <remarks>
    /// Builds timer string.
    /// </remarks>
    /// </summary>
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
}