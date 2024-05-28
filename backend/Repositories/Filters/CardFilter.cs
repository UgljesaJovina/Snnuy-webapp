using Repositories.Enums;

namespace Repositories.Filters;

public record CardFilter {
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 20;
    public CardRegions Regions { get; set; } = CardRegions.All;
    public CardTypes CardTypes { get; set; } = CardTypes.All;
    public CustomCardApprovalState ApprovalState { get; set; } = CustomCardApprovalState.Approved;
    public DateTime? PostedBefore { get; set; }
    public DateTime? PostedAfter { get; set; }
    public SortByDate ByDate { get; set; } = SortByDate.Newest;
    public SortByPopularity ByPopularity { get; set; } = SortByPopularity.None;
}
