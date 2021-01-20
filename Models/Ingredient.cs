using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp9demo.Models
{
    public class Ingredient
    {
        public string Url { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Recipe { get; set; } = null!;
        public string Slug { get; set; } = null!;

        public bool NameHasPunctuation()
        {
            foreach(var c in Name)
                // any character that isn't a letter or space is punctuation
                if (!((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c == ' ')))
                    return true;
            return false;
        }

        public virtual Ingredient SwapName(string newName) =>
            new Ingredient
            {
                Url = string.Empty, // the new ingredient is no longer from the db
                Name = newName,
                Recipe = Recipe,
                Slug = string.Empty // the new ingredient is no longer from the db
            };
    }
}
