using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIKI.Document.Consumer.Model;
using System.Data.SqlClient;
using Dapper;
using WIKI.Document.Consumer.Entities;

namespace WIKI.Document.Consumer.Database
{
    public class DataAccess
    {
        private static SqlConnection GetConnection()
        {
            string connectionStr = System.Configuration.ConfigurationManager.ConnectionStrings["WIKI"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionStr);

            return connection;
        }

        public static DocumentDto GetDocumentModel(long documentId)
        {
            DocumentDto model = null;

            var sql = @"
SELECT q.*
	  ,c.FullName as EX_CreatedBy
	  ,u.FullName as EX_UpdatedBy
	  ,c.Department as EX_CreatedByDept
	  ,u.Department as EX_UpdatedByDept
FROM Documents q 
left join Accounts c on c.UserName = q.CreatedBy
left join Accounts u on u.UserName = q.UpdatedBy
where q.Id = @Id 
select Value from Tags where ContentId = @Id
SELECT q.*
	  ,c.FullName as EX_CreatedBy
	  ,u.FullName as EX_UpdatedBy
	  ,c.Department as EX_CreatedByDept
	  ,u.Department as EX_UpdatedByDept
FROM DocumentAttachments q 
left join Accounts c on c.UserName = q.CreatedBy
left join Accounts u on u.UserName = q.UpdatedBy
where q.DocumentId = @Id
";

            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    using (var multi = connection.QueryMultiple(sql, new { Id = documentId }))
                    {
                        var question = multi.Read<Entities.Document>().SingleOrDefault();
                        if(question!=null)
                        {
                            model = question.MapToDto();

                            model.Tags = multi.Read<string>().ToList();
                            model.DocumentAttachment = multi.Read<DocumentAttachment>().Select(m => m.MapToDto()).ToList();
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

        public static void SetDocumentMessageIndexed(string Id)
        {
            var sql = @"
update Documents_ChangeLog set IsIndex = 1, UpdatedTime = GETDATE() where Id = @Id
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
