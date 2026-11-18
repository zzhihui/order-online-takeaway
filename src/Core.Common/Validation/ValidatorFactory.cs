using System;
using Autofac;
using FluentValidation;

namespace Core.Common.Validation
{
    public class ValidatorFactory : ValidatorFactoryBase, IValidatorFactory
    {
        private readonly IComponentContext _container;

        public ValidatorFactory(IComponentContext container)
        {
            _container = container;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            object validator;

            if (_container.TryResolve(validatorType, out validator))
                return validator as IValidator;

            return null;
        }
    }
}