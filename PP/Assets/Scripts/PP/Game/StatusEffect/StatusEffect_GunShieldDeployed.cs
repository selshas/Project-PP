public class StatusEffect_GunShieldDeployed : StatusEffect
{

    const float adjustValue = 0.25f;

    public StatusEffect_GunShieldDeployed(PP.Game.Pawn_Character _actor, float _duration) : base(_actor, _duration)
    { }

    public override void OnStart()
    {
        base.OnStart();
        actor.maxSpeed_mod -= adjustValue;
    }

    public override void OnSustain()
    {
        base.OnSustain();

        lifeTime = 10.0f;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        actor.maxSpeed_mod += adjustValue;
    }
}