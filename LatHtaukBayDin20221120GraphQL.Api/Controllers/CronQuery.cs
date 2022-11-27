using Hangfire;
using HotChocolate.Types;
using LatHtaukBayDin20221120GraphQL.Api.Models;
using System.Linq.Expressions;

namespace LatHtaukBayDin20221120GraphQL.Api.Controllers
{
    [ExtendObjectType("Query")]
    public class CronQuery
    {
        private void SetCron(Expression<Action> method, string cronScheduleExpression)
        {
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("Myanmar Standard Time");
            RecurringJob.AddOrUpdate(method, cronScheduleExpression, zone);
        }

        public CronQueryModel RunQuery(CronQueryModel cronQueryModel)
        {
            switch (cronQueryModel.MethodName)
            {
                case "A":
                    SetCron(() => TestA(), cronQueryModel.CronScheduleExpression);
                    break;
                case "B":
                    SetCron(() => TestB(), cronQueryModel.CronScheduleExpression);
                    break;
                case "C":
                    SetCron(() => TestC(), cronQueryModel.CronScheduleExpression);
                    break;
                default:
                    break;
            }
            return cronQueryModel;
        }

        public void TestA()
        {
            Console.WriteLine("Test A " + DateTime.Now.ToString());
        }

        public void TestB()
        {
            Console.WriteLine("Test B " + DateTime.Now.ToString());
        }

        public void TestC()
        {
            Console.WriteLine("Test C " + DateTime.Now.ToString());
        }
    }
}
