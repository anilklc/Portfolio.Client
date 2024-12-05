using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioClient.DTO.AuthorizeMenu
{
    public class ApplicationService
    {
        public string Name { get; set; }
        public List<ActionItem> Actions { get; set; }
    }
}
