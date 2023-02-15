using AntDesign;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;

namespace MyHomeServer.Client.Models
{
    public class CustomUploadFileModel
    {
        public IBrowserFile File { get; set; }
        public double UploadPercent { get; set; } = 0;
        public List<(DateTime, string)> Logs { get; set; } = new List<(DateTime, string)>();
        public ProgressStatus ProgressStatus { get; set; } = ProgressStatus.Active;
        public Dictionary<string, string> ProgressStrokeColor { get; set; } = new Dictionary<string, string>();
        public bool IsUploading { get; set; } = false;
    }
}
