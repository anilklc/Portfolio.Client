namespace PortfolioClient.DTO.CaseStudies.Admin
{
    public class CreateCaseStudiesDTO
    {
        public string Category { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public int BoxType { get; set; }
    }
}
