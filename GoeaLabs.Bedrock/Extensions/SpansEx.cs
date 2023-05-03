// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable LoopCanBeConvertedToQuery


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


using System.Runtime.CompilerServices;
using CommunityToolkit.Diagnostics;


namespace GoeaLabs.Bedrock.Extensions
{

    /// <summary>
    /// Extensions for unsigned integer spans.
    /// </summary>
    [SkipLocalsInit]
    public static class SpansEx
    {
        
        #region Split
        
        /// <summary>
        /// Writes the content of a span of <see cref="uint"/>(s) to a span of <see cref="byte"/>(s).
        /// </summary>
        /// <param name="self">Source span.</param>
        /// <param name="that">Output span.</param>
        /// <exception cref="ArgumentException">
        /// If the length of <paramref name="self"/> is greater than <c>int.MaxValue / sizeof(uint)</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If the length of <paramref name="that"/> is not equal to <c>self.Length * sizeof(uint)</c>.
        /// </exception>
        public static void Split(this Span<uint> self, Span<byte> that)
        {
            const int size = sizeof(uint);
            
            Guard.HasSizeLessThanOrEqualTo(self, int.MaxValue / size);
            Guard.HasSizeEqualTo(that, self.Length * size);

            for (var i = 0; i < self.Length; i++)
            {
                self[i].Halve(out var n16A, out var n16B);
                
                n16A.Halve(out var n8A, out var n8B);
                n16B.Halve(out var n8C, out var n8D);

                var spot = i * size;

                that[spot++] = n8A;
                that[spot++] = n8B;
                that[spot++] = n8C;
                // This warning is blatantly incorrect
                // ReSharper disable once RedundantAssignment
                that[spot++] = n8D;
            }
        }
        
        #endregion
        
        #region Merge
        
        /// <summary>
        /// Writes the content of a span of <see cref="byte"/>(s) to a span of <see cref="uint"/>(s).
        /// </summary>
        /// <param name="self">Source span.</param>
        /// <param name="that">Output span.</param>
        /// <exception cref="ArgumentException">
        /// If the length of <paramref name="self"/> is not a multiple of <c>sizeof(uint)</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If the length of <paramref name="that"/> is not equal to <c>self.Length / sizeof(uint)</c>.
        /// </exception>
        public static void Merge(this Span<byte> self, Span<uint> that)
        {
            const int size = sizeof(uint);
            
            if (self.Length % size > 0)
                ThrowHelper.ThrowArgumentException(
                    nameof(self), $"Length must be a multiple of {size}");
            
            Guard.HasSizeEqualTo(that, self.Length / size);

            for (var i = 0; i < that.Length; i++)
            {
                var spot = i * size;
                var n16A = self[spot].Merge(self[++spot]);
                var n16B = self[++spot].Merge(self[++spot]);

                that[i] = n16A.Merge(n16B);               
            }
        }

        /// <summary>
        /// Writes the content of a span of <see cref="byte"/>(s) to a span of <see cref="ulong"/>(s).
        /// </summary>
        /// <param name="self">Source span.</param>
        /// <param name="that">Output span.</param>
        /// <exception cref="ArgumentException">
        /// If the length of <paramref name="self"/> is not a multiple of <c>sizeof(ulong)</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If the length of <paramref name="that"/> is not equal to <c>self.Length / sizeof(ulong)</c>.
        /// </exception>
        public static void Merge(this Span<byte> self, Span<ulong> that)
        {
            const int size = sizeof(ulong);
            
            if (self.Length % size > 0)
                ThrowHelper.ThrowArgumentException(
                    nameof(self), $"Length must be a multiple of {size}");
            
            Guard.HasSizeEqualTo(that, self.Length / size);

            Span<uint> buff = stackalloc uint[self.Length / sizeof(uint)];
            
            self.Merge(buff);
            buff.Merge(that);
        }

        /// <summary>
        /// Writes the content of a span of <see cref="uint"/>(s) to a span of <see cref="ulong"/>(s).
        /// </summary>
        /// <param name="self">Source span.</param>
        /// <param name="that">Output span.</param>
        /// <exception cref="ArgumentException">
        /// If the length of <paramref name="self"/> is not a multiple of <c>sizeof(ulong) / sizeof(uint)</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If the length of <paramref name="that"/> is not equal to <c>self.Length / (sizeof(ulong) / sizeof(uint))</c>.
        /// </exception>
        public static void Merge(this Span<uint> self, Span<ulong> that)
        {
            const int size = sizeof(ulong) / sizeof(uint);

            if (self.Length % size > 0)
                ThrowHelper.ThrowArgumentException(
                    nameof(self), $"Length must be a multiple of {size}");

            Guard.HasSizeEqualTo(that, self.Length / size);

            for (var i = 0; i < that.Length; i++)
            {
                var spot = i * size;

                that[i] = self[spot].Merge(self[++spot]);
            }
        }

#if NET7_0_OR_GREATER

        /// <summary>
        /// Writes the content of a span of <see cref="uint"/>(s) to a span of <see cref="UInt128"/>(s).
        /// </summary>
        /// <param name="self">Source span.</param>
        /// <param name="that">Output span.</param>
        /// <remarks>
        /// Throws <see cref="ArgumentException"/>:
        /// <list type="bullet">
        /// <item>
        /// If the length of <paramref name="self"/> is not a multiple of <c>sizeof(UInt128) / sizeof(uint)</c>.
        /// </item>
        /// <item>
        /// If the length of <paramref name="that"/> is not equal to <c>self.Length / (sizeof(UInt128) / sizeof(uint))</c>.
        /// </item>
        /// </list>
        /// </remarks>
        public static void Merge(this Span<uint> self, Span<UInt128> that)
        {
            unsafe
            {
               var quot = sizeof(UInt128) / sizeof(uint);

                if (self.Length % quot > 0)
                    ThrowHelper.ThrowArgumentException(
                        nameof(self), $"Length must be a multiple of {quot}");

                Guard.HasSizeEqualTo(that, self.Length / quot);

                for (var i = 0; i < that.Length; i++)
                {
                    var spot = i * quot;
                    var n64A = self[spot].Merge(self[++spot]);
                    var n64B = self[++spot].Merge(self[++spot]);

                    that[i] = n64A.Merge(n64B);
                }
            }
        }

#endif
        
        #endregion

        #region Other
        
        /// <summary>
        /// <b>Xor(s)</b>s <paramref name="self"/> with <paramref name="that"/> and writes the results to
        /// <paramref name="self"/>.
        /// </summary>
        /// <param name="self">The span to operate on.</param>
        /// <param name="that">The span to XOR with.</param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="self"/> and <paramref name="that"/> don't have the same length.
        /// </exception>
        public static void Xor(this Span<byte> self, Span<byte> that)
        {
            Guard.HasSizeEqualTo(that, self.Length);

            for (var i = 0; i < self.Length; i++)
                self[i] = (byte)(self[i] ^ that[i]);
        }

        #endregion
        
    }
}
