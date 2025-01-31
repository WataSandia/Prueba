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
            using var streamReader = new StreamReader(fileStream);

            using CsvReader csvReader = GetCsvReader(streamReader);
            DataTable dataTable = GetDataTable();

            var counter = 0;
            var bulkInsertTasks = new List<Task>();
            var badFormattedRows = new List<Products1>();

            while (await csvReader.ReadAsync() is true)
            {
                Products1 product = null;

                try
                {
                    product = csvReader.GetRecord<Products1>();

                    DataRow newRow = dataTable.NewRow();
                    newRow["ID"] = product.Id;
                    newRow["Name"] = product.Name is not null ? product.Name : DBNull.Value;
                    newRow["CompanyID"] = product.CompanyId;
                    newRow["Amount"] = product.Amount;
                    newRow["Status"] = product.Status;
                    newRow["CreatedAt"] = product.CreatedAt;
                    newRow["PaidAt"] = product.PaidAt is not null ? product.PaidAt : DBNull.Value;

                    dataTable.Rows.Add(newRow);
                    counter++;

                    if (counter % 1000 is 0)
                    {
                        Task bulkInsertTask = BulkInsert(dataTable);
                        bulkInsertTasks.Add(bulkInsertTask);

                        dataTable = dataTable.Clone();
                        counter = 0;
                    }
                }
                catch
                {
                    badFormattedRows.Add(product);
                    counter++;
                }
            }

            await Task.WhenAll(bulkInsertTasks);
            return Ok($"{badFormattedRows.Count} Rows could't be copied in data base for bad formattign issues.");
        }

        private CsvReader GetCsvReader(StreamReader streamReader)
        {
            var csvReaderConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                IgnoreBlankLines = true,
                ShouldSkipRecord = args =>
                {
                    if (args.Row.Parser.Record?.All(column => string.IsNullOrWhiteSpace(column)) is true)
                        return true;

                    else if (string.IsNullOrWhiteSpace(args.Row.GetField(0)) is true)
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

        private async Task BulkInsert(DataTable dataTable)
        {
            var connectionString = _dbContext.Database.GetConnectionString();
            using var sqlBulkCopy = new SqlBulkCopy(connectionString)
            {
                BatchSize = 1000,
                DestinationTableName = "Products1"
            };

            sqlBulkCopy.ColumnMappings.Add("ID", "ID");
            sqlBulkCopy.ColumnMappings.Add("Name", "Name");
            sqlBulkCopy.ColumnMappings.Add("CompanyID", "CompanyID");
            sqlBulkCopy.ColumnMappings.Add("Amount", "Amount");
            sqlBulkCopy.ColumnMappings.Add("Status", "Status");
            sqlBulkCopy.ColumnMappings.Add("CreatedAt", "CreatedAt");
            sqlBulkCopy.ColumnMappings.Add("PaidAt", "PaidAt");

            await sqlBulkCopy.WriteToServerAsync(dataTable);
        }
    }
}
