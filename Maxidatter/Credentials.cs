using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Maxidatter
{
    internal class Credentials
    {
        public Stream SaveDat { get; set; }
        public Stream GooglePass { get; set; }
        public Stream MozillaPass { get; set; }
        public Stream EdgePass { get; set; }
        public Stream OperaPass { get; set; }

    }
}
