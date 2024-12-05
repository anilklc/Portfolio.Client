using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioClient.DTO.CaseStudies.Admin
{
    public class AdminCaseStudiesDTO
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public int BoxType { get; set; }
    }
}
