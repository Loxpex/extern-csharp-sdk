using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Model.Numbers
{
    /// <summary>
    /// Код ТОГС. Формат данных: XX-XX, где Х - это цифра от 0 до 9
    /// </summary>
    public record TogsCode
    {
        public static readonly RegexBasedParser<TogsCode> Parser = new(
            @"^\d{2}-\d{2}$",
            v => new TogsCode(v),
            (param, value) => Errors.InvalidAuthorityNumber(param, value, AuthorityNumberKind.Togs, "XX-XX")
        );

        /// <summary>
        /// Формат данных: XX-XX, где Х - это цифра от 0 до 9
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TogsCode Parse(string value) => Parser.Parse(value);

        private TogsCode(string value) => Value = value;

        public string Value { get; }
        public AuthorityNumberKind Kind => AuthorityNumberKind.Togs;
        
        public override string ToString() => Value;
    }
}