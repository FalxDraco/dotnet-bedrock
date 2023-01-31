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


using HWRNG = System.Security.Cryptography.RandomNumberGenerator;

namespace GoeaLabs.Bedrock.Extensions
{
    /// <summary>
    /// Extensions for integer arrays.
    /// </summary>
    public static class ArraysEx
    {
        static readonly HWRNG hwRNG = HWRNG.Create();

        /// <summary>
        /// Fills this array with cryptographically strong unsigned 8 bit integers.
        /// </summary>
        /// <param name="self">The array to operate on.</param>
        /// <returns>A reference to self.</returns>
        public static byte[] FillRandom(this byte[] self)
        {
            hwRNG.GetBytes(self);

            return self;
        }

        /// <summary>
        /// Fills this array with cryptographically strong unsigned 32 bit integers.
        /// </summary>
        /// <param name="self">The array to operate on.</param>
        /// <returns>A reference to itself.</returns>
        public static uint[] FillRandom(this uint[] self)
        {
            var buff = new byte[self.Length * sizeof(uint)];

            buff.FillRandom();

            Buffer.BlockCopy(buff, 0, self, 0, buff.Length);

            return self;
        }

        /// <summary>
        /// Performs an element-by-element equality test between this array and another array.
        /// </summary>
        /// <param name="self">Compared array.</param>
        /// <param name="that">Comparand array.</param>
        /// <returns><b>True</b> if all elements are equal, otherwise <b>False</b>.</returns>
        public static bool IsEqual(this byte[] self, byte[] that)
        {
            if (ReferenceEquals(self, that))
                return true;

            if (self.Length != that.Length)
                return false;

            for (int i = 0; i < self.Length; i++)
                if (self[i] != that[i]) return false;

            return true;
        }

        /// <summary>
        /// Performs an element-by-element equality test between this array and another array.
        /// </summary>
        /// <param name="self">Compared array.</param>
        /// <param name="that">Comparand array.</param>
        /// <returns><b>True</b> if all elements are equal, otherwise <b>False</b>.</returns>
        public static bool IsEqual(this uint[] self, uint[] that)
        {
            if (ReferenceEquals(self, that))
                return true;

            if (self.Length != that.Length)
                return false;

            for (int i = 0; i < self.Length; i++)
                if (self[i] != that[i]) return false;

            return true;
        }

        /// <summary>
        /// Checks whether all elements of this array are set to default.
        /// </summary>
        /// <param name="self">The array to operate on.</param>
        /// <returns><b>True</b> if all elements are default, otherwise <b>False</b>.</returns>
        public static bool IsEmpty(this byte[] self)
        {
            foreach (var item in self)
            {
                if (item != default)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Checks whether all elements of this array are set to default.
        /// </summary>
        /// <param name="self">The array to operate on.</param>
        /// <returns><b>True</b> if all elements are default, otherwise <b>False</b>.</returns>
        public static bool IsEmpty(this uint[] self)
        {
            foreach (var item in self)
                if (item != default) return false;

            return true;
        }

        /// <summary>
        /// Merges the elements of this array into 32 bit unsigned integers.
        /// </summary>
        /// <remarks>
        /// Can handle arrays of arbitrary lengths.
        /// </remarks>
        /// <param name="self">The array to operate on.</param>
        /// <returns>A new array of 32 bit unsigned integers.</returns>
        public static uint[] Merge32(this byte[] self)
        {
            int elements = self.Length / sizeof(uint);
            int reminder = self.Length % sizeof(uint);

            if (reminder > 0)
                ++elements;

            var output = new uint[elements];

            Buffer.BlockCopy(self, 0, output, 0, self.Length);

            return output;
        }

        /// <summary>
        /// Merges the elements of this array into 64 bit unsigned integers.
        /// </summary>
        /// <remarks>
        /// Can handle arrays of arbitrary sizes.
        /// </remarks>
        /// <param name="self">The array to operate on.</param>
        /// <returns>A new array of 64 bit unsigned integers.</returns>
        public static ulong[] Merge64(this byte[] self)
        {
            int elements = self.Length / sizeof(ulong);
            int reminder = self.Length % sizeof(ulong);

            if (reminder > 0)
                ++elements;

            var output = new ulong[elements];

            Buffer.BlockCopy(self, 0, output, 0, self.Length);

            return output;
        }

        /// <summary>
        /// Performs an element-by-element <b>XOR</b> between this array and another array.
        /// </summary>
        /// <remarks>
        /// Throws if <b>this array</b> has more elements than the other array.
        /// </remarks>
        /// <param name="self">The array to operate on.</param>
        /// <param name="that">The array to XOR with.</param>
        /// <returns>A reference to itself.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static byte[] XOR(this byte[] self, byte[] that)
        {
            if (self.Length < that.Length)
                throw new ArgumentException(
                    $"Must be minimum {self.Length} length.", nameof(that));

            for (int i = 0; i < self.Length; i++)
                self[i] = (byte)(self[i] ^ that[i]);

            return self;
        }
    }
}
