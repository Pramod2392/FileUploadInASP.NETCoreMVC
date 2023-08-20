﻿
using Dapper;
using System.Data.SqlClient;
namespace FileUploadInMVC {
    public class MSSQLDataAccess {
        private readonly IConfiguration _config;
        public MSSQLDataAccess(IConfiguration configuration) {
            this._config = configuration;
        }        
        public async Task<bool> SaveFileToDatabaseAsync(byte[] fileContent, string fileName) {
            bool result = false;
            try {               
                using var conn = new SqlConnection(_config.GetConnectionString("SQLDB"));                
                var query = "Insert into FileUpload ([Name],[Image]) values (@name,@image)";                  
                var parameters = new { name = fileName, image = fileContent };                    
                var numOfRows = await conn.ExecuteAsync(query, parameters);
                if (numOfRows > 0){
                    result = true;
                }
                return result;
            }
            catch (Exception) {
                return result;
            }
        }
    }
}
