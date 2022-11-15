using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperlogy.Common.Entities
{
    public enum Gender
    {
        /// <summary>
        /// Gioi tinh Nam
        /// </summary>
        Male = 0,

        /// <summary>
        /// Gioi tinh Nu
        /// </summary>
        FeMale = 1,

        /// <summary>
        /// Gioi tinh khac
        /// </summary>
        Other = 2,
    }

    public enum ActivityMode
    {
        GetMode = 0,
        PostMode = 1,
        PutMode = 2,
        DeleteMode = 3,
    }
}
