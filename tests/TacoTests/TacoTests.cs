using csharp9demo;
using csharp9demo.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TacoTests
{
    public class TacoTests
    {
        ApiWrapper api = null!;

        [SetUp]
        public void Setup() =>
            api = new(new System.Net.Http.HttpClient { BaseAddress = new("http://taco-randomizer.herokuapp.com") });

        [Test]
        public void EmptyTacoTest() => _ = Taco.GetEmptyTaco();

        [Test]
        public void ToStringTest() => Assert.IsTrue(api.GetRandomTaco().ToString() is not null);

        [Test]
        public void IsFullTest()
        {
            var emptyIngredient = new Ingredient();
            var t = Taco.GetEmptyTaco() with
            {
                BaseLayer = emptyIngredient,
                Condiment = emptyIngredient,
                Mixin = emptyIngredient,
                Seasoning = emptyIngredient,
                Shell = emptyIngredient
            };
            Assert.IsTrue(t.IsFull());
            t = t with { Condiment = null };
            Assert.IsFalse(t.IsFull());
        }

        [Test]
        public void SwapNameTest()
        {
            var newName = new Guid().ToString();
            Assert.IsTrue(api.GetRandomTaco(true).Result.SwapName(newName).Name == newName);
            Assert.IsTrue(api.GetRandomTaco(true).Result.SwapName(newName).Slug == string.Empty);
            var t = api.GetRandomTaco(true).Result;
            Assert.IsTrue(((Taco)t.SwapName(newName)).Shell == t.Shell);
        }

    }
}
