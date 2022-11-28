using LatHtaukBayDin20221120GraphQL.Api.Models;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using LatHtaukBayDin20221120GraphQL.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace LatHtaukBayDin20221120GraphQL.Api.Controllers
{
    [ExtendObjectType("Query")]
    public class PagingQuery
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _db;
        public PagingQuery(IConfiguration configuration, AppDbContext db)
        {
            _configuration = configuration;
            _db = db;
        }

        public List<BlogModel> GetListDapper(int pageNo = 1, int rowCount = 10)
        {
            string query = $@"
declare @rowCount int = {rowCount}
declare @pageNo int = {pageNo}

declare @skipRowCount int 
set @skipRowCount=  ((@pageNo - 1) * @rowCount)
select * from Tbl_Blog
ORDER BY Blog_Id DESC
OFFSET @skipRowCount ROWS
FETCH NEXT @rowCount ROWS ONLY;
";
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {

                var lst = db.Query<BlogModel>(query).ToList();
                return lst;
            }
        }

        public List<BlogModel> GetListEF(int pageNo, int rowCount)
        {
            return _db.blogs.AsNoTracking().Skip((pageNo - 1) * rowCount).Take(rowCount).ToList();
        }

    }
}
