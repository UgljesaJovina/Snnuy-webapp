export enum DeckType {
    None = 0,
    AggroBurn = 1,
    AgroSwarm = 2,
    Midrange = 4,
    Control = 8,
    Combo = 16,
    Agro = AgroSwarm | AggroBurn,
    All = 31
}