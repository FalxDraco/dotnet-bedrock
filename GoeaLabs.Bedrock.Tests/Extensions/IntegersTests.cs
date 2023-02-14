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


using System.Numerics;

namespace GoeaLabs.Bedrock.Tests.Extensions
{
    [TestClass]
    public class IntegersTests
    {
        [TestMethod]
        [DataRow(ulong.MaxValue, ulong.MinValue, ulong.MaxValue, 10UL, 20UL)]
        public void ScaleUnsigned64_behaves_correctly(ulong srcN, ulong minN, ulong maxN, ulong minR, ulong maxR)
        {
            var scaled = Integers.ScaleUnsigned64(srcN, minN, maxN, minR, maxR);

            Assert.IsTrue(scaled >= minR && scaled <= maxR);
        }

        [TestMethod]
        [DataRow(-1000000L, int.MinValue, int.MaxValue, -20L, 10L)]
        public void ScaleSigned64_behaves_correctly(long srcN, long minN, long maxN, long minR, long maxR)
        {
            var scaled = Integers.ScaleSigned64(srcN, minN, maxN, minR, maxR);

            Assert.IsTrue(scaled >= minR && scaled <= maxR);
        }


        [TestMethod]
        [DataRow(10U, 20U)]
        public void ScaleUnsigned128_behaves_correctly(uint min, uint max)
        {
            UInt128 srcN = UInt128.MaxValue;
            UInt128 minN = UInt128.MinValue;
            UInt128 maxN = UInt128.MaxValue;
            UInt128 minR = min;
            UInt128 maxR = max;

            var scaled = Integers.ScaleUnsigned128(srcN, minN, maxN, minR, maxR);

            Assert.IsTrue(scaled >= minR && scaled <= maxR);
        }

        [TestMethod]
        [DataRow(-10, 20)]
        [DataRow(-20, 10)]
        public void ScaleSigned128_behaves_correctly(int min, int max)
        {
            Int128 srcN = Int128.MaxValue;
            Int128 minN = Int128.MinValue;
            Int128 maxN = Int128.MaxValue;
            Int128 minR = min;
            Int128 maxR = max;

            var scaled = Integers.ScaleSigned128(srcN, minN, maxN, minR, maxR);

            Assert.IsTrue(scaled >= minR && scaled <= maxR);
        }


        [TestMethod]
        [DataRow(10, 20)]
        public void ScaleBigInt_behaves_correctly_for_unsigned_128bit(int min, int max)
        {
            BigInteger srcN = Integers.BigUInt128Max;
            BigInteger minN = Integers.BigUInt128Min;
            BigInteger maxN = Integers.BigUInt128Max;
            BigInteger minR = min;
            BigInteger maxR = max;

            var scaled = Integers.ScaleBigInt(srcN, minN, maxN, minR, maxR);

            Assert.IsTrue(scaled >= minR && scaled <= maxR);
        }

        [TestMethod]
        [DataRow(-10, 20)]
        [DataRow(-20, 10)]
        public void ScaleBigInt_behaves_correctly_for_signed_128bit(int min, int max)
        {
            BigInteger srcN = Integers.BigInt128Max;
            BigInteger minN = Integers.BigInt128Min;
            BigInteger maxN = Integers.BigInt128Max;
            BigInteger minR = min;
            BigInteger maxR = max;

            var scaled = Integers.ScaleBigInt(srcN, minN, maxN, minR, maxR);

            Assert.IsTrue(scaled >= minR && scaled <= maxR);
        }

        [TestMethod]
        [DataRow(10, 20)]
        public void ScaleBigInt_behaves_correctly_for_unsigned_256bit(int min, int max)
        {
            BigInteger srcN = Integers.BigUInt256Max;
            BigInteger minN = Integers.BigUInt256Min;
            BigInteger maxN = Integers.BigUInt256Max;
            BigInteger minR = min;
            BigInteger maxR = max;

            var scaled = Integers.ScaleBigInt(srcN, minN, maxN, minR, maxR);

            Assert.IsTrue(scaled >= minR && scaled <= maxR);
        }

        [TestMethod]
        [DataRow(-10, 20)]
        [DataRow(-20, 10)]
        public void ScaleBigInt_behaves_correctly_for_signed_256bit(int min, int max)
        {
            BigInteger srcN = Integers.BigInt256Max;
            BigInteger minN = Integers.BigInt256Min;
            BigInteger maxN = Integers.BigInt256Max;
            BigInteger minR = min;
            BigInteger maxR = max;

            var scaled = Integers.ScaleBigInt(srcN, minN, maxN, minR, maxR);

            Assert.IsTrue(scaled >= minR && scaled <= maxR);
        }

        [TestMethod]
        [DataRow(10, 20)]
        public void ScaleBigInt_behaves_correctly_for_unsigned_512bit(int min, int max)
        {
            BigInteger srcN = Integers.BigUInt512Max;
            BigInteger minN = Integers.BigUInt512Min;
            BigInteger maxN = Integers.BigUInt512Max;
            BigInteger minR = min;
            BigInteger maxR = max;

            var scaled = Integers.ScaleBigInt(srcN, minN, maxN, minR, maxR);

            Assert.IsTrue(scaled >= minR && scaled <= maxR);
        }

        [TestMethod]
        [DataRow(10, 20)]
        [DataRow(-20, 10)]
        public void ScaleBigInt_behaves_correctly_for_signed_512bit(int min, int max)
        {
            BigInteger srcN = Integers.BigInt512Max;
            BigInteger minN = Integers.BigInt512Min;
            BigInteger maxN = Integers.BigInt512Max;
            BigInteger minR = min;
            BigInteger maxR = max;

            var scaled = Integers.ScaleBigInt(srcN, minN, maxN, minR, maxR);

            Assert.IsTrue(scaled >= minR && scaled <= maxR);
        }
    }
}
