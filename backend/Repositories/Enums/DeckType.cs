namespace Repositories.Enums;

[Flags]
public enum DeckType
{
    None = 0b_0000_0000,
    AggroBurn = 0b_0000_0001,
    AgroSwarm = 0b_0000_0010,
    Midrange = 0b_0000_0100,
    Control = 0b_0000_1000,
    Combo = 0b_0001_0000,
    Agro = AgroSwarm | AggroBurn,
    All = 31
}