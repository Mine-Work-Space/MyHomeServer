using AntDesign;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;

namespace MyHomeServer.Client.Models
{
    public class CustomUploadFileModel
    {
        public IBrowserFile File { get; set; }
        public float UploadedPercent { get; set; } = 0.0f;
        public float OldPercent { get; set; } = 0.0f;
        public int Fragment { get; set; } = 0;
        public long UploadedBytes { get; set; } = 0;
        public long TotalBytes { get; set; } = 0;
        public long ChunkSize { get; set; } = 0;
        public RenderFragment Pending { get; set; }
        public List<(DateTime, string)> Logs { get; set; } = new List<(DateTime, string)>();
        public ProgressStatus ProgressStatus { get; set; } = ProgressStatus.Active;
        public Dictionary<string, string> ProgressStrokeColor { get; set; } = new Dictionary<string, string>();
        public bool IsUploading { get; set; } = false;
    }
}
