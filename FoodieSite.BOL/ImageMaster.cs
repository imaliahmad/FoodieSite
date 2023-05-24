using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FoodieSite.BOL
{
    public class ImageMaster
    {
        [Key]
        public long ImagMaesterId { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileSize { get; set; }
        public string FilePath { get; set; }

        public virtual ItemMaster ItemMaster { get; set; }
        public virtual AppUsers AppUsers { get; set; }
    }
}
