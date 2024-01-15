using FMOD;
using FSPRO;
using Microsoft.Xna.Framework.Audio;
using Newtonsoft.Json;
using RandallMod;
using System.Collections.Generic;

namespace RandallMod;

public sealed class ACompletePartial : CardAction
{

    //Action
        public override void Begin(G g, State s, Combat c)
    {
        if (s.ship.Get(ModInit.Instance.HalfDamageStatus.Status) == 1)
        {
            c.QueueImmediate([
                new AStatus()
                {
                    timer = 0.2,
                    targetPlayer = true,
                    status = ModInit.Instance.HalfDamageStatus.Status,
                    statusAmount = 1,
                }
            ]);
        }
        if (s.ship.Get(ModInit.Instance.HalfEvadeStatus.Status) == 1)
        {
            c.QueueImmediate([
                new AStatus()
                {
                    timer = 0.2,
                    targetPlayer = true,
                    status = ModInit.Instance.HalfEvadeStatus.Status,
                    statusAmount = 1,
                }
            ]);
        }
        if (s.ship.Get(ModInit.Instance.HalfShieldStatus.Status) == 1)
        {
            c.QueueImmediate([
                new AStatus()
                {
                    timer = 0.2,
                    targetPlayer = true,
                    status = ModInit.Instance.HalfShieldStatus.Status,
                    statusAmount = 1,
                }
            ]);
        }
        if (s.ship.Get(ModInit.Instance.HalfTempShieldStatus.Status) == 1)
        {
            c.QueueImmediate([
                new AStatus()
                {
                    timer = 0.2,
                    targetPlayer = true,
                    status = ModInit.Instance.HalfTempShieldStatus.Status,
                    statusAmount = 1,
                }
            ]);
        }
        if (s.ship.Get(ModInit.Instance.HalfCardStatus.Status) == 1)
        {
            c.QueueImmediate([
                new AStatus()
                {
                    timer = 0.2,
                    targetPlayer = true,
                    status = ModInit.Instance.HalfCardStatus.Status,
                    statusAmount = 1,
                }
            ]);
        }
        if (s.ship.Get(Status.energyFragment) > 0)
        {
            c.QueueImmediate([
                new AStatus()
                {
                    timer = 0.2,
                    targetPlayer = true,
                    status = Status.energyFragment,
                    statusAmount = 3 - s.ship.Get(Status.energyFragment),
                }
            ]);
        }
        if (s.ship.Get(ModInit.Instance.ChargeUpStatus.Status) > 0)
        {
            c.QueueImmediate([
                new AStatus()
                {
                    timer = 0.2,
                    targetPlayer = true,
                    status = ModInit.Instance.ChargeUpStatus.Status,
                    statusAmount = 3 - s.ship.Get(ModInit.Instance.ChargeUpStatus.Status) + s.ship.Get(ModInit.Instance.OverchargeStatus.Status),
                }
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