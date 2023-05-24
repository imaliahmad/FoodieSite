using System;
using System.Collections.Generic;
using System.Text;

namespace FoodieSite.ViewModels
{
    public class ImageMasterVM
    {
        public long ImageMasterId { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileSize { get; set; }
        public string FilePath { get; set; }

        public virtual ItemMasterVM ItemMaster { get; set; }
    }
}
