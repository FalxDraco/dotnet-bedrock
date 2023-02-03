using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoeaLabs.Bedrock.Extensions
{
    /// <summary>
    /// Extensions for integer spans.
    /// </summary>
    public static class SpansEx
    {

        /// <summary>
        /// Writes the content of the Span to a Span of <see cref="byte"/>(s);
        /// </summary>
        /// <remarks>
        /// Throws <see cref="ArgumentException"/>:
        /// <list type="bullet">
        /// <item>If the length of <paramref name="self"/> is greater than <c>int.MaxValue / sizeof(uint)</c></item>
        /// <item>If the length of <paramref name="that"/> is less than <c>self.Length * sizeof(uint)</c>.</item>
        /// </list>
        /// </remarks>
        /// <param name="self">The span to split.</param>
        /// <param name="that">The span to write to.</param>
        public static void Split(this Span<uint> self, Span<byte> that)
        {
            var selfMax = int.MaxValue / sizeof(uint);

            if (self.Length > selfMax)
                throw new ArgumentException($"Length must be maximum {selfMax}", nameof(self));

            var thatMin = self.Length * sizeof(uint);

            if (that.Length < thatMin)
                throw new ArgumentException($"Length must be minimum {thatMin}", nameof(that));

            for (int i = 0; i < self.Length; i++)
            {
                self[i].Halve(out ushort n16a, out ushort n16b);
                
                n16a.Halve(out byte n8a, out byte n8b);
                n16b.Halve(out byte n8c, out byte n8d);

                var pos = i * sizeof(uint);

                that[pos++] = n8a;
                that[pos++] = n8b;
                that[pos++] = n8c;
                that[pos++] = n8d;
            }
        }

        /// <summary>
        /// Writes the content of the Span to a Span of <see cref="uint"/>(s);
        /// </summary>
        /// <remarks>
        /// Throws <see cref="ArgumentException"/>:
        /// <list type="bullet">
        /// <item>If the length of <paramref name="self"/> is not equal to or multiple of <c>sizeof(uint)</c>.</item>
        /// <item>If the length of <paramref name="that"/> is less than <c>self.Length / sizeof(uint)</c>.</item>
        /// </list>
        /// </remarks>
        /// <param name="self">The span to split.</param>
        /// <param name="that">The span to write to.</param>
        public static void Merge(this Span<byte> self, Span<uint> that)
        {
            if (self.Length % sizeof(uint) > 0)
                throw new ArgumentException($"Length must be a multiple of {sizeof(uint)}", nameof(self));

            var thatMin = self.Length / sizeof(uint);

            if (that.Length < thatMin)
                throw new ArgumentException($"Length must be minimum {thatMin}", nameof(that));

            for (int i = 0; i < that.Length; i++)
            {
                var pos = i * sizeof(uint);

                var n16a = self[pos].Merge(self[++pos]);
                var n16b = self[++pos].Merge(self[++pos]);

                that[i] = n16a.Merge(n16b);               
            }
        }
    }
}
