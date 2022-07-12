using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController_EXPBar : MonoBehaviour
{
    int memorialValue;
    public PP.Game.EXPGatherer expGatherer;

    private void Awake()
    {
        memorialValue = expGatherer.exp;
    }

    // Update is called once per frame
    void Update()
    {
        if (memorialValue == expGatherer.exp) return;

        float n = ((float)expGatherer.exp / (float)expGatherer.CurrentCap);
        RectTransform rectTransform_parent = transform.parent.GetComponent<RectTransform>();
        RectTransform rectTransform = transform.Find("RawImage").GetComponent<RectTransform>();
        float width_max = rectTransform_parent.rect.width;
        rectTransform.sizeDelta = new Vector2(width_max*n, rectTransform.sizeDelta.y);

        memorialValue = expGatherer.exp;
    }
}
