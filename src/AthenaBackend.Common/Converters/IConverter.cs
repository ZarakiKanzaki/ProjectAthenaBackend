using System;

namespace AthenaBackend.Common.Converters
{
    public abstract class BaseConverterWithValidation<Ffrom, Tto, Cconverter> : IConverter<Ffrom, Tto>
    {
        public virtual Tto Convert(Ffrom objectToConvert)
        {
            ValidateObjectToConvert(objectToConvert);
            return GetConvertedObject(objectToConvert);
        }

        protected virtual void ValidateObjectToConvert(Ffrom objectToConvert)
        {
            if (IsObjectInvalid(objectToConvert))
            {
                var temp = $"{nameof(ValidateObjectToConvert)} method of {typeof(Cconverter).Name} was called with invalid {nameof(objectToConvert)}: null or empty.";
                throw new ArgumentNullException(temp);
            }
        }

        protected virtual bool IsObjectInvalid(Ffrom objectToValidate) => objectToValidate == null;

        protected abstract Tto GetConvertedObject(Ffrom objectToConvert);
    }

    public interface IConverter<F, T>
    {
        public T Convert(F objectToConvert);
    }
}
