namespace LatHtaukBayDin20221120GraphQL.Api.Models
{
    public class OtherApi_PagePropsModel
    {
        public List<OtherApi_QuestionModel> questions { get; set; }
    }

    public class OtherApi_QuestionModel
    {
        public int qId { get; set; }
        public string quest { get; set; }
        public int catId { get; set; }
        public int count { get; set; }
    }

    public class OtherApi_BayDinQuestionResponseModel
    {
        public OtherApi_PagePropsModel pageProps { get; set; }
        public bool __N_SSG { get; set; }
    }

    public class OurApi_BayDinQuestionResponseModel : OurApi_BaseResponseModel
    {
        public List<OurApi_QuestionModel> lstQuestion { get; set; }
    }

    public class OurApi_BayDinAnswerResponseModel : OurApi_BaseResponseModel
    {
        public int QuestionId { get; set; }
        public string QuestionName { get; set; }
        public string Answer { get; set; }
    }

    public class OurApi_QuestionModel
    {
        public int QuestionId { get; set; }
        public string QuestionName { get; set; }
    }

    public class OurApi_ResponseModel
    {
        public EnumRespType RespType { get; set; }
        public string RespDesp { get; set; }
    }

    public class OurApi_BaseResponseModel
    {
        public OurApi_ResponseModel Response { get; set; }
    }

    public enum EnumRespType
    {
        Success,
        Information,
        Warning,
        Error
    }

    public class OtherApi_BayDinAnswerResponseModel
    {
        public int qId { get; set; }
        public int ansID { get; set; }
        public string quest { get; set; }
        public string ans { get; set; }
    }
}
