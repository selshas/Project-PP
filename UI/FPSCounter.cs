using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class FPSCounter : MonoBehaviour
{
    int i = 0;
    float[] fpsHistories;
    TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        fpsHistories = new float[Application.targetFrameRate];
    }

    // Update is called once per frame
    void Update()
    {
        fpsHistories[i] = (1 / Time.deltaTime);
        textMeshProUGUI.text = $"FPS: {(int)fpsHistories.Average()}";
        i = (i + 1) % fpsHistories.Length;
    }
}
