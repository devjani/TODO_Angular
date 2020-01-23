namespace TODO.Model.Common
{
    public class FilterModel : PaginationRequest
    {
        public string OrderBy { get; set; }
        public string SearchExpression { get; set; }        
    }
}
