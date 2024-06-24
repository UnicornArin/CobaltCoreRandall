using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandallMod.Jester;

internal class RandallJesterProvider : IJesterApi.IProvider
{
    public IEnumerable<(double, IJesterApi.IEntry)> GetEntries(IJesterApi.IJesterRequest request)
    {
        List<(double, IJesterApi.IEntry)> ProviderList = new List<(double, IJesterApi.IEntry)>();
        if (!ModInit.Instance.JesterApi!.HasCardFlag("exhaust", request))
        {
            //Half Statuses
            ProviderList.Add((1, new JesterHalfEvade()));
            ProviderList.Add((1, new JesterHalfDamage()));
            ProviderList.Add((1, new JesterHalfCard()));
            ProviderList.Add((1, new JesterHalfShield()));
            ProviderList.Add((1, new JesterHalfTemp()));
            //Actions
            ProviderList.Add((1, new JesterSynergize()
            {
                Count = 2
            }));
        } else {
            //Big statuses
            ProviderList.Add((1, new JesterCoPilot()));
            ProviderList.Add((1, new JesterArchive()));
            ProviderList.Add((1, new JesterAuxShields()));
        }
        return ProviderList;
    }
}

internal class JesterHalfEvade : IJesterApi.IEntry
{
    public IReadOnlySet<string> Tags => new HashSet<string> { "defensive", "status", "halfEvade" };

    public void AfterSelection(IJesterApi.IJesterRequest request)
    {
        request.Blacklist.Add("halfEvade");
    }

    public IEnumerable<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = [];

        actions.Add(
        new AStatus
        {
            targetPlayer = true,
            status = ModInit.Instance.HalfEvadeStatus.Status,
            statusAmount = 1
        });

        return actions;
    }

    public int GetCost()
    {
        return 4;
    }

    public IEnumerable<(double, IJesterApi.IEntry)> GetUpgradeOptions(IJesterApi.IJesterRequest request, Upgrade upDir)
    {
        return new List<(double, IJesterApi.IEntry)>();
    }
}

internal class JesterHalfShield : IJesterApi.IEntry
{
    public IReadOnlySet<string> Tags => new HashSet<string> { "defensive", "status", "halfShield" };

    public void AfterSelection(IJesterApi.IJesterRequest request)
    {
        request.Blacklist.Add("halfShield");
    }

    public IEnumerable<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = [];

        actions.Add(
        new AStatus
        {
            targetPlayer = true,
            status = ModInit.Instance.HalfShieldStatus.Status,
            statusAmount = 1
        });

        return actions;
    }

    public int GetCost()
    {
        return 4;
    }

    public IEnumerable<(double, IJesterApi.IEntry)> GetUpgradeOptions(IJesterApi.IJesterRequest request, Upgrade upDir)
    {
        return new List<(double, IJesterApi.IEntry)>();
    }
}

internal class JesterHalfTemp : IJesterApi.IEntry
{
    public IReadOnlySet<string> Tags => new HashSet<string> { "defensive", "status", "halfTemp" };

    public void AfterSelection(IJesterApi.IJesterRequest request)
    {
        request.Blacklist.Add("halfTemp");
    }

    public IEnumerable<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = [];

        actions.Add(
        new AStatus
        {
            targetPlayer = true,
            status = ModInit.Instance.HalfTempShieldStatus.Status,
            statusAmount = 1
        });

        return actions;
    }

    public int GetCost()
    {
        return 2;
    }

    public IEnumerable<(double, IJesterApi.IEntry)> GetUpgradeOptions(IJesterApi.IJesterRequest request, Upgrade upDir)
    {
        return new List<(double, IJesterApi.IEntry)>();
    }
}

internal class JesterHalfCard : IJesterApi.IEntry
{
    public IReadOnlySet<string> Tags => new HashSet<string> { "utility", "status", "halfCard" };

    public void AfterSelection(IJesterApi.IJesterRequest request)
    {
        request.Blacklist.Add("halfCard");
    }

    public IEnumerable<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = [];

        actions.Add(
        new AStatus
        {
            targetPlayer = true,
            status = ModInit.Instance.HalfCardStatus.Status,
            statusAmount = 1
        });

        return actions;
    }

    public int GetCost()
    {
        return 4;
    }

    public IEnumerable<(double, IJesterApi.IEntry)> GetUpgradeOptions(IJesterApi.IJesterRequest request, Upgrade upDir)
    {
        return new List<(double, IJesterApi.IEntry)>();
    }
}

internal class JesterHalfDamage : IJesterApi.IEntry
{
    public IReadOnlySet<string> Tags => new HashSet<string> { "cost", "status", "halfDamage" };

    public void AfterSelection(IJesterApi.IJesterRequest request)
    {
        request.Blacklist.Add("halfDamage");
    }

    public IEnumerable<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = [];

        actions.Add(
        new AStatus
        {
            targetPlayer = true,
            status = ModInit.Instance.HalfDamageStatus.Status,
            statusAmount = 1
        });

        return actions;
    }

    public int GetCost()
    {
        return -5;
    }

    public IEnumerable<(double, IJesterApi.IEntry)> GetUpgradeOptions(IJesterApi.IJesterRequest request, Upgrade upDir)
    {
        return new List<(double, IJesterApi.IEntry)>();
    }
}

internal class JesterCoPilot : IJesterApi.IEntry
{
    public IReadOnlySet<string> Tags => new HashSet<string> { "utility", "status", "coPilot" };

    public void AfterSelection(IJesterApi.IJesterRequest request)
    {
        request.Blacklist.Add("coPilot");
    }

    public IEnumerable<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = [];

        actions.Add(
        new AStatus
        {
            targetPlayer = true,
            status = ModInit.Instance.CoPilotStatus.Status,
            statusAmount = 1
        });

        return actions;
    }

    public int GetCost()
    {
        return 73;
    }

    public IEnumerable<(double, IJesterApi.IEntry)> GetUpgradeOptions(IJesterApi.IJesterRequest request, Upgrade upDir)
    {
        return new List<(double, IJesterApi.IEntry)>();
    }
}

internal class JesterAuxShields : IJesterApi.IEntry
{
    public IReadOnlySet<string> Tags => new HashSet<string> { "defensive", "status", "auxShields" };

    public void AfterSelection(IJesterApi.IJesterRequest request)
    {
        request.Blacklist.Add("auxShields");
    }

    public IEnumerable<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = [];

        actions.Add(
        new AStatus
        {
            targetPlayer = true,
            status = ModInit.Instance.AuxiliaryShieldsStatus.Status,
            statusAmount = 1
        });

        return actions;
    }

    public int GetCost()
    {
        return 73;
    }

    public IEnumerable<(double, IJesterApi.IEntry)> GetUpgradeOptions(IJesterApi.IJesterRequest request, Upgrade upDir)
    {
        return new List<(double, IJesterApi.IEntry)>();
    }
}

internal class JesterArchive : IJesterApi.IEntry
{
    public IReadOnlySet<string> Tags => new HashSet<string> { "utility", "status", "archive" };

    public void AfterSelection(IJesterApi.IJesterRequest request)
    {
        request.Blacklist.Add("archive");
    }

    public IEnumerable<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = [];

        actions.Add(
        new AStatus
        {
            targetPlayer = true,
            status = ModInit.Instance.ArchiveStatus.Status,
            statusAmount = 1
        });

        return actions;
    }

    public int GetCost()
    {
        return 73;
    }

    public IEnumerable<(double, IJesterApi.IEntry)> GetUpgradeOptions(IJesterApi.IJesterRequest request, Upgrade upDir)
    {
        return new List<(double, IJesterApi.IEntry)>();
    }
}

internal class JesterSynergize : IJesterApi.IEntry
{
    public int Count { get; init; }
    public IReadOnlySet<string> Tags => new HashSet<string> { "offensive", "status", "synergize" };

    public void AfterSelection(IJesterApi.IJesterRequest request)
    {
        request.Blacklist.Add("synergize");
    }

    public IEnumerable<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = [];

        actions.Add(
        new ASynergize
        {
           count = Count,
        });

        return actions;
    }

    public int GetCost()
    {
        return 2 + (Count * 2);
    }

    public IEnumerable<(double, IJesterApi.IEntry)> GetUpgradeOptions(IJesterApi.IJesterRequest request, Upgrade upDir)
    {
        if (Count == 6) { return new List<(double, IJesterApi.IEntry)>(); }
        List<(double, IJesterApi.IEntry)> upgrades = [];
        upgrades.Add((1, new JesterSynergize() { Count = Count + 1}));
        return upgrades;
    }
}