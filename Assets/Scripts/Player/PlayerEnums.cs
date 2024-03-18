[System.Flags]
public enum PlayerCombatState
{
    Preparation = 1 << 0,

    Combat = 1 << 2,
}


[System.Flags]
public enum PlayerTargetState
{
    NotChosen = 1 << 0,
    Chosen = 1 << 1,
}

