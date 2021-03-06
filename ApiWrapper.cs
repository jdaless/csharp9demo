﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using csharp9demo.Models;
using System.Dynamic;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace csharp9demo
{
    public class ApiWrapper
    {
        private HttpClient httpClient;
        public ApiWrapper(HttpClient httpClient) => this.httpClient = httpClient;

        public async Task<Taco> GetRandomTaco(bool fullTaco = false)
        {
            var json = await httpClient.GetStringAsync($"/random/?full-taco={fullTaco}");

            dynamic apiObject = JsonConvert.DeserializeObject<dynamic>(
                json,
                new ExpandoObjectConverter())!;

            static Ingredient? ingredientConverter(dynamic d) => 
                d != null 
                    ? new Ingredient
                    {
                        Url = d.url,
                        Name = d.name,
                        Recipe = d.recipe,
                        Slug = d.slug
                    } 
                    : null;

            return new Taco
            {
                Name = apiObject.name,
                Url = apiObject.url,
                Recipe = apiObject.recipe,
                Slug = apiObject.slug,
                Seasoning = ingredientConverter(apiObject.seasoning),
                Condiment = ingredientConverter(apiObject.condiment),
                Mixin = ingredientConverter(apiObject.mixin),
                BaseLayer = ingredientConverter(apiObject.base_layer),
                Shell = ingredientConverter(apiObject.shell)
            };
        }

        /// <summary>
        /// Returns a taco whose ingredients have no punctuation in the names.
        /// </summary>
        /// <param name="fullTaco"></param>
        /// <returns></returns>
        public Task<Taco> GetCleanRandomTaco(bool fullTaco = false) =>
            GetGoodRandomTaco((_, i) => !i.NameHasPunctuation(), fullTaco);

        /// <summary>
        /// Returns a taco whose name contains the name of all of its ingredients.
        /// </summary>
        /// <param name="fullTaco"></param>
        /// <returns></returns>
        public Task<Taco> GetDescriptiveRandomTaco(bool fullTaco = false) =>
            GetGoodRandomTaco((t, i) => t.Name.Contains(i.Name), fullTaco);

        /// <summary>
        /// Get a random taco where each ingredient meets some parameter, what good means
        /// is up to the consumer.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="fullTaco"></param>
        /// <returns></returns>
        public async Task<Taco> GetGoodRandomTaco(Func<Taco, Ingredient, bool> func, bool fullTaco = false)
        {
            Taco t;
            bool good;
            do
            {
                good = true;
                t = await GetRandomTaco(fullTaco);
                var enumerator = t.GetEnumerator();
                while (enumerator.MoveNext())
                    if (!func(t, enumerator.Current))
                        good = false;
            }
            while (!good);

            return t;
        }
    }
}
