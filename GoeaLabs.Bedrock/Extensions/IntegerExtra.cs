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

namespace GoeaLabs.Bedrock.Extensions
{
    /// <summary>
    /// Integer extensions.
    /// </summary>
    public static class IntegerExtra
    {
        /// <summary>
        /// Halves a 16 bit unsigned integer.
        /// </summary>
        /// <param name="self">Source integer.</param>
        /// <param name="left">Left half.</param>
        /// <param name="right">Right half.</param>
        public static void Halve(this ushort self, out byte left, out byte right)
        {
            left = (byte)(self >> 8);
            right = (byte)self;
        }

        /// <summary>
        /// Halves a 32 bit unsigned integer.
        /// </summary>
        /// <param name="self">Source integer.</param>
        /// <param name="left">Left half.</param>
        /// <param name="right">Right half.</param>
        public static void Halve(this uint self, out ushort left, out ushort right)
        {
            left = (ushort)(self >> 16);
            right = (ushort)self;
        }

        /// <summary>
        /// Halves a 64 bit unsigned integer.
        /// </summary>
        /// <param name="self">Source integer.</param>
        /// <param name="left">Left half.</param>
        /// <param name="right">Right half.</param>
        public static void Halve(this ulong self, out uint left, out uint right)
        {
            left = (uint)(self >> 32);
            right = (uint)self;
        }

#if NET7_0_OR_GREATER

        /// <summary>
        /// Halves a 128 bit unsigned integer.
        /// </summary>
        /// <param name="self">Self.</param>
        /// <param name="left">Left half.</param>
        /// <param name="right">Right half.</param>
        public static void Halve(this UInt128 self, out ulong left, out ulong right)
        {
            left = (ulong)(self >> 64);
            right = (ulong)self;
        }

#endif

        /// <summary>
        /// Merges two 8 bit unsigned integers.
        /// </summary>
        /// <param name="self">Left half.</param>
        /// <param name="that">Right half.</param>
        /// <returns>A new 16 bit unsigned integer.</returns>
        public static ushort Merge(this byte self, byte that) => (ushort)(self << 8 | that);

        /// <summary>
        /// Merges two 16 bit unsigned integers.
        /// </summary>
        /// <param name="self">Left half.</param>
        /// <param name="that">Right half.</param>
        /// <returns>A 32 bit unsigned integer.</returns>
        public static uint Merge(this ushort self, ushort that) => (uint)self << 16 | that;

        /// <summary>
        /// Merges two 32 bit unsigned integers.
        /// </summary>
        /// <param name="self">Left half.</param>
        /// <param name="that">Right half.</param>
        /// <returns>A new 64 bit unsigned integer.</returns>
        public static ulong Merge(this uint self, uint that) => (ulong)self << 32 | that;

#if NET7_0_OR_GREATER

        /// <summary>
        /// Merges two 64 bit unsigned integers.
        /// </summary>
        /// <param name="self">Left half.</param>
        /// <param name="that">Right half.</param>
        /// <returns>A new 128 bit unsigned integer.</returns>
        public static UInt128 Merge(this ulong self, ulong that) => (UInt128)self << 64 | that;

#endif

        /// <summary>
        /// XORs two bytes.
        /// </summary>
        /// <param name="self">This byte.</param>
        /// <param name="that">The byte to XOR with.</param>
        /// <returns>The result of the XOR operation.</returns>
        public static byte Xor(this byte self, byte that) => (byte)(self ^ that);
    }
}
