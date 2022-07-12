using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalTimer
{
    public static bool isPlaying = true;
    public static void Play()
    {
        Time.timeScale = 1.0f;
        isPlaying = true;
    }
    public static void Pause()
    {
        Time.timeScale = 0;
        isPlaying = false;
    }
    public static void Toggle()
    {
        if (isPlaying) Pause();
        else Play();
    }
}
