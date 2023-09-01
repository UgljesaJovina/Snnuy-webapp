namespace Repositories.Enums;

[Flags]
public enum CardRegions
{
    Bandle_City = 0b_0000_0000_0001,
    Bilgewater = 0b_0000_0000_0010,
    Demacia = 0b_0000_0000_0100,
    Freljord = 0b_0000_0000_1000,
    Ionia = 0b_0000_0000_1000,
    Noxus = 0b_0000_0001_0000,
    PNZ = 0b_0000_0010_0000,
    Shadow_Isles = 0b_0000_0100_0000,
    Shurima = 0b_0000_1000_0000,
    Targon = 0b_0001_0000_0000,
    Runterra = 0b_0010_0000_0000,
    Custom = 0b_0100_0000_0000,
}