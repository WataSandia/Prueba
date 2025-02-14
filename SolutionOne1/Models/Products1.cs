﻿using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SolutionOne1.Models
{
    public partial class Products1
    {
        [Name("id")]
        public string Id { get; set; } = null!;

        [Name("name")]
        public string? Name { get; set; }

        [Name("company_id")]
        public string CompanyId { get; set; } = null!;

        [Name("amount")]
        public double Amount { get; set; }

        [Name("status")]
        public string Status { get; set; } = null!;

        [Name("created_at")]
        public DateTime CreatedAt { get; set; }

        [Name("paid_at")]
        public DateTime? PaidAt { get; set; }
    }
}
