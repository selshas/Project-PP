using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_Tether : StatusEffect
{
    public StatusEffect_Tether(PP.Game.Pawn_Character _actor, float _duration) : base(_actor, _duration)
    { }

    public override void OnStart()
    {
        base.OnStart();

        actor.animator.SetTrigger("PowerOff");
        actor.isActive = false;
    }

    public override void OnSustain()
    {
        base.OnSustain();
        actor.isActive = false;
    }

    public override void OnEnd()
    {
        actor.animator.SetTrigger("BackOnline");
        actor.isActive = true;
        base.OnEnd();
    }
}
