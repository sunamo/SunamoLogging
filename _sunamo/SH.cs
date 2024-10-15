using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoLogging._sunamo;
internal class SH
{
    internal static string NullToStringOrDefault(object n)
    {

        return n == null ? " " + Consts.nulled : AllStrings.space + n;
    }
}
