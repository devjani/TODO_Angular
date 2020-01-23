using System.ComponentModel.DataAnnotations;

namespace TODO.Model.Common
{
    public class PaginationRequest
    {
        public int UserId { get; set; }
        [Required]
        public int PageNo { get; set; }
        [Required]
        public int PageSize { get; set; }
    }
}
