﻿//This version of the package does not support Netcore 3.1 - removing until a suitable alternative view testing framework is used
//using RazorEngine.Configuration;
//using RazorEngine.Templating;
//using System.Collections.Generic;
//using System.IO;

//namespace DFC.App.JobProfileOverview.Views.UnitTests.Services
//{
//    public class RazorEngineRenderer : IViewRenderer
//    {
//        private readonly string viewRootPath;

//        public RazorEngineRenderer(string viewRootPath)
//        {
//            this.viewRootPath = viewRootPath;
//        }

//        public string Render(string templateValue, object model, IDictionary<string, object> viewBag)
//        {
//            var razorConfig = new TemplateServiceConfiguration()
//            {
//                TemplateManager = CreateTemplateManager(),
//                BaseTemplateType = typeof(HtmlSupportTemplateBase<>),
//            };

//            using var razorEngine = RazorEngineService.Create(razorConfig);

//            var dynamicViewBag = new DynamicViewBag(viewBag);
//            return razorEngine.RunCompile(templateValue, model?.GetType(), model, dynamicViewBag);
//        }

//        private ITemplateManager CreateTemplateManager()
//        {
//            var directories = Directory.GetDirectories(viewRootPath, "*.*", SearchOption.AllDirectories);
//            return new ResolvePathTemplateManager(directories);
//        }
//    }
//}