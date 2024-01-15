using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandallMod
{
    public partial interface IKokoroApi
    {
        IActionApi Actions { get; }
        void RegisterStatusRenderHook(IStatusRenderHook hook, double priority);

        public interface IActionApi
        {
            CardAction MakeHidden(CardAction action, bool showTooltips = false);
        }
        
    }
    public interface IStatusRenderHook
    {
        List<Tooltip> OverrideStatusTooltips(Status status, int amount, Ship? ship, List<Tooltip> tooltips);
    }
}
