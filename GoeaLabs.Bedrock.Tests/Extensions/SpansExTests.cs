using GoeaLabs.Bedrock.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [ExpectedException(typeof(ArgumentException))]
        public void Merge_bytes_to_uints_throws_ArgumentException_if_input_not_multiple_of_sizeof_uint()
        {
            Span<byte> bytes = stackalloc byte[7];
            Span<uint> uints = stackalloc uint[2];

            bytes.Merge(uints);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Merge_bytes_to_uints_throws_ArgumentException_if_output_too_small()
        {
            Span<byte> bytes = stackalloc byte[8];
            Span<uint> uints = stackalloc uint[1];

            bytes.Merge(uints);
        }
    }
}
