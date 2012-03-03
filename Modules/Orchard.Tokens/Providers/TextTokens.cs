using System;
using System.Web;
using Orchard.Localization;

namespace Orchard.Tokens.Providers {
    public class TextTokens : ITokenProvider {
        public TextTokens() {
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public void Describe(DescribeContext context) {
            context.For("Text", T("Text"), T("Tokens for text strings"))
                .Token("Limit:*", T("Limit:<text length><:limit string>"), T("Limit text to specified length and append ellipsis (...) or alternative limit string"))
                .Token("Format:*", T("Format:<text format>"), T("Optional format specifier (e.g. foo{0}bar). See format strings at <a target=\"_blank\" href=\"http://msdn.microsoft.com/en-us/library/az4se3k1.aspx\">Standard Formats</a> and <a target=\"_blank\" href=\"http://msdn.microsoft.com/en-us/library/8kb3ddd4.aspx\">Custom Formats</a>"), "DateTime")
                .Token("UrlEncode", T("Url Encode"), T("Encodes a URL string."), "Text")
                .Token("HtmlEncode", T("Html Encode"), T("Encodes an HTML string."), "Text")
                ;
        }
        public void Evaluate(EvaluateContext context) {
            context.For<String>("Text", () => "")
                // {Text}
                .Token(
                    token => token == String.Empty ? String.Empty : null,
                    (token, d) => d.ToString())
                // {Text.Limit:<length><:ellipsis>}
                .Token(
                    token => {
                        if (token.StartsWith("Limit:", StringComparison.OrdinalIgnoreCase)) {
                            var param = token.Substring("Limit:".Length);
                            var split = token.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                            if (split.Length > 0 && split.Length <= 2) {
                                return param;
                            }
                        }
                        return null;
                    },
                    (token, t) => Limit(token,t))
                // {Text.Format:<formatstring>}
                .Token(
                    token => token.StartsWith("Format:", StringComparison.OrdinalIgnoreCase) ? token.Substring("Format:".Length) : null,
                    (token, d) => String.Format(d,token))
                .Token("UrlEncode", text => HttpUtility.UrlEncode(text))
                .Token("HtmlEncode", text => HttpUtility.HtmlEncode(text))
                ;
                
        }

        private string Limit(string token, string param) {
            // Check if we have an ellipsis override
            var split = param.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            string lengthText = split[0];
            string ellipsis = (split.Length > 1)?split[1]:"...";
            
            var length = Convert.ToInt16(lengthText);
            if (token.Length > length) {
                token = token.Substring(0, length - ellipsis.Length) + ellipsis;
            }
            return token;
        }

    }
}