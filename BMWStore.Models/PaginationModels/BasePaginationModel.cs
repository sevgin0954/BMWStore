namespace BMWStore.Models.PaginationModels
{
    public abstract class BasePaginationModel
    {
        public int TotalPagesCount { get; set; }
        public int CurrentPage { get; set; }
    }
}
