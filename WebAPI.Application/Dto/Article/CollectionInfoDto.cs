namespace WebAPI.Application.Dto
{
    public class CollectionInfoDto<T>
    {
        public int Count { get; set; }
        public int Pages { get; set; }
        public int? TotalRows { get; set; }
        public List<T> Collection { get; set; }
    }
}