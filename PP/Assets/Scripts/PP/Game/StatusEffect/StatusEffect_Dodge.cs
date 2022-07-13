using UnityEngine;

public class StatusEffect_Dodge : StatusEffect
{
    int layer_original;

    public StatusEffect_Dodge(PP.Game.Pawn_Character _actor, float duration) : base(_actor, duration)
    {}

    public override void OnStart()
    {
        base.OnStart();

        layer_original = actor.gameObject.layer;
        actor.gameObject.layer = 11;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        actor.gameObject.layer = layer_original;
    }
}
