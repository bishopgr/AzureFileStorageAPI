using AzureFileStorage.API.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AzureFileStorage.Tests.Helpers
{
    public class ResponseTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new[] { new ErrorResponse("Borked") };
            yield return new[] { new AzureFileResponse($"https://test.box.dev-notazure.com/{Guid.NewGuid()}-fakecontainer/testFile.tiff") };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
