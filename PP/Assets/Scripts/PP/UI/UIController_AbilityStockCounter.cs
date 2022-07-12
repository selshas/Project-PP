using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class UIController_AbilityStockCounter : MonoBehaviour
{
    public UIController_AbilityStateObserver uiController_abilityStateObserver;

    public string path_tableTexture = "Textures/UI/AbilityStockCounter";
    public Dictionary<int, List<Sprite>> dict_sprites;

    public Image img_sprite;

    int memorizedStockCount = 0;
    int memorizedAbilityLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        dict_sprites = new Dictionary<int, List<Sprite>>();
        Sprite[] sprites = Resources.LoadAll<Sprite>(path_tableTexture);
        
        for (int i = 1; i <= 5; i++)
        {
            int s = 0;
            for (int k = i; k > 0; k--) s += k;
            dict_sprites[i] = new List<Sprite>();

            for (int j = 0; j <= i; j++)
            {
                dict_sprites[i].Add(sprites[s+j-1]);
            }
        }

        img_sprite = GetComponent<Image>();

        PP.Ability ability = uiController_abilityStateObserver.pawn_char.abilities[uiController_abilityStateObserver.slotNum];
        memorizedStockCount = ability.currentStock;
        img_sprite.sprite = dict_sprites[ability.maxStock[ability.level]][ability.currentStock];
        memorizedStockCount = ability.currentStock;
    }

    // Update is called once per frame
    void Update()
    {
        PP.Ability ability = uiController_abilityStateObserver.pawn_char.abilities[uiController_abilityStateObserver.slotNum];
        if (ability.maxStock[ability.level] == 1)
            img_sprite.enabled = false;
        if (memorizedStockCount != ability.currentStock || memorizedAbilityLevel != ability.level)
        {
            img_sprite.sprite = dict_sprites[ability.maxStock[ability.level]][ability.currentStock];
            memorizedStockCount = ability.currentStock;
        }
    }
}
