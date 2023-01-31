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

using GoeaLabs.Bedrock.Extensions;

namespace GoeaLabs.Bedrock.Tests.Extensions
{
    [TestClass]
    public class ArraysExTests
    {
        #region UInt8 array extensions tests

        [TestMethod]
        [DataRow(1000)]
        public void Byte_array_FillRandom_behaves_correctly(int count) => Assert.IsFalse(new byte[count].FillRandom().IsEmpty());

        [TestMethod]
        [DataRow(
            new byte[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 },
            new byte[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 })]
        public void Byte_array_IsEqual_returns_true_when_arrays_are_equal(byte[] self, byte[] that) => Assert.IsTrue(self.IsEqual(that));

        [TestMethod]
        [DataRow(new byte[] { 10, 11 }, new byte[] { 11, 12 })]
        [DataRow(new byte[] { 0 }, new byte[] { 0, 1 })]
        public void Byte_array_IsEqual_returns_false_when_arrays_are_not_equal(byte[] self, byte[] that) => Assert.IsFalse(self.IsEqual(that));

        [TestMethod]
        [DataRow(new byte[2] { default, default })]
        public void Byte_array_IsEmpty_returns_true_when_array_is_empty(byte[] self) => Assert.IsTrue(self.IsEmpty());

        [TestMethod]
        [DataRow(new byte[] { 0, 1 })]
        public void Byte_array_IsEmpty_returns_false_when_array_is_not_empty(byte[] self) => Assert.IsFalse(self.IsEmpty());

        [TestMethod]
        [DataRow(new byte[] { 0, 1 }, new byte[] { 0, 1, 2 })]
        [ExpectedException(typeof(ArgumentException))]
        public void Byte_array_XOR_throws_ArgumentException_if_self_length_not_greater_than_that_length(byte[] self, byte[] that) => self.XOR(that);


        [TestMethod]
        [DataRow(new byte[] { 1, 2, 3, 4 }, new byte[] { 5, 6, 7, 8 }, new byte[] { 4, 4, 4, 12 })]
        public void Byte_array_XOR_behaves_correctly(byte[] self, byte[] that, byte[] okay) => Assert.IsTrue(self.XOR(that).IsEqual(okay));

        [TestMethod]
        [DataRow(new byte[] { 0, 1, 2 }, 0, 131328U)]
        [DataRow(new byte[] { 0, 1, 2, 3 }, 0, 50462976U)]
        [DataRow(new byte[] { 0, 1, 2, 3, 4, 5 }, 1, 1284U)]
        public void Byte_array_Merge32_beaves_correctly(byte[] input, int index, uint valid) => Assert.IsTrue(input.Merge32()[index] == valid);

        [TestMethod]
        [DataRow(new byte[] { 0, 1, 2, 3, 4, 5, 6 }, 0, 1694364648734976UL)]
        [DataRow(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 0, 506097522914230528UL)]
        [DataRow(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, 1, 185207048UL)]
        public void Byte_array_Merge64_behaves_correctly(byte[] input, int index, ulong valid) => Assert.IsTrue(input.Merge64()[index] == valid);

        #endregion

        #region UInt32 array extensions tests

        [TestMethod]
        [DataRow(new uint[2] { default, default })]
        public void UInt32_array_IsEmpty_returns_true_when_array_is_empty(uint[] self) => Assert.IsTrue(self.IsEmpty());

        [TestMethod]
        [DataRow(new uint[] { 0, 1 })]
        public void UInt32_array_IsEmpty_returns_false_when_array_is_not_empty(uint[] self) => Assert.IsFalse(self.IsEmpty());

        #endregion

    }
}
