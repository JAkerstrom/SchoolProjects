using Nackowski.Models.API_ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowski.Caching
{
    public class CacheKeys
    {
        public static string Auktions { get { return "_Auktions"; } }
        public static string OpenAuktions { get { return "_OpenAuktions"; } }
        public static string Entry { get { return "_Entry"; } }
    }
}
