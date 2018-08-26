using System;
using System.Configuration;

namespace ConfigPoc.Tests
{
    internal sealed class UserBankSection : ConfigurationSectionBase
    {
        [ConfigurationProperty(nameof(Name))]
        public string Name => base.GetValue();

        [ConfigurationProperty(nameof(Account))]
        public AccountElement Account => base.GetElement<AccountElement>();
    }

    internal sealed class AccountElement : ConfigurationElementBase
    {
        [ConfigurationProperty(nameof(Name))]
        public string Name => base.GetValue();

        [ConfigurationProperty(nameof(Balance))]
        public string Balance => base.GetValue();

        [ConfigurationProperty(nameof(Expiration))]
        public string Expiration => base.GetValue();

        [ConfigurationProperty(nameof(Creation))]
        public string Creation => base.GetValue();

        [ConfigurationProperty(nameof(Display))]
        public DisplayElement Display => base.GetElement<DisplayElement>();
    }

    internal sealed class DisplayElement : ConfigurationElementBase
    {
        [ConfigurationProperty(nameof(Name))]
        public string Name => base.GetValue();

        [ConfigurationProperty(nameof(State))]
        public string State => base.GetValue();

        [ConfigurationProperty(nameof(Update))]
        public string Update => base.GetValue();

        [ConfigurationProperty(nameof(MissingDateValue))]
        public string MissingDateValue => base.GetValue();
    }
}
