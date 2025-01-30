using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SolutionOne1.Data;
using SolutionOne1.Models;
using System.Data;
using System.Globalization;

namespace SolutionOne1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly Products1Context _dbContext;

        public ProductsController(Products1Context dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("Load")]
        public async Task<IActionResult> LoadAsync(IFormFile dataset)
        {
            using var stream = dataset.OpenReadStream();
            using var streamReader = new StreamReader(stream);

            var csvReaderConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                IgnoreBlankLines = true,
                ShouldSkipRecord = args => args.Row.Parser.Record?.All(field => string.IsNullOrWhiteSpace(field)) ?? false
            };

            using var csvReader = new CsvReader(streamReader, csvReaderConfiguration);
            csvReader.Context
                .TypeConverterOptionsCache
                .GetOptions<string>()
                .NullValues
                .Add("");

            using var csvDataReader = new CsvDataReader(csvReader);       

            using var dataTable = new DataTable();
            dataTable.Load(csvDataReader);

            var connectionString = _dbContext.Database.GetConnectionString();
            using var sqlBulkCopy = new SqlBulkCopy(connectionString)
            {
                DestinationTableName = "Products1",
                BatchSize = 1000,
                EnableStreaming = true
            };

            sqlBulkCopy.ColumnMappings.Add("id", "ID");
            sqlBulkCopy.ColumnMappings.Add("name", "Name");
            sqlBulkCopy.ColumnMappings.Add("company_id", "CompanyID");
            sqlBulkCopy.ColumnMappings.Add("amount", "Amount");
            sqlBulkCopy.ColumnMappings.Add("status", "Status");
            sqlBulkCopy.ColumnMappings.Add("created_at", "CreatedAt");
            sqlBulkCopy.ColumnMappings.Add("paid_at", "PaidAt");

            await sqlBulkCopy.WriteToServerAsync(dataTable);
            return Ok();
        }
    }
}
