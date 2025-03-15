namespace RandallMod;

public sealed class ACompletePartial : CardAction
{

    //Action
        public override void Begin(G g, State s, Combat c)
    {
        if (s.ship.Get(ModInit.Instance.HalfDamageStatus.Status) == 1)
        {
            c.QueueImmediate([
                new AHurt(){
                    timer = 0.5,
                    hurtAmount = 1,
                    targetPlayer = true,
                }
            ]);
        }
        if (s.ship.Get(ModInit.Instance.HalfEvadeStatus.Status) == 1)
        {
            c.QueueImmediate([
                new AStatus()
                {
                    timer = 0.5,
                    targetPlayer = true,
                    status = Status.evade,
                    statusAmount = 1,
                }
            ]);
        }
        if (s.ship.Get(ModInit.Instance.HalfShieldStatus.Status) == 1)
        {
            c.QueueImmediate([
                new AStatus()
                {
                    timer = 0.5,
                    targetPlayer = true,
                    status = Status.shield,
                    statusAmount = 1,
                }
            ]);
        }
        if (s.ship.Get(ModInit.Instance.HalfTempShieldStatus.Status) == 1)
        {
            c.QueueImmediate([
                new AStatus()
                {
                    timer = 0.5,
                    targetPlayer = true,
                    status = Status.tempShield,
                    statusAmount = 1,
                }
            ]);
        }
        if (s.ship.Get(ModInit.Instance.HalfCardStatus.Status) == 1)
        {
            c.QueueImmediate([
                new ADrawCard(){
                    count = 1,
                    timer = 0.5,
                }
            ]);
        }
        if (s.ship.Get(Status.energyFragment) > 0)
        {
            c.QueueImmediate([
                new AEnergy()
                {
                    timer = 0.5,
                    changeAmount = 1,
                }
            ]);
        }
        if (s.ship.Get(ModInit.Instance.ChargeUpStatus.Status) > 0)
        {
            c.QueueImmediate([
                new AAttack() {
                    timer = 0.5,
                    damage = Card.GetActualDamage(g.state, 1 + g.state.ship.Get(ModInit.Instance.OverchargeStatus.Status))
                },
            ]);
        }
    }


    //Tooltip thing
    /*
    public override List<Tooltip> GetTooltips(State s)
    {
        if (s.route is Combat combat && combat.stuff.TryGetValue(WorldX, out var @object))
            @object.hilight = 2;

        return [
            new CustomTTGlossary(
                CustomTTGlossary.GlossaryType.action,
                () => StableSpr.icons_droneFlip,
                () => Loc.T("action.ASynergize.name"),
                () => Loc.T("action.ASynergize.desc")
            )
        ];
    }*/

    //This requires no icon, as it will have no display ever
    /*public override Icon? GetIcon(State s)
        => new(ModInit.Instance.IconSynzergize.Sprite, Count, Colors.textMain);*/
}