namespace RandallMod
{
    public partial interface IKokoroApi
    {
        Color DefaultActiveStatusBarColor { get; }
        Color DefaultInactiveStatusBarColor { get; }

        IActionApi Actions { get; }
        void RegisterStatusRenderHook(IStatusRenderHook hook, double priority);

        public interface IActionApi
        {
            CardAction MakeHidden(CardAction action, bool showTooltips = false);
        }
        
    }
    public interface IStatusRenderHook
    {
        List<Tooltip> OverrideStatusTooltips(Status status, int amount, Ship? ship, List<Tooltip> tooltips) => tooltips;
        bool? ShouldOverrideStatusRenderingAsBars(State state, Combat combat, Ship ship, Status status, int amount) => null;
        (IReadOnlyList<Color> Colors, int? BarTickWidth) OverrideStatusRendering(State state, Combat combat, Ship ship, Status status, int amount) => new();
    }


}
