using Hangfire;
using Hangfire.Storage;
using HotChocolate.Types;
using LatHtaukBayDin20221120GraphQL.Api.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Linq.Expressions;
using System.Reflection;

namespace LatHtaukBayDin20221120GraphQL.Api.Controllers
{
    [ExtendObjectType("Query")]
    public class waiwaikhaingQuery
    {
        //Recurring Jobs
        private void SetCronRecurring(Expression<Action> method, string cronScheduleExpression)
        {
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("Myanmar Standard Time");
            var tt = method.Body.GetType().Name;
            // var tt1 = method.Body[MemberTypes].Name;
            //   var tt2 = method.Body().Name;
            RecurringJob.AddOrUpdate(method, cronScheduleExpression, zone);
        }

        public CronQueryModel RunQueryRecurring(CronQueryModel cronQueryModel)
        {
            switch (cronQueryModel.MethodName)
            {
                case "A":
                    SetCronRecurring(() => TestA(), cronQueryModel.CronScheduleExpression);
                    break;
                case "B":
                    SetCronRecurring(() => TestB(), cronQueryModel.CronScheduleExpression);
                    break;
                case "C":
                    SetCronRecurring(() => TestC(), cronQueryModel.CronScheduleExpression);
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

        //Fire-and-Forget Jobs
        private void SetCronForFireANDForget(Expression<Action> method)
        {
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("Myanmar Standard Time");
            var jobId = BackgroundJob.Enqueue(method);
        }

        public CronQueryModel RunQueryForFireANDForget(CronQueryModel cronQueryModel)
        {
            switch (cronQueryModel.MethodName)
            {
                case "A":
                    SetCronForFireANDForget(() => TestA());
                    break;
                case "B":
                    SetCronForFireANDForget(() => TestB());
                    break;
                case "C":
                    SetCronForFireANDForget(() => TestC());
                    break;
                default:
                    break;
            }
            return cronQueryModel;
        }

        //DelayedJobs
        private void SetCronForDelayedJobs(Expression<Action> method, string cronScheduleExpression)
        {
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("Myanmar Standard Time");
            TimeSpan CurrentTime = DateTime.Now.TimeOfDay;

            var ts = TimeSpan.Parse("00:59");
            BackgroundJob.Schedule(method, ts);
        }

        public CronQueryModel RunQueryForDelayedJobs(CronQueryModel cronQueryModel)
        {
            switch (cronQueryModel.MethodName)
            {
                case "A":
                    SetCronForDelayedJobs(() => TestA(), cronQueryModel.CronScheduleExpression);
                    break;
                case "B":
                    SetCronForDelayedJobs(() => TestB(), cronQueryModel.CronScheduleExpression);
                    break;
                case "C":
                    SetCronForDelayedJobs(() => TestC(), cronQueryModel.CronScheduleExpression);
                    break;
                default:
                    break;
            }
            return cronQueryModel;
        }
        public CronQueryModel DeleteRecurringJobs(CronQueryModel str)
        {
            DeleteJobsByID(str.MethodName);
            return str;
        }
        public void DeleteJobsByID(string jobId)
        {
            // BackgroundJob.Delete(jobId);
            RecurringJob.RemoveIfExists(jobId);
        }

        public CronQueryModel DeleteAllRecurringJobs(CronQueryModel cronQueryModel)
        {
            switch (cronQueryModel.MethodName)
            {
                case "all":
                    DeleteJobsAll();
                    break;
                default:
                    DeleteJobsByID(cronQueryModel.MethodName);
                    break;
            }
            return cronQueryModel;
            //  DeleteJobsAll();
            //return "Delete All jobs";
        }
        public void DeleteJobsAll()
        {
            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in connection.GetRecurringJobs())
                {
                    RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }
        }

    }
}
