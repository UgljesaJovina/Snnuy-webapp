namespace Repositories.Enums;

[Flags]
public enum DeckType
{
    None = 0b_0000_0000,
    Aggro_Burn = 0b_0000_0001,
    Agro_Swarm = 0b_0000_0010,
    Midrange = 0b_0000_0100,
    Control = 0b_0000_1000,
    Agro = Agro_Swarm | Aggro_Burn
}