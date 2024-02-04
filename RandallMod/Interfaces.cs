//using CobaltCoreModding.Definitions.ModContactPoints;
using HarmonyLib;

namespace RandallMod;

internal interface IRegisterableArtifact
{
    void ApplyPatches(Harmony harmony) { }
    void InjectDialogue() { }
}

internal interface IRegisterableCard
{
    void ApplyPatches(Harmony harmony) { }
    void InjectDialogue() { }
}
/*
internal interface Card
{
    void InjectDialogue() { }
}*/