using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_
{
    public class DAL
    {
        public void AddNewWord_WordTranslate(string word, string wordTranslate)
        {
            try
            {
                using (DAL_.Dictionary ctx = new Dictionary())
                {
                    ctx.Word_WordTranslate.Add(new Word_WordTranslate() { Word = word, WordTranslate = wordTranslate });
                    ctx.SaveChanges();
                }
            }
            catch(Exception  ex)
            {
                var exs = ex.Message;
            }
        }
        public string[] GetTranslationsWords(string word)
        {
            using (DAL_.Dictionary ctx = new Dictionary())
            {
                return ctx.Word_WordTranslate.Where(w => w.Word == word).Select(q => q.WordTranslate).ToArray();
            }
        }
    }
}
