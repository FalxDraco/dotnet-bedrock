﻿/*
   Copyright 2022, GoeaLabs

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 */

using CommunityToolkit.Diagnostics;


namespace GoeaLabs.Bedrock.Extensions
{

    /// <summary>
    /// Extensions for integer spans.
    /// </summary>
    public static class SpansEx
    {

        /// <summary>
        /// Performs an element-by-element equality test between 
        /// <paramref name="self"/> and <paramref name="that"/>.
        /// </summary>
        /// <param name="self">Compared array.</param>
        /// <param name="that">Comparand array.</param>
        /// <returns><b>True</b> if all elements are equal, otherwise <b>False</b>.</returns>
        public static bool IsEqual(this Span<byte> self, Span<byte> that)
        {
            if (self.Length != that.Length)
                return false;

            for (int i = 0; i < self.Length; i++)
                if (self[i] != that[i]) return false;

            return true;
        }

        /// <summary>
        /// Writes the content of a span of <see cref="uint"/>(s) to a span of <see cref="byte"/>(s).
        /// </summary>
        /// <remarks>
        /// Throws <see cref="ArgumentException"/>:
        /// <list type="bullet">
        /// <item>If the length of <paramref name="self"/> is greater than <c>int.MaxValue / sizeof(uint)</c>.</item>
        /// <item>If the length of <paramref name="that"/> is less than <c>self.Length * sizeof(uint)</c>.</item>
        /// </list>
        /// </remarks>
        /// <param name="self">The span to split.</param>
        /// <param name="that">The span to write to.</param>
        public static void Split(this Span<uint> self, Span<byte> that)
        {
            var selfMax = int.MaxValue / sizeof(uint);
            Guard.HasSizeLessThanOrEqualTo(self, selfMax);

            var thatMin = self.Length * sizeof(uint);
            Guard.HasSizeGreaterThanOrEqualTo(that, thatMin);

            for (int i = 0; i < self.Length; i++)
            {
                self[i].Halve(out ushort n16a, out ushort n16b);
                
                n16a.Halve(out byte n8a, out byte n8b);
                n16b.Halve(out byte n8c, out byte n8d);

                var spot = i * sizeof(uint);

                that[spot++] = n8a;
                that[spot++] = n8b;
                that[spot++] = n8c;
                that[spot++] = n8d;
            }
        }

        /// <summary>
        /// Writes the content of a span of <see cref="byte"/>(s) to a span of <see cref="uint"/>(s).
        /// </summary>
        /// <remarks>
        /// Throws <see cref="ArgumentException"/>:
        /// <list type="bullet">
        /// <item>If the length of <paramref name="self"/> is a multiple of <c>sizeof(uint)</c>.</item>
        /// <item>If the length of <paramref name="that"/> is less than <c>self.Length / sizeof(uint)</c>.</item>
        /// </list>
        /// </remarks>
        /// <param name="self">The span to split.</param>
        /// <param name="that">The span to write to.</param>
        public static void Merge(this Span<byte> self, Span<uint> that)
        {
            var quot = sizeof(uint);

            if (self.Length % quot > 0)
                ThrowHelper.ThrowArgumentException(
                    nameof(self), $"Length must be a multiple of {quot}");

            Guard.HasSizeGreaterThanOrEqualTo(that, self.Length / quot);

            for (int i = 0; i < that.Length; i++)
            {
                var spot = i * quot;

                var n16a = self[spot].Merge(self[++spot]);
                var n16b = self[++spot].Merge(self[++spot]);

                that[i] = n16a.Merge(n16b);               
            }
        }

        /// <summary>
        /// Writes the content of a span of <see cref="uint"/>(s) to a span of <see cref="ulong"/>(s).
        /// </summary>
        /// <remarks>
        /// Throws <see cref="ArgumentException"/>:
        /// <list type="bullet">
        /// <item>If the length of <paramref name="self"/> is not a multiple of <c>sizeof(ulong) / sizeof(uint)</c>.</item>
        /// <item>If the length of <paramref name="that"/> is less than <c>self.Length / (sizeof(ulong) / sizeof(uint))</c>.</item>
        /// </list>
        /// </remarks>
        /// <param name="self">The span to split.</param>
        /// <param name="that">The span to write to.</param>
        public static void Merge(this Span<uint> self, Span<ulong> that)
        {
            var quot = sizeof(ulong) / sizeof(uint);

            if (self.Length % quot > 0)
                ThrowHelper.ThrowArgumentException(
                    nameof(self), $"Length must be a multiple of {quot}");

            Guard.HasSizeGreaterThanOrEqualTo(that, self.Length / quot);

            for (int i = 0; i < that.Length; i++)
            {
                var spot = i * quot;

                that[i] = self[spot].Merge(self[++spot]);
            }
        }

#if NET7_0_OR_GREATER

        /// <summary>
        /// Writes the content of a span of <see cref="uint"/>(s) to a span of <see cref="UInt128"/>(s).
        /// </summary>
        /// <remarks>
        /// Throws <see cref="ArgumentException"/>:
        /// <list type="bullet">
        /// <item>If the length of <paramref name="self"/> is not a multiple of <c>16 / sizeof(uint)</c>.</item>
        /// <item>If the length of <paramref name="that"/> is less than <c>self.Length / (16 / sizeof(uint))</c>.</item>
        /// </list>
        /// </remarks>
        /// <param name="self">The span to split.</param>
        /// <param name="that">The span to write to.</param>
        public static void Merge(this Span<uint> self, Span<UInt128> that)
        {
            var quot = 16 / sizeof(uint);

            if (self.Length % quot > 0)
                ThrowHelper.ThrowArgumentException(
                    nameof(self), $"Length must be a multiple of {quot}");

            Guard.HasSizeGreaterThanOrEqualTo(that, self.Length / quot);

            for (int i = 0; i < that.Length; i++)
            {
                var spot = i * quot;

                var n64a = self[spot].Merge(self[++spot]);
                var n64b = self[++spot].Merge(self[++spot]);

                that[i] = n64a.Merge(n64b);
            }
        }

#endif

        /// <summary>
        /// <b>XOR</b>s <paramref name="self"/> with <paramref name="that"/> 
        /// and writes the results in <paramref name="self"/>.
        /// </summary>
        /// <remarks>
        /// Throws <see cref="ArgumentException"/>:
        /// <list type="bullet">
        /// <item>
        /// If <paramref name="self"/> has more elements than <paramref name="that"/>.
        /// </item>
        /// </list>
        /// </remarks>
        /// <param name="self">The span to operate on.</param>
        /// <param name="that">The span to XOR with.</param>
        public static void XOR(this Span<byte> self, Span<byte> that)
        {
            Guard.HasSizeGreaterThanOrEqualTo(that, self.Length);

            for (int i = 0; i < self.Length; i++)
                self[i] = (byte)(self[i] ^ that[i]);
        }
    }
}
