using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GoeaLabs.Bedrock
{
    /// <summary>
    /// Integers utility methods.
    /// </summary>
    public static class Integers
    {

        /// <summary>
        /// Minimum possible value for an 128 bit unsigned <see cref="BigInteger"/>.
        /// </summary>
        public static readonly BigInteger BigUInt128Min = 0;

        /// <summary>
        /// Maximum possible value for an 128 bit unsigned <see cref="BigInteger"/>.
        /// </summary>
        public static readonly BigInteger BigUInt128Max = BigInteger.Pow(2, 128) - 1;

        /// <summary>
        /// Minimum possible value for an 128 bit signed <see cref="BigInteger"/>.
        /// </summary>
        public static readonly BigInteger BigInt128Min = -BigInteger.Pow(2, 127);

        /// <summary>
        /// Maximum possible value for an 128 bit signed <see cref="BigInteger"/>.
        /// </summary>
        public static readonly BigInteger BigInt128Max = BigInteger.Pow(2, 127) - 1;

        /// <summary>
        /// Minimum possible value for an 256 bit unsigned <see cref="BigInteger"/>.
        /// </summary>
        public static readonly BigInteger BigUInt256Min = 0;

        /// <summary>
        /// Maximum possible value for an 256 bit unsigned <see cref="BigInteger"/>.
        /// </summary>
        public static readonly BigInteger BigUInt256Max = BigInteger.Pow(2, 256) - 1;

        /// <summary>
        /// Minimum possible value for an 256 bit signed <see cref="BigInteger"/>.
        /// </summary>
        public static readonly BigInteger BigInt256Min = -BigInteger.Pow(2, 255);

        /// <summary>
        /// Maximum possible value for an 256 bit signed <see cref="BigInteger"/>.
        /// </summary>
        public static readonly BigInteger BigInt256Max = BigInteger.Pow(2, 255) - 1;

        /// <summary>
        /// Minimum possible value for an 512 bit unsigned <see cref="BigInteger"/>.
        /// </summary>
        public static readonly BigInteger BigUInt512Min = 0;

        /// <summary>
        /// Maximum possible value for an 512 bit unsigned <see cref="BigInteger"/>.
        /// </summary>
        public static readonly BigInteger BigUInt512Max = BigInteger.Pow(2, 512) - 1;

        /// <summary>
        /// Minimum possible value for an 512 bit signed <see cref="BigInteger"/>.
        /// </summary>
        public static readonly BigInteger BigInt512Min = -BigInteger.Pow(2, 511);

        /// <summary>
        /// Maximum possible value for an 512 bit signed <see cref="BigInteger"/>.
        /// </summary>
        public static readonly BigInteger BigInt512Max = BigInteger.Pow(2, 511) - 1;


        /// <summary>
        /// Applies min-max scaling.
        /// </summary>
        /// <remarks>
        /// Throws <see cref="ArgumentException"/>:
        /// <list type="bullet">
        /// <item>If <paramref name="minN"/> is not less than or equal to <paramref name="minR"/>.</item>
        /// <item>If <paramref name="maxN"/> is not greater than or equal to <paramref name="maxR"/>.</item>
        /// <item>If <paramref name="maxR"/> is not greater than <paramref name="minR"/>.</item>
        /// </list>
        /// </remarks>
        /// <param name="srcN">Source number.</param>
        /// <param name="minN">Minimum possible value of the number.</param>
        /// <param name="maxN">Maximum possible value of the number.</param>
        /// <param name="minR">Minimum possible value of the range.</param>
        /// <param name="maxR">Maximum possible value of the range.</param>
        /// <returns>The number scaled in the given range.</returns>
        public static ulong ScaleUnsigned64(ulong srcN, ulong minN, ulong maxN, ulong minR, ulong maxR)
        {
            if (!(minN <= minR))
                throw new ArgumentException($"{nameof(minN)} must be less than or equal {nameof(minR)}");
            if (!(maxN >= maxR))
                throw new ArgumentException($"{nameof(maxN)} must be greater than or equal {nameof(maxR)}");
            if (!(maxR > minR))
                throw new ArgumentException($"{nameof(maxR)} must be greater than {nameof(minR)}");

            if (srcN >= minR && srcN <= maxR) return srcN;

            return minR + (srcN - minN) * (maxR - minR) / (maxN - minN);
        }

        /// <summary>
        /// Applies min-max scaling.
        /// </summary>
        /// <remarks>
        /// Throws <see cref="ArgumentException"/>:
        /// <list type="bullet">
        /// <item>If <paramref name="minN"/> is not less than or equal to <paramref name="minR"/>.</item>
        /// <item>If <paramref name="maxN"/> is not greater than or equal to <paramref name="maxR"/>.</item>
        /// <item>If <paramref name="maxR"/> is not greater than <paramref name="minR"/>.</item>
        /// </list>
        /// </remarks>
        /// <param name="srcN">Source number.</param>
        /// <param name="minN">Minimum possible value of the number.</param>
        /// <param name="maxN">Maximum possible value of the number.</param>
        /// <param name="minR">Minimum possible value of the range.</param>
        /// <param name="maxR">Maximum possible value of the range.</param>
        /// <returns>The number scaled in the given range.</returns>
        public static long ScaleSigned64(long srcN, long minN, long maxN, long minR, long maxR)
        {
            if (srcN >= int.MinValue && srcN <= int.MaxValue &&
                minR >= int.MinValue && minR <= int.MaxValue &&
                maxR >= int.MinValue && maxR <= int.MinValue)
            {
                if (!(minN <= minR))
                    throw new ArgumentException($"{nameof(minN)} must be less than or equal to {nameof(minR)}");
                if (!(maxN >= maxR))
                    throw new ArgumentException($"{nameof(maxN)} must be greater than or equal to {nameof(maxR)}");
                if (!(maxR > minR))
                    throw new ArgumentException($"{nameof(maxR)} must be greater than {nameof(minR)}");

                if (srcN >= minR && srcN <= maxR) return srcN;

                return minR + (srcN - minN) * (maxR - minR) / (maxN - minN);
            }

#if NET7_0_OR_GREATER
            return (long)ScaleSigned128(srcN, minN, maxN, minR, maxR);
#else
            return (long)ScaleBigInt(srcN, minN, maxN, minR, maxR);
#endif
        }

        /// <summary>
        /// Applies min-max scaling.
        /// </summary>
        /// <remarks>
        /// Throws <see cref="ArgumentException"/>:
        /// <list type="bullet">
        /// <item>If <paramref name="minN"/> is not less than or equal to <paramref name="minR"/>.</item>
        /// <item>If <paramref name="maxN"/> is not greater than or equal to <paramref name="maxR"/>.</item>
        /// <item>If <paramref name="maxR"/> is not greater than <paramref name="minR"/>.</item>
        /// </list>
        /// </remarks>
        /// <param name="srcN">Source number.</param>
        /// <param name="minN">Minimum possible value of the number.</param>
        /// <param name="maxN">Maximum possible value of the number.</param>
        /// <param name="minR">Minimum possible value of the range.</param>
        /// <param name="maxR">Maximum possible value of the range.</param>
        /// <returns>The number scaled in the given range.</returns>
        public static BigInteger ScaleBigInt(BigInteger srcN, BigInteger minN, BigInteger maxN, BigInteger minR, BigInteger maxR)
        {
            if (!(minN <= minR))
                throw new ArgumentException($"{nameof(minN)} must be less than or equal to {nameof(minR)}");
            if (!(maxN >= maxR))
                throw new ArgumentException($"{nameof(maxN)} must be greater than or equal to {nameof(maxR)}");
            if (!(maxR > minR))
                throw new ArgumentException($"{nameof(maxR)} must be greater than {nameof(minR)}");

            if (srcN >= minR && srcN <= maxR) return srcN;

            return minR + (srcN - minN) * (maxR - minR) / (maxN - minN);
        }

#if NET7_0_OR_GREATER

        /// <summary>
        /// Applies min-max scaling.
        /// </summary>
        /// <remarks>
        /// Throws <see cref="ArgumentException"/>:
        /// <list type="bullet">
        /// <item>If <paramref name="minN"/> is not less than or equal to <paramref name="minR"/>.</item>
        /// <item>If <paramref name="maxN"/> is not greater than or equal to <paramref name="maxR"/>.</item>
        /// <item>If <paramref name="maxR"/> is not greater than <paramref name="minR"/>.</item>
        /// </list>
        /// </remarks>
        /// <param name="srcN">Source number.</param>
        /// <param name="minN">Minimum possible value of the number.</param>
        /// <param name="maxN">Maximum possible value of the number.</param>
        /// <param name="minR">Minimum possible value of the range.</param>
        /// <param name="maxR">Maximum possible value of the range.</param>
        /// <returns>The number scaled in the given range.</returns>
        public static UInt128 ScaleUnsigned128(UInt128 srcN, UInt128 minN, UInt128 maxN, UInt128 minR, UInt128 maxR)
        {
            if (!(minN <= minR))
                throw new ArgumentException($"{nameof(minN)} must be less than or equal to {nameof(minR)}");
            if (!(maxN >= maxR))
                throw new ArgumentException($"{nameof(maxN)} must be greater than or equal to {nameof(maxR)}");
            if (!(maxR > minR))
                throw new ArgumentException($"{nameof(maxR)} must be greater than {nameof(minR)}");

            if (srcN >= minR && srcN <= maxR) return srcN;

            return minR + (srcN - minN) * (maxR - minR) / (maxN - minN);
        }

        /// <summary>
        /// Applies min-max scaling.
        /// </summary>
        /// <remarks>
        /// Throws <see cref="ArgumentException"/>:
        /// <list type="bullet">
        /// <item>If <paramref name="minN"/> is not less than or equal to <paramref name="minR"/>.</item>
        /// <item>If <paramref name="maxN"/> is not greater than or equal to <paramref name="maxR"/>.</item>
        /// <item>If <paramref name="maxR"/> is not greater than <paramref name="minR"/>.</item>
        /// </list>
        /// </remarks>
        /// <param name="srcN">Source number.</param>
        /// <param name="minN">Minimum possible value of the number.</param>
        /// <param name="maxN">Maximum possible value of the number.</param>
        /// <param name="minR">Minimum possible value of the range.</param>
        /// <param name="maxR">Maximum possible value of the range.</param>
        /// <returns>The number scaled in the given range.</returns>
        public static Int128 ScaleSigned128(Int128 srcN, Int128 minN, Int128 maxN, Int128 minR, Int128 maxR)
        {
            if (srcN >= long.MinValue && srcN <= long.MaxValue && 
                minR >= long.MinValue && minR <= long.MaxValue &&
                maxR >= long.MinValue && maxR <= long.MinValue)
            {
                if (!(minN <= minR))
                    throw new ArgumentException($"{nameof(minN)} must be less than or equal to {nameof(minR)}");
                if (!(maxN >= maxR))
                    throw new ArgumentException($"{nameof(maxN)} must be greater than or equal to {nameof(maxR)}");
                if (!(maxR > minR))
                    throw new ArgumentException($"{nameof(maxR)} must be greater than {nameof(minR)}");

                if (srcN >= minR && srcN <= maxR) return srcN;

                return minR + (srcN - minN) * (maxR - minR) / (maxN - minN);
            }

            return (Int128)ScaleBigInt(srcN, minN, maxN, minR, maxR);
        }

#endif
    }
}
