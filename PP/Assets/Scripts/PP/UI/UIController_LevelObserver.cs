using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController_LevelObserver : MonoBehaviour
{
    public PP.Game.EXPGatherer expGatherer;

    int memorizedLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        memorizedLevel = expGatherer.level;
        transform.Find("Text_Level").GetComponent<TMPro.TextMeshProUGUI>().text = (expGatherer.level).ToString("00");
    }

    // Update is called once per frame
    void Update()
    {
        if (memorizedLevel == expGatherer.level) return;

        transform.Find("Text_Level").GetComponent<TMPro.TextMeshProUGUI>().text = (expGatherer.level).ToString("00");
        memorizedLevel = expGatherer.level;
    }
}
