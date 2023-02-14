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
        public void UInt8_array_FillRandom_behaves_correctly(int count) => Assert.IsFalse(new byte[count].FillRandom().IsEmpty());

        [TestMethod]
        [DataRow(
            new byte[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 },
            new byte[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 })]
        public void UInt8_array_IsEqual_returns_true_when_arrays_are_equal(byte[] self, byte[] that) => Assert.IsTrue(self.IsEqual(that));

        [TestMethod]
        [DataRow(new byte[] { 10, 11 }, new byte[] { 11, 12 })]
        [DataRow(new byte[] { 0 }, new byte[] { 0, 1 })]
        public void UInt8_array_IsEqual_returns_false_when_arrays_are_not_equal(byte[] self, byte[] that) => Assert.IsFalse(self.IsEqual(that));

        [TestMethod]
        [DataRow(new byte[2] { default, default })]
        public void UInt8_array_IsEmpty_returns_true_when_array_is_empty(byte[] self) => Assert.IsTrue(self.IsEmpty());

        [TestMethod]
        [DataRow(new byte[] { 0, 1 })]
        public void UInt8_array_IsEmpty_returns_false_when_array_is_not_empty(byte[] self) => Assert.IsFalse(self.IsEmpty());

        #endregion

        #region UInt32 array extensions tests

        [TestMethod]
        [DataRow(1000)]
        public void UInt32_array_FillRandom_behaves_correctly(int count) => Assert.IsFalse(new uint[count].FillRandom().IsEmpty());

        [TestMethod]
        [DataRow(new uint[2] { default, default })]
        public void UInt32_array_IsEmpty_returns_true_when_array_is_empty(uint[] self) => Assert.IsTrue(self.IsEmpty());

        [TestMethod]
        [DataRow(new uint[] { 0, 1 })]
        public void UInt32_array_IsEmpty_returns_false_when_array_is_not_empty(uint[] self) => Assert.IsFalse(self.IsEmpty());

        #endregion

    }
}
