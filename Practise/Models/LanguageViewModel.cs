using Practise.DAL.Entities;

namespace Practise.Models
{
    public class LanguageViewModel
    {
        public List<Language> Languages { get; set; } 
        public Language SelectedLanguage { get; set; }
    }
}
