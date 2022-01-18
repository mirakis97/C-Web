﻿using BasicWebServer.Server.HTTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Server.Responses
{
    public class ViewResponse : ContentResponse
    {
        private const char PathSeparator = '/';

        public ViewResponse(string viewHame, string controllerName, object model = null)
            : base("", ContentType.Html)
        {
            if (!viewHame.Contains(PathSeparator))
            {
                viewHame = controllerName + PathSeparator + viewHame;
            }

            var viewPath = Path.GetFullPath(
                $"./Views/" +
                viewHame.TrimStart(PathSeparator)
                + ".cshtml");

            var viewContent = File.ReadAllText(viewPath);

            if (model != null)
            {
                viewContent = this.PopulateModel(viewContent, model);
            }

            this.Body = viewContent;
        }

        private string PopulateModel(string viewContent,object model)
        {
            var data = model
                .GetType()
                .GetProperties()
                .Select(pr => new
                {
                    pr.Name,
                    Value = pr.GetValue(model)
                });

            foreach (var entry in data)
            {
                const string openingBrackets = "{{";
                const string closingBrackets = "}}";

                viewContent = viewContent.Replace(
                    $"{openingBrackets}{entry.Name}{closingBrackets}",
                    entry.Value.ToString());
            }

            return viewContent;
        }
    }
}