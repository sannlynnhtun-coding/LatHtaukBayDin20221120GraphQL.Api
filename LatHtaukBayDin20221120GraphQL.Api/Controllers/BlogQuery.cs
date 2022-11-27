using Dapper;
using LatHtaukBayDin20221120GraphQL.Api.Models;
using System.Data;
using System.Data.SqlClient;

namespace LatHtaukBayDin20221120GraphQL.Api.Controllers
{
    [ExtendObjectType("Query")]
    public class BlogQuery
    {
        private readonly IConfiguration _configuration;

        public BlogQuery(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<BlogModel> GetList(int pageNo = 1, int rowCount = 10)
        {
            string query = $@"
declare @rowCount int = {pageNo}
declare @pageNo int = {rowCount}

declare @skipRowCount int 
set @skipRowCount=  ((@pageNo - 1) * @rowCount)
select * from Tbl_Blog
ORDER BY Blog_Id DESC
OFFSET @skipRowCount ROWS
FETCH NEXT @rowCount ROWS ONLY;
";
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                return db.Query<BlogModel>(query).ToList();
            }
        }
    }
}
