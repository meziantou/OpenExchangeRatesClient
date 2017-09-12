using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Meziantou.OpenExchangeRates
{
    [DebuggerDisplay("{Code} ({DisplayName})")]
    public class Currency : IEquatable<Currency>
    {
        public Currency(string code, string displayName)
        {
            Code = code;
            DisplayName = displayName;
        }

        public string Code { get; }
        public string DisplayName { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Currency);
        }

        public bool Equals(Currency other)
        {
            return other != null &&
                   Code == other.Code &&
                   DisplayName == other.DisplayName;
        }

        public override int GetHashCode()
        {
            var hashCode = -553194262;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Code);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DisplayName);
            return hashCode;
        }

        public static bool operator ==(Currency currency1, Currency currency2)
        {
            return EqualityComparer<Currency>.Default.Equals(currency1, currency2);
        }

        public static bool operator !=(Currency currency1, Currency currency2)
        {
            return !(currency1 == currency2);
        }
    }
}
