using AntDesign;
using System;
using System.Collections.Generic;

namespace MyHomeServer.Client.Models
{
    public class CustomUploadFileModel
    {
        public string FileName { get; set; }
        public double UploadPercent { get; set; }
        public List<(DateTime, string)> Logs { get; set; } = new List<(DateTime, string)>();
        public ProgressStatus ProgressStatus { get; set; }
        public Dictionary<string, string> ProgressStrokeColor { get; set; } = new Dictionary<string, string>();
    }
}
