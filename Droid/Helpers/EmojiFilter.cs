using System.Text.RegularExpressions;
using Android.Text;
using Java.Lang;

namespace XamarinNativeTemplate.Droid
{
    public class EmojiFilter : Object, IInputFilter
    {
        public ICharSequence FilterFormatted(ICharSequence source, int start, int end, ISpanned dest, int dstart, int dend)
        {
            var regex = new Regex(@"[a-zA-Z0-9/.,!\n ]");

            if (!regex.IsMatch(source.ToString()) && source.ToString().Length > 0)
                return new String(string.Empty);

            return source;
        }
    }
}