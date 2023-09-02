using Dapper;
using FileUploadInMVC.Models;
using System.Data.SqlClient;

namespace FileUploadInMVC
{
    public class MSSQLDataAccess
    {
        private readonly IConfiguration _config;
        public MSSQLDataAccess(IConfiguration configuration) { this._config = configuration; }        
        public async Task<bool> SaveFileToDatabaseAsync(byte[] fileContent, string fileName)
        {
            bool result = false;
            try {               
                using var conn = new SqlConnection(_config.GetConnectionString("SQLDB"));                
                var query = "Insert into FileUpload ([Name],[Image]) values (@name,@image)";                  
                var parameters = new { name = fileName, image = fileContent };                    
                var numOfRows = await conn.ExecuteAsync(query, parameters);
                if (numOfRows > 0){ result = true; }
                return result;
            }
            catch (Exception) { return result; }
        }
        public async Task<IEnumerable<SelectFilesModel>> GetAllFilesFromDatabaseAsync()
        {
            try
            {
                using var conn = new SqlConnection(_config.GetConnectionString("SQLDB"));
                var query = "SELECT * FROM FileUpload";
                var queryResult = await conn.QueryAsync<SelectFilesModel>(query);
                return queryResult;
            }
            catch (Exception) { return Enumerable.Empty<SelectFilesModel>(); }
        }
        public async Task<bool> DeleteFileById(int Id)
        {
            try
            {
                using var conn = new SqlConnection(_config.GetConnectionString("SQLDB"));
                var query = "DELETE From FileUpload WHERE Id = @id";
                var parameters = new { id = Id };
                var numOfRows = await conn.ExecuteAsync(query, parameters);
                if (numOfRows == 1) { return true; }
                return false;
            }
            catch (Exception) { return false; }
        }
    }
}
