using System.Collections.Generic;

namespace TODO.Model.Common
{
    public class PaginationResponse<T> where T : class
    {
        public PaginationResponse()
        {
            Data = new List<T>();
        }
        public List<T> Data { get; set; }
        public int TotalRecords { get; set; }
    }
}
