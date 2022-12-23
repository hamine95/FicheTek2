using System;

namespace BackEnd2.CustomClass
{
    public class YesNoQuestion
    {
        public Action<bool> YesNoCallback { get; set; }
        public string Question { get; set; }

        public Action<bool> UploadCallback { get; set; }
    }
}