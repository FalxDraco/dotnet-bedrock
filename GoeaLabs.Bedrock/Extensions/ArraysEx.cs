// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable LoopCanBeConvertedToQuery
// ReSharper disable ReturnTypeCanBeEnumerable.Global


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


using System.Security.Cryptography;


namespace GoeaLabs.Bedrock.Extensions
{
    /// <summary>
    /// Extensions for integer arrays.
    /// </summary>
    public static class ArraysEx
    {
        private static readonly RandomNumberGenerator HwRng = RandomNumberGenerator.Create();

        /// <summary>
        /// Fills this array with cryptographically strong unsigned 8 bit integers.
        /// </summary>
        /// <param name="self">The array to operate on.</param>
        /// <returns>A reference to self.</returns>
        public static byte[] FillRandom(this byte[] self)
        {
            HwRng.GetBytes(self);
            return self;
        }

        /// <summary>
        /// Fills this array with cryptographically strong unsigned 32 bit integers.
        /// </summary>
        /// <param name="self">The array to operate on.</param>
        /// <returns>A reference to itself.</returns>
        public static uint[] FillRandom(this uint[] self)
        {
            var size = self.Length * sizeof(uint);
            var buff = new byte[size];

            buff.FillRandom().AsSpan().Merge(self);

            return self;
        }
    }
}
