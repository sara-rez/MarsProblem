using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace MarsProblem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly ConnectionStrings _connectionStrings;

        public BookController(ILogger<BookController> logger, IOptions<ConnectionStrings> options)
        {
            _logger = logger;
            _connectionStrings = options.Value;
        }

        [Route("MarsOn")]
        [HttpGet]
        public IEnumerable<Book> GetMarsOn()
        {
            return GetBooksMarsOn();
        }

        [Route("MarsOnAsync")]
        [HttpGet]
        public async Task<IEnumerable<Book>> GetMarsOnAsync()
        {
            return await GetBooksMarsOnAsync();
        }

        [Route("MarsOff")]
        [HttpGet]
        public IEnumerable<Book> GetMarsOff()
        {
            return GetBooksMarsOff();
        }

        [Route("MarsOffAsync")]
        [HttpGet]
        public async Task<IEnumerable<Book>> GetMarsOffAsync()
        {
            return await GetBooksMarsOffAsync();
        }

        #region MARS Enabled

        // Get books asynchronous with MARS enabled
        private async Task<IEnumerable<Book>> GetBooksMarsOnAsync()
        {
            var connectionString = _connectionStrings.ConnectionStringMarsEnabled;
            string queryString = "SELECT Id,Title from dbo.Book";

            using SqlConnection connection = new(connectionString);
            SqlCommand command = new(queryString, connection);
            List<Book> books = new();

            try
            {
                await connection.OpenAsync();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    books.Add(new()
                    {
                        Id = Convert.ToInt64(reader["Id"]),
                        Title = Convert.ToString(reader["Title"])
                    });
                }

                await reader.CloseAsync();
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception \nMessage: {}, \nStackTrace: \n{}", ex.Message, ex.StackTrace);
            }

            return books;
        }

        // Get books synchronous with MARS enabled
        private IEnumerable<Book> GetBooksMarsOn()
        {
            var connectionString = _connectionStrings.ConnectionStringMarsEnabled;
            string queryString = "SELECT Id,Title from dbo.Book";

            using SqlConnection connection = new(connectionString);
            SqlCommand command = new(queryString, connection);
            List<Book> books = new();

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    books.Add(new()
                    {
                        Id = Convert.ToInt64(reader["Id"]),
                        Title = Convert.ToString(reader["Title"])
                    });
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception \nMessage: {}, \nStackTrace: \n{}", ex.Message, ex.StackTrace);
            }

            return books;
        }

        #endregion

        #region MARS Disabled

        // Get books asynchronous with MARS disabled
        private async Task<IEnumerable<Book>> GetBooksMarsOffAsync()
        {
            var connectionString = _connectionStrings.ConnectionStringMarsDisabled;
            string queryString = "SELECT Id,Title from dbo.Book";

            using SqlConnection connection = new(connectionString);
            SqlCommand command = new(queryString, connection);
            List<Book> books = new();

            try
            {
                await connection.OpenAsync();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    books.Add(new()
                    {
                        Id = Convert.ToInt64(reader["Id"]),
                        Title = Convert.ToString(reader["Title"])
                    });
                }

                await reader.CloseAsync();
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception \nMessage: {}, \nStackTrace: \n{}", ex.Message, ex.StackTrace);
            }

            return books;
        }

        // Get books synchronous with MARS disabled
        private IEnumerable<Book> GetBooksMarsOff()
        {
            var connectionString = _connectionStrings.ConnectionStringMarsDisabled;
            string queryString = "SELECT Id,Title from dbo.Book";

            using SqlConnection connection = new(connectionString);
            SqlCommand command = new(queryString, connection);
            List<Book> books = new();

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    books.Add(new()
                    {
                        Id = Convert.ToInt64(reader["Id"]),
                        Title = Convert.ToString(reader["Title"])
                    });
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception \nMessage: {}, \nStackTrace: \n{}", ex.Message, ex.StackTrace);
            }

            return books;
        }

        #endregion      

    }
}