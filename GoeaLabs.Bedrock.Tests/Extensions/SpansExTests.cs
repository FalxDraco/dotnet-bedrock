// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo


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
        [DataRow(
            new [] { 0xFFFFFFFF }, 
            new byte[] { 0xFF, 0xFF, 0xFF, 0xFF })]
        [DataRow(
            new uint[] { 0xFFFFFFFF, 0x0000FFFF }, 
            new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF })]
        public void Split_uints_to_bytes_behaves_correctly(uint[] data, byte[] okay)
        {
            Span<byte> test = stackalloc byte[data.Length * sizeof(uint)];
            new Span<uint>(data).Split(test);

            Assert.IsTrue(test.SequenceEqual(okay));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Split_uints_to_bytes_throws_ArgumentException_if_input_too_large()
        {
            var max = int.MaxValue / sizeof(uint);
            var arr = new uint[++max];

            Span<uint> uints = arr;
            Span<byte> bytes = stackalloc byte[1];

            uints.Split(bytes);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Split_uints_to_bytes_throws_ArgumentException_if_output_too_small()
        {
            Span<uint> uints = stackalloc uint[1];
            Span<byte> bytes = stackalloc byte[1];

            uints.Split(bytes);
        }

        [TestMethod]
        [DataRow(
            new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }, 
            new [] { 0xFFFFFFFF })]
        [DataRow(
            new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF }, 
            new uint[] { 0xFFFFFFFF, 0x0000FFFF })]
        public void Merge_bytes_to_uints_behaves_correctly(byte[] data, uint[] okay)
        {
            Span<uint> test = stackalloc uint[data.Length / sizeof(uint)];
            new Span<byte>(data).Merge(test);
            
            Assert.IsTrue(test.SequenceEqual(okay)); 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Merge_bytes_to_uints_throws_ArgumentException_if_incorrect_input_length()
        {
            Span<byte> bytes = stackalloc byte[7];
            Span<uint> uints = stackalloc uint[2];

            bytes.Merge(uints);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Merge_bytes_to_uints_throws_ArgumentException_if_incorrect_output_length()
        {
            Span<byte> bytes = stackalloc byte[8];
            Span<uint> uints = stackalloc uint[1];

            bytes.Merge(uints);
        }

        [TestMethod]
        [DataRow(
            new [] { 0xFFFFFFFF, 0xFFFFFFFF }, 
            new [] { 0xFFFFFFFFFFFFFFFF })]
        [DataRow(
            new [] { 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFE }, 
            new [] { 0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFE })]
        public void Merge_uints_to_ulongs_behaves_correctly(uint[] data, ulong[] okay)
        {
            Span<uint> dataS = data;

            Span<ulong> testS = stackalloc ulong[okay.Length];
            dataS.Merge(testS);

            Assert.IsTrue(testS.SequenceEqual(okay));
        }

        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(3, 1)]
        [ExpectedException(typeof(ArgumentException))]
        public void Merge_uints_to_ulongs_throws_ArgumentException_if_incorrect_input_length(int szSelf, int szThat)
        {
            Span<uint> span32 = stackalloc uint[szSelf];
            Span<ulong> span64 = stackalloc ulong[szThat];

            span32.Merge(span64);
        }

        [TestMethod]
        [DataRow(4, 1)]
        [DataRow(8, 3)]
        [ExpectedException(typeof(ArgumentException))]
        public void Merge_uints_to_ulongs_throws_ArgumentException_if_incorrect_output_length(int szSelf, int szThat)
        {
            Span<uint> span32 = stackalloc uint[szSelf];
            Span<ulong> span64 = stackalloc ulong[szThat];

            span32.Merge(span64);
        }

        [TestMethod]
        [DataRow(new []
        {
            0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF
        })]
        [DataRow(new []
            {
                0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 
                0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFE
            })]
        public void Merge_uints_to_uint128s_behaves_correctly(uint[] data)
        {
            Span<UInt128> test = stackalloc UInt128[data.Length / 4];
            new Span<uint>(data).Merge(test);

            switch (data.Length)
            {
                case 4:
                    Assert.IsTrue(test.SequenceEqual(new [] { UInt128.MaxValue }));
                    break;
                case 8:
                    Assert.IsTrue(test.SequenceEqual(new [] { UInt128.MaxValue, UInt128.MaxValue - 1 }));
                    break;
                default:
                    throw new ArgumentException("Length must be either 4 or 8.", nameof(data));
            }
        }

        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(5, 1)]
        [ExpectedException(typeof(ArgumentException))]
        public void Merge_uints_to_uint128s_throws_ArgumentException_if_incorrect_input_length(int szSelf, int szThat)
        {
            Span<uint> span32 = stackalloc uint[szSelf];
            Span<UInt128> span128 = stackalloc UInt128[szThat];

            span32.Merge(span128);
        }

        [TestMethod]
        [DataRow(8, 1)]
        [DataRow(16, 3)]
        [ExpectedException(typeof(ArgumentException))]
        public void Merge_uints_to_uint128s_throws_ArgumentException_if_incorrect_output_length(int szSelf, int szThat)
        {
            Span<uint> span32 = stackalloc uint[szSelf];
            Span<UInt128> span128 = stackalloc UInt128[szThat];

            span32.Merge(span128);
        }


        [TestMethod]
        [DataRow(new byte[] { 0, 1, 2 }, new byte[] { 0, 1 })]
        [ExpectedException(typeof(ArgumentException))]
        public void UInt8span_XOR_throws_ArgumentException_if_incorrect_lengths(byte[] self, byte[] that) 
            => self.AsSpan().Xor(that);


        [TestMethod]
        [DataRow(new byte[] { 1, 2, 3, 4 }, new byte[] { 5, 6, 7, 8 }, new byte[] { 4, 4, 4, 12 })]
        public void UInt8_span_Xor_behaves_correctly(byte[] self, byte[] that, byte[] okay)
        {
            self.AsSpan().Xor(that);
            Assert.IsTrue(self.IsEqual(okay));
        }
    }
}
