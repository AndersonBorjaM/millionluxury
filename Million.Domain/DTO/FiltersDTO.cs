namespace Million.Domain.DTO
{
    public class FiltersDTO
    {
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public string PropertyOrderBy { get; set; }
        public bool AscendingOrderBy { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
