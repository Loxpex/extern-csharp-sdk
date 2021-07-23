using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Model.Numbers
{
    /// <summary>
    /// КПП для юрлиц. Формат данных: 123456789
    /// </summary>
    public record Kpp
    {
        public static readonly RegexBasedParser<Kpp> Parser = 
            new(@"^\d{9}$", v => new Kpp(v), (param, value) => Errors.InvalidAuthorityNumber(param, value, AuthorityNumberKind.Kpp, "XXXXXXXXX"));

        /// <summary>
        /// Формат данных: 123456789
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Kpp Parse(string value) => Parser.Parse(value);

        private Kpp(string value) => Value = value;

        public string Value { get; }
        public AuthorityNumberKind Kind => AuthorityNumberKind.Kpp;
        
        public override string ToString() => Value;
    }
}