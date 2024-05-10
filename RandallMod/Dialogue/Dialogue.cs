namespace RandallMod;

internal static class Dialogue
{
    private static ModInit Instance => ModInit.Instance;

    internal static void Inject()
    {
        EventDialogue.Inject();
        ArtifactDialogue.Inject();
        CombatDialogue.Inject();

        foreach (var cardType in ModInit.AllCards)
        {
            if (Activator.CreateInstance(cardType) is not IRegisterableCard card)
                continue;
            card.InjectDialogue();
        }
    }
}