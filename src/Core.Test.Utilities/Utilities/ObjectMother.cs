using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.AutoFixture;

namespace Core.Test.Utilities.Utilities
{
    public class ObjectMother
    {
        private static readonly Fixture AutoFixture = new Fixture();

        private static readonly IDictionary<Type, Func<Object>> Mappings = new Dictionary<Type, Func<Object>>()
        {
            { typeof(int), () => AutoFixture.Create<int>() },
            { typeof(int?), () => AutoFixture.Create<int>() },
            { typeof(long), () => AutoFixture.Create<long>() },
            { typeof(long?), () => AutoFixture.Create<long>() },
            { typeof(decimal), () => AutoFixture.Create<decimal>() },
            { typeof(decimal?), () => AutoFixture.Create<decimal>() },
            { typeof(double?), () => AutoFixture.Create<int>() },
            { typeof(bool), () => AutoFixture.Create<bool>() },
            { typeof(bool?), () => AutoFixture.Create<bool>() },
            { typeof(DateTime), () => AutoFixture.Create<DateTime>() },
            { typeof(DateTime?), () => AutoFixture.Create<DateTime>() },
            { typeof(string), () => AutoFixture.Create<string>() }
        };

        public static T CreateAndFill<T>() where T : new()
        {
            var entity = new T();
            Fill(entity);
            return entity;
        }

        public static IEnumerable<T> CreateAndFill<T>(int quantity) where T : new()
        {
            return Enumerable.Range(0, quantity).Select(i => CreateAndFill<T>());
        }

        public static T CreateAndFill<T>(Action<T> customisation) where T : new()
        {
            var entity = CreateAndFill<T>();
            customisation(entity);
            return entity;
        }

        public static IEnumerable<T> CreateAndFill<T>(int quantity, Action<T> customisation) where T : new()
        {
            return Enumerable.Range(0, quantity).Select(i => CreateAndFill<T>(customisation));
        }

        private static void Fill<T>(T entity)
        {
            var type = entity.GetType();
            foreach (var property in type.GetProperties())
            {
                Fill(entity, property);
            }
        }

        private static void Fill<T>(T entity, PropertyInfo property)
        {
            var type = property.PropertyType;
            if (!Mappings.ContainsKey(type)) return;
            if (property.CanWrite)
                property.SetValue(entity, Mappings[type].Invoke(), null);
        }
    }
}