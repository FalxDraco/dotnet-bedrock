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
    public class IntegersExTests
    {

        [TestMethod]
        [DataRow((ushort)0xFFFF, (byte)0xFF, (byte)0xFF)]
        public void Halve_UInt16_behaves_correctly(ushort src, byte ok1, byte ok2)
        {
            src.Halve(out byte left, out byte right);

            Assert.IsTrue(left == ok1 && right == ok2);
        }

        [TestMethod]
        [DataRow(0xFFFFFFFF, (ushort)0xFFFF, (ushort)0xFFFF)]
        public void Halve_UInt32_behaves_correctly(uint src, ushort ok1, ushort ok2)
        {
            src.Halve(out ushort left, out ushort right);

            Assert.IsTrue(left == ok1 && right == ok2);
        }

        [TestMethod]
        [DataRow(0xFFFFFFFFFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF)]
        public void Halve_UInt64_behaves_correctly(ulong src, uint ok1, uint ok2)
        {
            src.Halve(out uint left, out uint right);

            Assert.IsTrue(left == ok1 && right == ok2);
        }

        [TestMethod]
        [DataRow(0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF)]
        public void Halve_UInt128_behaves_correctly(ulong ok1, ulong ok2)
        {
            UInt128.MaxValue.Halve(out ulong left, out ulong right);

            Assert.IsTrue(left == ok1 && right == ok2);
        }

        [TestMethod]
        [DataRow((byte)0xFF, (byte)0xFF, (ushort)0xFFFF)]
        public void Merge_UInt8_behaves_correctly(byte self, byte that, ushort good) => Assert.IsTrue(self.Merge(that) == good);

        [TestMethod]
        [DataRow((ushort)0xFFFF, (ushort)0xFFFF, 0xFFFFFFFF)]
        public void Merge_UInt16_behaves_correctly(ushort self, ushort that, uint good) => Assert.IsTrue(self.Merge(that) == good);

        [TestMethod]
        [DataRow(0xDEADDEAD, 0xC0DEC0DE, 0xDEADDEADC0DEC0DE)]
        public void Merge_UInt32_behaves_correctly(uint self, uint that, ulong good) => Assert.IsTrue(self.Merge(that) == good);

        [TestMethod]
        [DataRow(0xFFFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF)]
        public void Merge_UInt64_behaves_correctly(ulong self, ulong that) => Assert.IsTrue(self.Merge(that) == UInt128.MaxValue);


        [TestMethod]
        [DataRow((byte)0xF, (byte)0x19, (byte)0x16)]
        public void XOR_behaves_correctly(byte self, byte that, byte good) => Assert.IsTrue(self.XOR(that) == good);
    }
}
