using System;
using System.Collections.Generic;
using System.Text;

namespace csharp9demo.Models
{
    public record Taco : Ingredient
    {
        public Ingredient? Seasoning { get; init; }
        public Ingredient? Condiment { get; init; }
        public Ingredient? Mixin { get; init; }
        public Ingredient? BaseLayer { get; init; }
        public Ingredient? Shell { get; init; }

        public override string ToString()
        {
            var sb = new StringBuilder($"{Name}: {BaseLayer?.Name}");

            if (Mixin is not null || Condiment is not null) sb.Append(" covered in ");
            if (Mixin is not null) sb.Append(Mixin.Name);
            if ((Mixin, Condiment) is (object, object)) sb.Append(" and ");
            if (Condiment is not null) sb.Append(Condiment.Name);
            if (Seasoning is not null) sb.Append($", topped with {Seasoning.Name}");
            if (Shell is not null) sb.Append($" in a {Shell.Name} shell");
            return sb.ToString();
        }


        public static Taco GetEmptyTaco() => new Taco();

        public bool IsFull() => (Seasoning, Condiment, Mixin, BaseLayer, Shell) is (object, object, object, object, object);

        public override Taco SwapName(string newName) =>
            this with
            {
                Url = string.Empty,
                Name = newName,
                Slug = string.Empty
            };
    }
}
