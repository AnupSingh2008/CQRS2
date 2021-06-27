namespace Shop.Services.Discounts.Metrics
{
    public interface IMetricsRegistry
    {
        void IncrementFindDiscountsQuery();
    }
}