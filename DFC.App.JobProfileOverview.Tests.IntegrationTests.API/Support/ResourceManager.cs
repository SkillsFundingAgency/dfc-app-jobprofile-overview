﻿using Newtonsoft.Json;
using System;
using System.IO;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support
{
    internal class ResourceManager
    {
        internal static T GetResource<T>(string resourceName)
        {
            DirectoryInfo resourcesDirectory = Directory.CreateDirectory(Environment.CurrentDirectory).GetDirectories("Resource")[0];
            FileInfo[] files = resourcesDirectory.GetFiles();
            FileInfo selectedResource = null;

            for (int fileIndex = 0; fileIndex < files.Length; fileIndex++)
            {
                if (files[fileIndex].Name.ToLower().StartsWith(resourceName.ToLower()))
                {
                    selectedResource = files[fileIndex];
                    break;
                }
            }

            if (selectedResource == null)
            {
                throw new Exception($"No resource with the name {resourceName} was found");
            }

            using (StreamReader streamReader = new StreamReader(selectedResource.FullName))
            {
                string content = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(content);
            }
        }
    }
}
