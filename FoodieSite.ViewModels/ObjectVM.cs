using System;
using System.Collections.Generic;
using System.Text;

namespace FoodieSite.ViewModels
{
    public class ObjectVM
    {
        public long ImageId { get; set; }
        public string Name { get; set; }
        public string FilePathStr { get; set; }
        public string FilePathURL { get; set; }
        public long FileSize { get; set; }
    }
  
}
