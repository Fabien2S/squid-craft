using System.Globalization;

namespace SquidCraft.API.I18N
{
    public interface II18NManager
    {
        string Locale { get; }
        void LoadLanguage(CultureInfo cultureInfo);
        string this[int key] { get; }
    }
}