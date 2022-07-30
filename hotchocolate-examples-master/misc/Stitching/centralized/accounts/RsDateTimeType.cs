 
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using HotChocolate.Language;
    using HotChocolate.Properties;
using HotChocolate.Types;

namespace Accounts;

    public class RsDateTimeType : ScalarType<DateTime, StringValueNode>
    {
        private const string _dateFormat = "yyyy-MM-ddTHH:mm:ss";

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeType"/> class.
        /// </summary>
        public RsDateTimeType() :this("RsDateTime","RsDateTime")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeType"/> class.
        /// </summary>
        public RsDateTimeType(
            String name,
            string? description = null,
            BindingBehavior bind = BindingBehavior.Explicit)
            : base(name, bind)
        {
            Description = description;
        }

        protected override DateTime ParseLiteral(StringValueNode valueSyntax)
        {
            if (TryDeserializeFromString(valueSyntax.Value, out DateTime? value))
            {
                return value.Value;
            }

            throw new SerializationException(
                "Can not parse value.Parse Error",
                this);
        }

        protected override StringValueNode ParseValue(DateTime runtimeValue) =>
            new(Serialize(runtimeValue));

        public override IValueNode ParseResult(object? resultValue)
        {
            if (resultValue is null)
            {
                return NullValueNode.Default;
            }

            if (resultValue is string s)
            {
                return new StringValueNode(s);
            }

            if (resultValue is DateTimeOffset d)
            {
                return ParseValue(d);
            }

            if (resultValue is DateTime dt)
            {
                return ParseValue(new DateTimeOffset(dt.ToUniversalTime(),dt.TimeOfDay));
            }

            throw new SerializationException(
                "Serialization Exception",
                this);
        }

        public override bool TrySerialize(object? runtimeValue, out object? resultValue)
        {
            if (runtimeValue is null)
            {
                resultValue = null;
                return true;
            }

            if (runtimeValue is DateTime dt)
            {
                resultValue = Serialize(dt);
                return true;
            }

            resultValue = null;
            return false;
        }

        public override bool TryDeserialize(object? resultValue, out object? runtimeValue)
        {
            if (resultValue is null)
            {
                runtimeValue = null;
                return true;
            }

            if (resultValue is string s && TryDeserializeFromString(s, out DateTime? d))
            {
                runtimeValue = d;
                return true;
            }

            if (resultValue is DateTimeOffset dt)
            {
                runtimeValue = dt.UtcDateTime;
                return true;
            }

            if (resultValue is DateTime)
            {
                runtimeValue = resultValue;
                return true;
            }

            runtimeValue = null;
            return false;
        }

        private static string Serialize(DateTime value) =>
            value.Date.ToString(_dateFormat, CultureInfo.InvariantCulture);

        private static bool TryDeserializeFromString(
            string? serialized,
            [NotNullWhen(true)] out DateTime? value)
        {
            if (DateTime.TryParse(
               serialized,
               CultureInfo.InvariantCulture,
               DateTimeStyles.AssumeLocal,
               out DateTime dateTime))
            {
                value = dateTime;
                return true;
            }

            value = null;
            return false;
        }
    }


