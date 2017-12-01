using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIKI.Question.Consumer.Model;

namespace WIKI.Question.Consumer.ELK
{
    public class IndexAccess
    {
        private static string INDEX_NAME = "wiki-content-question";
        private static string TYPE_NAME = "doc";


        public static IResponse UpdateQuestion(QuestionDto model)
        {
            if (model == null)
                return null;

            var client = GetClient();
            return client.Index(model, idx => idx.Index(INDEX_NAME).Type(TYPE_NAME));

        }

        public static IResponse DeleteQuestion(long QuestionId)
        {
            var client = GetClient();
            return client.Delete(new DeleteRequest(INDEX_NAME, TYPE_NAME, QuestionId));
            
        }


        public static IResponse UpdateAnswer(AnswerDto model)
        {
            if (model == null)
                return null;

            var client = GetClient();
            return client.Index(model, idx => idx.Index(INDEX_NAME).Type(TYPE_NAME).Routing(model.join_field.parent.ToString()));

        }

        public static IResponse DeleteAnswer(string AnswerId)
        {
            var client = GetClient();

            var response = client.DeleteByQuery<AnswerDto>(m => m
                .Index(INDEX_NAME).Type(TYPE_NAME)
                .Query(q => q
                    .Match(qm => qm.Field(f => f.AnswerId).Query(AnswerId)))
                );

            return response;

        }


        private static ElasticClient GetClient()
        {
            var server = System.Configuration.ConfigurationManager.AppSettings["Elastic_Server"];
            var user = System.Configuration.ConfigurationManager.AppSettings["Elastic_User"];
            var password = System.Configuration.ConfigurationManager.AppSettings["Elastic_Password"];

            var node = new Uri(server);
            var settings = new ConnectionSettings(node);
            settings.BasicAuthentication(user, password);
            
            var client = new ElasticClient(settings);

            return client;
        }
    }
}
