using Hyperlogy.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperlogy.Common.ResponeData
{
    public class ResponeData
    {
        public int Code { get; set; }
        public string messages { get; set; }
        public List<Student> Students { get; set; }
    }
}
