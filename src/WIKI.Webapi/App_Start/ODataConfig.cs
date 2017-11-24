using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using WIKI.Core.Entities;
using WIKI.WebApi.Models;

namespace WIKI.WebApi
{
    public class ODataConfig
    {
        public static void RegisterOData(HttpConfiguration config)
        {
            // OData 配置
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();

            builder.EntitySet<Question>("Question");
            var createQuestionAction = builder.EntityType<Question>().Collection.Action("Create");
            createQuestionAction.Parameter<QuestionCreateInputDto>("dto");
            createQuestionAction.ReturnsFromEntitySet<Question>("Question");

            var updateQuestionAction = builder.EntityType<Question>().Action("Update");
            updateQuestionAction.Parameter<QuestionUpdateInputDto>("dto");
            updateQuestionAction.ReturnsFromEntitySet<Question>("Question");

            builder.EntitySet<QuestionView>("QuestionView");

            builder.EntitySet<Answer>("Answer");


            builder.EntitySet<Document>("Document");
            var createDocumentAction = builder.EntityType<Document>().Collection.Action("Create");
            createDocumentAction.Parameter<DocumentCreateInputDto>("dto");
            createDocumentAction.ReturnsFromEntitySet<Document>("Document");

            var updateDocumentAction = builder.EntityType<Document>().Action("Update");
            updateDocumentAction.Parameter<DocumentUpdateInputDto>("dto");
            updateDocumentAction.ReturnsFromEntitySet<Document>("Document");

            builder.EntitySet<DocumentAttachment>("DocumentAttachment");

            builder.EntitySet<DocumentView>("DocumentView");


            // Statistic
            builder.EntitySet<ViewLogDaily>("ViewLogDaily");
            var createViewLogDailyAction = builder.EntityType<ViewLogDaily>().Collection.Action("Create");
            createViewLogDailyAction.Parameter<ViewLogDailyCreateInputDto>("dto");
            createViewLogDailyAction.ReturnsFromEntitySet<ViewLogDaily>("ViewLogDaily");

            builder.EntitySet<DownLoadLogDaily>("DownLoadLogDaily");
            var createDownLoadLogDailyAction = builder.EntityType<DownLoadLogDaily>().Collection.Action("Create");
            createDownLoadLogDailyAction.Parameter<DownLoadLogDailyCreateInputDto>("dto");
            createDownLoadLogDailyAction.ReturnsFromEntitySet<DownLoadLogDaily>("DownLoadLogDaily");


            // Search
            builder.EntitySet<Content>("Content");
            var searchQuestionAction = builder.EntityType<Content>().Collection.Action("SearchQuestion");
            searchQuestionAction.Parameter<System.Web.OData.Query.ODataQueryOptions<Content>>("queryOptions");
            searchQuestionAction.ReturnsCollectionFromEntitySet<Content>("Content");

            var searchDocumentAction = builder.EntityType<Content>().Collection.Action("SearchDocument");
            searchDocumentAction.Parameter<System.Web.OData.Query.ODataQueryOptions<Content>>("queryOptions");
            searchDocumentAction.ReturnsCollectionFromEntitySet<Content>("Content");





            config.Count().Filter().OrderBy().Expand().Select().MaxTop(null);
            config.AddODataQueryFilter();

            builder.Namespace = "WIKI";
            config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

        }
    }
}