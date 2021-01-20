using System;
using System.Collections.Generic;
using System.Text;

namespace csharp9demo.Models
{
    public class Taco : Ingredient
    {
        public Ingredient? Seasoning { get; set; }
        public Ingredient? Condiment { get; set; }
        public Ingredient? Mixin { get; set; }
        public Ingredient? BaseLayer { get; set; }
        public Ingredient? Shell { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder($"{Name}: {BaseLayer?.Name}");

            if (Mixin != null || Condiment != null) sb.Append(" covered in ");
            if (Mixin != null) sb.Append(Mixin.Name);
            if (Mixin != null && Condiment != null) sb.Append(" and ");
            if (Condiment != null) sb.Append(Condiment.Name);
            if (Seasoning != null) sb.Append($", topped with {Seasoning.Name}");
            if (Shell != null) sb.Append($" in a {Shell.Name} shell");
            return sb.ToString();
        }


        public static Taco GetEmptyTaco() => new Taco();

        public bool IsFull() =>
            !(Seasoning is null)
                && !(Condiment is null)
                && !(Mixin is null)
                && !(BaseLayer is null)
                && !(Shell is null);

        public override Ingredient SwapName(string newName)
        {
            var t = new Taco
            {
                Url = string.Empty,
                Name = newName,
                Recipe = Recipe,
                Slug = string.Empty,
                Seasoning = Seasoning,
                Condiment = Condiment, 
                Mixin = Mixin,
                BaseLayer = BaseLayer,
                Shell = Shell
            };
            return t;
        }
    }
}
