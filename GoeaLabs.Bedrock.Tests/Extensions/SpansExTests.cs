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
    public class SpansExTests
    {

        [TestMethod]
        [DataRow(new uint[] { 0xFFFFFFFF }, new byte[] { 0xFF, 0xFF, 0xFF, 0xFF })]
        [DataRow(new uint[] { 0xFFFFFFFF, 0x0000FFFF }, new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF })]
        public void Split_uints_to_bytes_behaves_correctly(uint[] data, byte[] okay)
        {
            Span<uint> dataS = data;
            Span<byte> okayS = okay;

            Span<byte> testS = stackalloc byte[dataS.Length * sizeof(uint)];
            dataS.Split(testS);

            Assert.IsTrue(testS.ToArray().IsEqual(okay));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Split_uints_to_bytes_throws_ArgumentException_if_input_too_large()
        {
            var max = int.MaxValue / sizeof(uint);
            var arr = new uint[++max];

            Span<uint> uints = arr;
            Span<byte> bytes = stackalloc byte[1];

            uints.Split(bytes);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Split_uints_to_bytes_throws_ArgumentException_if_output_too_small()
        {
            Span<uint> uints = stackalloc uint[1];
            Span<byte> bytes = stackalloc byte[1];

            uints.Split(bytes);
        }

        [TestMethod]
        [DataRow(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }, new uint[] { 0xFFFFFFFF })]
        [DataRow(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF }, new uint[] { 0xFFFFFFFF, 0x0000FFFF })]
        public void Merge_bytes_to_uints_behaves_correctly(byte[] data, uint[] okay)
        {
            Span<byte> dataS = data;
            Span<uint> okayS = okay;

            Span<uint> testS = stackalloc uint[dataS.Length / sizeof(uint)];
            dataS.Merge(testS);

            Assert.IsTrue(testS.ToArray().IsEqual(okay));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Merge_bytes_to_uints_throws_ArgumentOutOfRangeException_if_input_not_multiple_of_sizeof_uint()
        {
            Span<byte> bytes = stackalloc byte[7];
            Span<uint> uints = stackalloc uint[2];

            bytes.Merge(uints);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Merge_bytes_to_uints_throws_ArgumentOutOfRangeException_if_output_too_small()
        {
            Span<byte> bytes = stackalloc byte[8];
            Span<uint> uints = stackalloc uint[1];

            bytes.Merge(uints);
        }

        [TestMethod]
        [DataRow(new byte[] { 0, 1 }, new byte[] { 0, 1, 2 })]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UInt8span_XOR_throws_ArgumentOutOfRangeException_if_self_length_not_greater_than_that_length(byte[] self, byte[] that) 
            => self.AsSpan().XOR(that);


        [TestMethod]
        [DataRow(new byte[] { 1, 2, 3, 4 }, new byte[] { 5, 6, 7, 8 }, new byte[] { 4, 4, 4, 12 })]
        public void UInt8_span_XOR_behaves_correctly(byte[] self, byte[] that, byte[] okay)
        {
            self.AsSpan().XOR(that);
            Assert.IsTrue(self.IsEqual(okay));
        }
    }
}
