using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp9demo.Models
{
    public record Ingredient
    {
        public string Url { get; init; } = null!;
        public string Name { get; init; } = null!;
        public string Recipe { get; init; } = null!;
        public string Slug { get; init; } = null!;

        public bool NameHasPunctuation()
        {
            foreach(var c in Name)
                // any character that isn't a letter or space is punctuation
                if (c is not ((>= 'a' and <= 'z') or( >= 'A' and <= 'Z') or ' '))
                    return true;
            return false;
        }

        public virtual Ingredient SwapName(string newName) =>
            this with 
            {
                Url = string.Empty, // the new ingredient is no longer from the db
                Name = newName,
                Slug = string.Empty // the new ingredient is no longer from the db
            };
    }
}
