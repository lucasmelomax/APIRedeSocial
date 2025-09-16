using System.ComponentModel.DataAnnotations;

namespace RedeSocial.API.Models {
    public class PaginationParams {

        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; }

        [Range(1,50, ErrorMessage = "O maximo de items por pagina é 50.")]
        public int PageSize { get; set; }
    }
}
