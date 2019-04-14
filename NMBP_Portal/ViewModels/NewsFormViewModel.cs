using System.ComponentModel.DataAnnotations;

namespace NMBP_Portal.ViewModels
{
    public class NewsFormViewModel
    {
        public string Headline { get; set; }

        public string Text { get; set; }

        public string Author { get; set; }

        [Required]
        public string Picture { get; set; }
    }
}