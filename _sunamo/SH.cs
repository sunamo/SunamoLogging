namespace SunamoLogging._sunamo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class SH
{
    internal static string NullToStringOrDefault(object n)
    {

        return n == null ? " " + "(null)" : "" + n;
    }
}