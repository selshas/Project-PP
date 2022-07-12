using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController_SkillPointObserver : MonoBehaviour
{
    public PP.Game.Pawn_Character pawn_target;
    TMPro.TextMeshProUGUI text;

    int memorizedValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        text = transform.Find("Text").GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pawn_target.skillPoint == memorizedValue) return;

        text.text = pawn_target.skillPoint.ToString();
        memorizedValue = pawn_target.skillPoint;

        if (memorizedValue == 0) GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, 0);
        else GetComponent<UnityEngine.UI.Image>().color = Color.white;
    }
}
