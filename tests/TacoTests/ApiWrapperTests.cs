using csharp9demo;
using csharp9demo.Models;
using NUnit.Framework;
using System.Linq;

namespace TacoTests
{
    public class ApiWrapperTests
    {
        ApiWrapper api = null!;

        [SetUp]
        public void Setup() =>
            api = new(new System.Net.Http.HttpClient { BaseAddress = new("http://taco-randomizer.herokuapp.com") });

        [Test]
        public void TestGetRandomTaco()
        {
            _ = api.GetRandomTaco();
        }

        [Test]
        public void TestGetRandomFullTaco()
        {
            var t = api.GetRandomTaco(true).Result;
            // taco should have ingredient data
            Assert.True(t.Name != null);
            Assert.True(t.Url != null);
            Assert.True(t.Slug != null);
            Assert.True(t.Recipe != null);
        }

        [Test]
        public void TestGetCleanRandomTaco()
        {
            var t = api.GetCleanRandomTaco().Result;
            var e = t.GetEnumerator();
            for (Ingredient? ingredient = null; e.MoveNext(); ingredient = e.Current)
                Assert.IsTrue(ingredient?.Name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c) || c == ':') ?? true);
        }

        [Test]
        public void TestGetDescriptiveRandomTaco()
        {
            var t = api.GetDescriptiveRandomTaco().Result;
            Assert.IsTrue(t.Name.Contains(t.Seasoning?.Name ?? string.Empty));
            Assert.IsTrue(t.Name.Contains(t.Condiment?.Name ?? string.Empty));
            Assert.IsTrue(t.Name.Contains(t.Mixin?.Name ?? string.Empty));
            Assert.IsTrue(t.Name.Contains(t.BaseLayer?.Name ?? string.Empty));
            Assert.IsTrue(t.Name.Contains(t.Shell?.Name ?? string.Empty));
        }
    }
}