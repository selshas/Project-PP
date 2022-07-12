using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController_HPBar : MonoBehaviour
{
    PP.Game.Capacitor memorialValue;
    public PP.Game.Damagable caster_observing;

    private void Awake()
    {
        memorialValue = caster_observing.hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (memorialValue.Equals(caster_observing.hp)) return;

        float n = (caster_observing.hp.current / caster_observing.hp.max);
        RectTransform rectTransform_parent = transform.parent.GetComponent<RectTransform>();
        RectTransform rectTransform = transform.Find("RawImage").GetComponent<RectTransform>();
        float width_max = rectTransform_parent.rect.width;
        rectTransform.sizeDelta = new Vector2(width_max*n, rectTransform.sizeDelta.y);

        memorialValue = caster_observing.hp;
    }
}
