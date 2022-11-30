using LatHtaukBayDin20221120GraphQL.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LatHtaukBayDin20221120GraphQL.App.Controllers
{
    public class HashPasswordController : Controller
    {
        [ActionName("Index")]
        public IActionResult HashPasswordIndex()
        {
            return View("HashPasswordIndex");
        }

        [HttpPost]
        [ActionName("Index")]
        public IActionResult HashPasswordIndex(HashPasswordRequestModel reqModel)
        {
            string userName = Request.Form["reqModel[UserName]"].ToString();
            HashPasswordResponseModel model = new HashPasswordResponseModel();
            string secrect = "LatHtaukBayDin";
            model.GeneratePassword = (reqModel.Password + secrect + reqModel.UserName).ToHashPassword();
            return Json(model);
        }
    }

    public class HashPasswordRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class HashPasswordResponseModel
    {
        public string GeneratePassword { get; set; }
    }
}
