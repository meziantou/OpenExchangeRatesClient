using System.Diagnostics;

namespace Meziantou.OpenExchangeRates
{
    [DebuggerDisplay("{Code} ({DisplayName})")]
    public class Currency
    {
        public Currency(string code, string displayName)
        {
            Code = code;
            DisplayName = displayName;
        }

        public string Code { get; }
        public string DisplayName { get; }
    }
}
