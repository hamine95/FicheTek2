using System;

namespace BackEnd2.CustomClass
{
    public class LoadedImage
    {
        public Action<bool> IsConfirm { get; set; }

        public Action<string,string,bool> UploadCallback { get; set; }
    }
}