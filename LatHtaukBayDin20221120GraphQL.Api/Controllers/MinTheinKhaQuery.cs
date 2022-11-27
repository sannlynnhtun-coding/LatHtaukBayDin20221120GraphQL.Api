using LatHtaukBayDin20221120GraphQL.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace LatHtaukBayDin20221120GraphQL.Api.Controllers
{
    [ExtendObjectType("Query")]
    public class MinTheinKhaQuery
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MinTheinKhaQuery(IHttpClientFactory httpClientFactory) =>
                _httpClientFactory = httpClientFactory;

        public async Task<OurApi_BayDinQuestionResponseModel> GetQuestionListAsync()
        {
            OurApi_BayDinQuestionResponseModel model = new OurApi_BayDinQuestionResponseModel();
            string url = "https://www.ydnp.net/baydin/_next/data/HmAmLWm8d-Je6rehKaa27/index.json";
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.GetAsync(url);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                model.Response = new OurApi_ResponseModel
                {
                    RespType = EnumRespType.Information,
                    RespDesp = "No Data."
                };
                goto result;
            }
            var jsonStr = await httpResponseMessage.Content.ReadAsStringAsync();
            OtherApi_BayDinQuestionResponseModel resModel = JsonSerializer.Deserialize<OtherApi_BayDinQuestionResponseModel>(jsonStr);

            if (resModel.pageProps.questions.Count > 0)
            {
                model.lstQuestion = resModel.pageProps.questions.Select(x => new OurApi_QuestionModel
                {
                    QuestionId = x.qId,
                    QuestionName = x.quest
                }).ToList();
            }
            model.Response = new OurApi_ResponseModel
            {
                RespType = EnumRespType.Success,
                RespDesp = "Success."
            };
        result:
            return model;
        }

        public async Task<OurApi_BayDinAnswerResponseModel> GetAnswerByQuestionIdAsync(int questionId)
        {
            OurApi_BayDinAnswerResponseModel model = new OurApi_BayDinAnswerResponseModel();
            string url = $"https://bd.ydnp.net/api/v1/reqans.ashx?count=true&qId={questionId}";
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.GetAsync(url);

            //string str = JsonSerializer.Serialize(model);
            //HttpContent content = new StringContent(str, Encoding.UTF8, "application/json");
            //httpClient.PostAsync("", content);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                model.Response = new OurApi_ResponseModel
                {
                    RespType = EnumRespType.Information,
                    RespDesp = "No Data."
                };
                goto result;
            }
            var jsonStr = await httpResponseMessage.Content.ReadAsStringAsync();
            List<OtherApi_BayDinAnswerResponseModel> resModel = JsonSerializer.Deserialize<List<OtherApi_BayDinAnswerResponseModel>>(jsonStr);
            if (resModel.Count > 0)
            {
                var item = resModel[0];
                model.QuestionId = questionId;
                model.QuestionName = item.quest;
                model.Answer = item.ans;
            }
            model.Response = new OurApi_ResponseModel
            {
                RespType = EnumRespType.Success,
                RespDesp = "Success."
            };
        result:
            return model;
        }
    }
}
