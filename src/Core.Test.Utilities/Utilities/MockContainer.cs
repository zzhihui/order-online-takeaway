using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace Core.Test.Utilities.Utilities
{
    public class MockContainer : IEnumerable<Mock>
    {
        private readonly Dictionary<Type, Mock> _mocks = new Dictionary<Type, Mock>();

        public T BuildSubjectUnderTest<T>() where T : class
        {
            var constructor = ReflectionHelper.GetConstructor<T>();
            var types = ReflectionHelper.GetParameterTypes(constructor);

            var arguments = types.Select(type => ReflectionHelper.InvokeGenericMethod(this, "AddMock", type)).ToList();

            return ReflectionHelper.Instantiate<T>(constructor, arguments);
        }

        public T AddMock<T>() where T : class
        {
            if (!_mocks.ContainsKey(typeof(T)))
                _mocks.Add(typeof(T), new Mock<T>());

            return ((Mock<T>)_mocks[typeof(T)]).Object;
        }

        public Mock<T> GetMock<T>() where T : class
        {
            if (_mocks.ContainsKey(typeof(T)))
                return (Mock<T>)_mocks[typeof(T)];

            throw new Exception(
                String.Format(
                    "No mock has been created for type {0}", typeof(T).Name));
        }

        public void Clear()
        {
            _mocks.Clear();
        }

        public IEnumerator<Mock> GetEnumerator()
        {
            return _mocks.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _mocks.Values.GetEnumerator();
        }
    }
}