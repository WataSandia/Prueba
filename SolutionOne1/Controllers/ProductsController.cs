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
            using var fileStream = dataset.OpenReadStream();
            var streamReader = new StreamReader(fileStream);

            var csvReader = GetCsvReader(streamReader);
            var recordsCollection = csvReader.GetRecordsAsync<Products1>();

            var dataTable = GetDataTable();

            await foreach (var record in recordsCollection)
            {
                var r = record;
            }

            throw new NotImplementedException();
        }

        private CsvReader GetCsvReader(StreamReader streamReader)
        {
            var csvReaderConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                IgnoreBlankLines = true,
                ShouldSkipRecord = args =>
                {
                    if (string.IsNullOrWhiteSpace(args.Row.GetField(0)) is true)
                        return true;

                    else if (string.IsNullOrWhiteSpace(args.Row.GetField(2)) is true)
                        return true;

                    else if (string.IsNullOrWhiteSpace(args.Row.GetField(3)) is true)
                        return true;

                    else if (string.IsNullOrWhiteSpace(args.Row.GetField(4)) is true)
                        return true;

                    else if (string.IsNullOrWhiteSpace(args.Row.GetField(5)) is true)
                        return true;

                    return false;
                }
            };

            var csvReader = new CsvReader(streamReader, csvReaderConfiguration);
            return csvReader;
        }

        private DataTable GetDataTable()
        {
            var connectionString = _dbContext.Database.GetConnectionString();
            var dummyQuery = "SELECT * FROM Products1 WHERE 1 = 0;";

            var sqlDataAdapter = new SqlDataAdapter(dummyQuery, connectionString);

            var dataTable = new DataTable();
            sqlDataAdapter.FillSchema(dataTable, SchemaType.Source);

            return dataTable;
        }
    }
}
