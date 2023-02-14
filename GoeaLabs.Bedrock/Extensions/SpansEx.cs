/*
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
        /// Writes the content of the Span to a Span of <see cref="byte"/>(s);
        /// </summary>
        /// <remarks>
        /// Throws <see cref="ArgumentOutOfRangeException"/>:
        /// <list type="bullet">
        /// <item>If the length of <paramref name="self"/> is greater than <c>int.MaxValue / sizeof(uint)</c></item>
        /// <item>If the length of <paramref name="that"/> is less than <c>self.Length * sizeof(uint)</c>.</item>
        /// </list>
        /// </remarks>
        /// <param name="self">The span to split.</param>
        /// <param name="that">The span to write to.</param>
        public static void Split(this Span<uint> self, Span<byte> that)
        {
            var selfMax = int.MaxValue / sizeof(uint);
            Guard.IsLessThanOrEqualTo(self.Length, selfMax);

            var thatMin = self.Length * sizeof(uint);
            Guard.IsGreaterThanOrEqualTo(that.Length, thatMin);

            for (int i = 0; i < self.Length; i++)
            {
                self[i].Halve(out ushort n16a, out ushort n16b);
                
                n16a.Halve(out byte n8a, out byte n8b);
                n16b.Halve(out byte n8c, out byte n8d);

                var pos = i * sizeof(uint);

                that[pos++] = n8a;
                that[pos++] = n8b;
                that[pos++] = n8c;
                that[pos++] = n8d;
            }
        }

        /// <summary>
        /// Writes the content of the Span to a Span of <see cref="uint"/>(s);
        /// </summary>
        /// <remarks>
        /// Throws <see cref="ArgumentOutOfRangeException"/>:
        /// <list type="bullet">
        /// <item>If the length of <paramref name="self"/> is not equal to or multiple of <c>sizeof(uint)</c>.</item>
        /// <item>If the length of <paramref name="that"/> is less than <c>self.Length / sizeof(uint)</c>.</item>
        /// </list>
        /// </remarks>
        /// <param name="self">The span to split.</param>
        /// <param name="that">The span to write to.</param>
        public static void Merge(this Span<byte> self, Span<uint> that)
        {         
            if (self.Length % sizeof(uint) > 0)
                ThrowHelper.ThrowArgumentOutOfRangeException(
                    nameof(self), $"Length must be a multiple of {sizeof(uint)}");

            var thatMin = self.Length / sizeof(uint);
            Guard.IsGreaterThanOrEqualTo(that.Length, thatMin);

            for (int i = 0; i < that.Length; i++)
            {
                var pos = i * sizeof(uint);

                var n16a = self[pos].Merge(self[++pos]);
                var n16b = self[++pos].Merge(self[++pos]);

                that[i] = n16a.Merge(n16b);               
            }
        }

        /// <summary>
        /// <b>XOR</b>s each element of <paramref name="self"/> with the element at the same 
        /// index in <paramref name="that"/> and writes the results in <paramref name="self"/>.
        /// </summary>
        /// <remarks>
        /// Throws <see cref="ArgumentOutOfRangeException"/>:
        /// <list type="bullet">
        /// <item>If <paramref name="self"/> has more elements than <paramref name="that"/>.</item>
        /// </list>
        /// </remarks>
        /// <param name="self">The array to operate on.</param>
        /// <param name="that">The array to XOR with.</param>
        public static void XOR(this Span<byte> self, Span<byte> that)
        {
            Guard.IsGreaterThanOrEqualTo(self.Length, that.Length);

            for (int i = 0; i < self.Length; i++)
                self[i] = (byte)(self[i] ^ that[i]);
        }
    }
}
