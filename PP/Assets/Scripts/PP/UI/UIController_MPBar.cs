using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController_MPBar : MonoBehaviour
{
    PP.Game.Capacitor memorialValue;
    public PP.Game.Caster caster_observing;

    private void Awake()
    {
        memorialValue = caster_observing.mp;
    }

    // Update is called once per frame
    void Update()
    {
        if (memorialValue.Equals(caster_observing.mp)) return;

        float n = (caster_observing.mp.current / caster_observing.mp.max);
        RectTransform rectTransform_parent = transform.parent.GetComponent<RectTransform>();
        RectTransform rectTransform = transform.Find("RawImage").GetComponent<RectTransform>();
        float width_max = rectTransform_parent.rect.width;
        rectTransform.sizeDelta = new Vector2(width_max*n, rectTransform.sizeDelta.y);

        memorialValue = caster_observing.mp;
    }
}
