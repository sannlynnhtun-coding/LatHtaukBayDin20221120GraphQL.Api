using LatHtaukBayDin20221120GraphQL.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace LatHtaukBayDin20221120GraphQL.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public BlogController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{pageNo}/{rowCount}")]
        public List<BlogModel> GetList(int pageNo = 1, int rowCount = 10)
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
                return db.Query<BlogModel>(query).ToList();
            }

            //db.TblBlog.AsNoTracking().Skip(pageNo - 1 * rowCount).Take(rowCount).ToList();
        }

        [HttpGet("{id}")]
        public BlogModel Get(int id)
        {
            string query = $"select * from tbl_blog where blog_id={id}";
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                return db.Query<BlogModel>(query).FirstOrDefault();
            }
        }
    }
}
