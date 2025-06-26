namespace Definitions
{
    public enum BattleState
    {
        Start,
        PlayerTurn,
        EnemyTurn,
        Busy,
        Victory,
        Defeat,
        Escape
    }

    public enum ItemEffectType
    {
        Heal,
        BoostAttack,
        BoostDefence,
        CureStatus
    }

    public enum CombatActionType
    {
        Attack,
        UseItem,
        Escape
    }
}