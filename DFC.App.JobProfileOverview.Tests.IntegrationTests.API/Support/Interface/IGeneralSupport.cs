﻿using System;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.Interface
{
    internal interface IGeneralSupport
    {
        string RandomString(int length);

        void InitialiseAppSettings();

        byte[] ConvertObjectToByteArray(object obj);

        string GetDescription(Enum enumerator);
    }
}