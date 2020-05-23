namespace DAL_
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Dictionary : DbContext
    {
        public Dictionary()
            : base("name=Dictionary")
        {
        }
        public virtual DbSet<Word_WordTranslate> Word_WordTranslate { get; set; }
    }
    public class Word_WordTranslate
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string WordTranslate { get; set; }
    }
}