using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Image))]
public class UIController_AbilityStateObserver : MonoBehaviour
{
    UnityEngine.UI.Image image_abilityIcon;
    Transform transform_text_cooldown;

    public PP.Game.Pawn_Character pawn_char;
    public int slotNum = 0;

    int rememberedRemainingSP = 0;

    private void Awake()
    {
        image_abilityIcon = transform.GetComponent<UnityEngine.UI.Image>();
        transform_text_cooldown = transform.Find("Text_Cooldown");
    }

    // Update is called once per frame
    void Update()
    {
        float timer = 0;
        if (pawn_char.abilities[slotNum].currentStock == 0)
            timer = pawn_char.abilities[slotNum].time_reload;
        else
            timer = pawn_char.abilities[slotNum].time_cooldown;

        if (timer > 0)
        {
            transform_text_cooldown.gameObject.SetActive(true);
            transform_text_cooldown.GetComponent<TMPro.TextMeshProUGUI>().text = timer.ToString("00.0");

            image_abilityIcon.color = Color.gray;
        }
        else
        {
            transform_text_cooldown.gameObject.SetActive(false);
            image_abilityIcon.color = Color.white;
        }

        if (rememberedRemainingSP != pawn_char.skillPoint)
        {
            if (pawn_char.skillPoint > 0 && pawn_char.abilities[slotNum].level < pawn_char.abilities[slotNum].level_max) ShowLevelUpButton();
            else HideLevelUpButton();
            rememberedRemainingSP = pawn_char.skillPoint;
        }
    }

    public void InjectSkillPoint()
    {
        if (pawn_char.skillPoint <= 0) return;
        if (pawn_char.abilities[slotNum].level >= pawn_char.abilities[slotNum].level_max) return;

        pawn_char.skillPoint--;
        pawn_char.abilities[slotNum].level++;
    }

    public void ShowLevelUpButton()
    {
        if (pawn_char.abilities[slotNum].level >= pawn_char.abilities[slotNum].level_max) return;

        transform.Find("Button_LevelUp").gameObject.SetActive(true);
    }
    public void HideLevelUpButton()
    {
        transform.Find("Button_LevelUp").gameObject.SetActive(false);
    }
}
