using System;
using System.Collections.Generic;

namespace SolutionOne1.Models
{
    public partial class Products1
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid CompanyId { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime PaidAt { get; set; }
    }
}
