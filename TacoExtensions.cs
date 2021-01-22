using csharp9demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp9demo
{
    public static class TacoExtensions
    {
        public static IEnumerator<Ingredient> GetEnumerator(this Taco t)
        {
            if(t.Seasoning is not null) yield return t.Seasoning;
            if (t.Condiment is not null) yield return t.Condiment;
            if (t.Mixin is not null) yield return t.Mixin;
            if (t.BaseLayer is not null) yield return t.BaseLayer;
            if (t.Shell is not null) yield return t.Shell;
        }
    }
}
