using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIKI.Question.Consumer.Model;
using System.Data.SqlClient;
using Dapper;
using WIKI.Question.Consumer.Entities;

namespace WIKI.Question.Consumer.Database
{
    public class DataAccess
    {
        private static SqlConnection GetConnection()
        {
            string connectionStr = System.Configuration.ConfigurationManager.ConnectionStrings["WIKI"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionStr);

            return connection;
        }

        public static QuestionDto GetQuestionModel(long questionId)
        {
            QuestionDto model = null;

            var sql = @"
SELECT q.*
	  ,c.FullName as EX_CreatedBy
	  ,u.FullName as EX_UpdatedBy
	  ,c.Department as EX_CreatedByDept
	  ,u.Department as EX_UpdatedByDept
FROM [Questions] q 
left join Accounts c on c.UserName = q.CreatedBy
left join Accounts u on u.UserName = q.UpdatedBy
where q.Id = @Id 
select Value from Tags where ContentId = @Id

";

            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    using (var multi = connection.QueryMultiple(sql, new { Id = questionId }))
                    {
                        var question = multi.Read<Entities.Question>().SingleOrDefault();
                        if(question!=null)
                        {
                            model = question.MapToDto();

                            model.Tags = multi.Read<string>().ToList();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

            return model;
        }

        public static void SetQuestionMessageIndexed(string Id)
        {
            var sql = @"
update Questions_ChangeLog set IsIndex = 1, UpdatedTime = GETDATE() where Id = @Id
";

            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    connection.Execute(sql, new { Id = Id });

                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        public static AnswerDto GetAnswerModel(long AnswerId)
        {
            AnswerDto model = null;

            var sql = @"
SELECT q.*
	  ,c.FullName as EX_CreatedBy
	  ,u.FullName as EX_UpdatedBy
	  ,c.Department as EX_CreatedByDept
	  ,u.Department as EX_UpdatedByDept
FROM Answers q 
left join Accounts c on c.UserName = q.CreatedBy
left join Accounts u on u.UserName = q.UpdatedBy
where q.Id = @Id 
";

            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    using (var multi = connection.QueryMultiple(sql, new { Id = AnswerId }))
                    {
                        var Answer = multi.Read<Entities.Answer>().SingleOrDefault();
                        if (Answer != null)
                        {
                            model = Answer.MapToDto();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

            return model;
        }


        public static void SetAnswerMessageIndexed(string Id)
        {
            var sql = @"
update Answers_ChangeLog set IsIndex = 1, UpdatedTime = GETDATE() where Id = @Id
";

            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    connection.Execute(sql, new { Id = Id });

                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

    }
}
