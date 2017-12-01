using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIKI.Document.Consumer.Model;

namespace WIKI.Document.Consumer.ELK
{
    public class IndexAccess
    {
        private static string INDEX_NAME = "wiki-content-document";
        private static string TYPE_NAME = "doc";


        public static IResponse UpdateDocument(DocumentDto model)
        {
            if (model == null)
                return null;

            var client = GetClient();
            return client.Index(model, idx => idx.Index(INDEX_NAME).Type(TYPE_NAME));

        }

        public static IResponse DeleteDocument(long documentId)
        {
            var client = GetClient();
            return client.Delete(new DeleteRequest(INDEX_NAME, TYPE_NAME, documentId));
            
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
