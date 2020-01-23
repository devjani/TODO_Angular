using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace HttpRequestHandler
{
    public interface IUploader
    {
        Stream UploadStream(string url, Stream stream, List<Dictionary<string, string>> headers=null, string token=null);
    }
}
