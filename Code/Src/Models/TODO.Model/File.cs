namespace TODO.Model
{
    using System;
    using System.IO;
    [Serializable()]
    public class File
    {
        public string Name { get; set; }

        public long Size { get; set; }

        public string Url { get; set; }

        public Stream FileStream { get; set; }

        public string FileExtension { get; set; }

        public string ContainerName { get; set; }

        public string FieldName { get; set; }
        public string FileContent { get; set; }
    }
}
