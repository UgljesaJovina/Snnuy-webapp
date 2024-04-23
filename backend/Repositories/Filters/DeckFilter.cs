using Repositories.Enums;

namespace Repositories.Filters;

public class DeckFilter
{
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 20;
    public bool IncludeEternal { get; set; } = true;
    public CardRegions Regions { get; set; } = CardRegions.All;
    public DeckType Types { get; set; } = DeckType.All;
    public DateTime? PostedBefore { get; set; }
    public DateTime? PostedAfter { get; set; }
    public SortByDate ByDate { get; set; } = SortByDate.Newest;
    public SortByPopularity ByPopularity { get; set; } = SortByPopularity.None;
}