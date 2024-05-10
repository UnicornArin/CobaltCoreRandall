namespace RandallMod;

internal sealed class TemplatyTemplate : Card
{
    public override CardData GetData(State state)
        => new()
        {
            cost = 1,
        };

    public override List<CardAction> GetActions(State s, Combat c)
        => [
            new AStatus
            {
                targetPlayer = true,
                status = Status.shield,
                statusAmount = 1
            },
        ];
}