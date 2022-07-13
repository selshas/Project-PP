public abstract class StatusEffect : IStatusEffect
{
    public int index = -1;
    protected PP.Game.Pawn_Character actor;
    public bool isDisplayingOnHud { get; protected set; } = false;
    public float lifeTime;
    public bool isSustained = false;

    public StatusEffect(PP.Game.Pawn_Character _actor, float duration)
    {
        actor = _actor;
        lifeTime = duration;
    }

    public void Tick() 
    {
        lifeTime -= UnityEngine.Time.deltaTime;
        if (lifeTime <= 0) OnEnd();
    }

    public virtual void OnStart()
    {
        isSustained = true;
    }
    public virtual void OnSustain()
    {
    }
    public virtual void OnEnd()
    {
        isSustained = false;
    }
}
