namespace SunamoLogger;
                // For unit tests
                //////////DebugLogger.Instance.WriteLine(st + ": " + format);
            }
            else
            {
                StatusSetted(st, format);
            }
        }
    }
    internal static void Ordinal(string v, params string[] o)
    {
        SetStatus(TypeOfMessage.Ordinal, v, o);
    }
}