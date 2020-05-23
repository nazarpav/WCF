using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DAL_;

namespace Translator
{
    public class Service1 : IService1
    {
        private DAL _DAL = new DAL();
        public void AddNewWord_WordTranslate(string word, string wordTranslate)
        {
            _DAL.AddNewWord_WordTranslate(word, wordTranslate);
        }
        public string[] GetTranslationsWords(string word)
        {
            return _DAL.GetTranslationsWords(word);
        }
    }
}
