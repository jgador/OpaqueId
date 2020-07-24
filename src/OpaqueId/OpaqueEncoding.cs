using System;
using System.Linq;

namespace OpaqueId
{
    /// <summary>
    /// Encodes long type (i.e. current time since epoch) to an opaque string.
    /// </summary>
    internal class OpaqueEncoding
    {
        public OpaqueEncoding(string baseCharacters)
        {
            if (string.IsNullOrWhiteSpace(baseCharacters))
            {
                throw new ArgumentException($"'{nameof(baseCharacters)}' cannot be null, empty, or contain only whitespace.", nameof(baseCharacters));
            }

            BaseCharacters = baseCharacters;
        }

        /// <summary>
        /// Base characters to be used for encoding long type values.
        /// </summary>
        public readonly string BaseCharacters;

        /// <summary>
        /// Encodes the long type value to an opaque string of the chosen base characters.
        /// </summary>
        /// <param name="value">The long type value to be encoded to opaque string.</param>
        public string Convert(long value)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Number to be converted must be positive number and greater than zero.");
            }

            var encoded = ToBase((ulong)value, BaseCharacters);
            return encoded.ToString();
        }

        internal static TargetBasePlaceHolderCollection ToBase(ulong identifier, string baseCharacters)
        {
            if (string.IsNullOrWhiteSpace(baseCharacters))
            {
                throw new ArgumentNullException(nameof(baseCharacters));
            }
            if (baseCharacters.Length > byte.MaxValue || baseCharacters.Length < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(baseCharacters), $"Length of base characters should be > 2 and <= {byte.MaxValue}.");
            }
            if (baseCharacters.LongCount() > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(baseCharacters), $"Can only accommodate as many base characters as '{int.MaxValue}'.");
            }

            TargetBasePlaceHolderCollection placeHolders = new TargetBasePlaceHolderCollection();

            // This assumes that the length of the baseCharacters will be the target base number for conversion
            byte baseNumber = (byte)baseCharacters.Length;
            int placeValue = 0;
            ulong quotient = identifier;
            while (quotient > 0)
            {
                // Given that the base number is a byte, I think we can assure that the remainder will be an int
                int remainder = (int)(quotient % baseNumber);
                quotient /= baseNumber;
                placeValue++;
                placeHolders.Add(baseCharacters[remainder]);
            }

            return new TargetBasePlaceHolderCollection(placeHolders.Reverse());
        }
    }
}
