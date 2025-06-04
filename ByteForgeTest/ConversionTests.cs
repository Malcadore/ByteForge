using ByteForge;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ByteForgeTest
{
    public struct TestStructure
    {
        public Int32 member1;
        public Int16 member2;
        public Byte member3;
    }
    [TestClass]
    public class ConversionTests
    {
        [TestMethod]
        public void zeros_in_zeros_out()
        {
            var converter = new PackedStructConverter<TestStructure>();
            var test = new TestStructure() { member1 = 0, member2 = 0, member3 = 0 };
            var bytes = converter.Serialize(test, ByteOrder.LittleEndian);
            foreach (byte b in bytes)
            {
                Assert.AreEqual(0, b);
            }
            Assert.AreEqual(7, bytes.Length);
        }

        [TestMethod]
        /// summary> Sets the TestStructure to random numbers, serializes it to a byte array, and 
        /// then deserializes it back to a TestStructure.  The test passes if the original and 
        /// deserialized structures are equal.
        public void automated_random_number_conversion()
        {
            // Probably overkill, but hey, let's have some fun with it.
            var converter = new PackedStructConverter<TestStructure>();
            var random = new Random();
            for (int i = 0; i < 10000; i++)
            {
                var test = new TestStructure()
                {
                    member1 = random.Next(int.MinValue, int.MaxValue),
                    member2 = (short)random.Next(short.MinValue, short.MaxValue),
                    member3 = (byte)random.Next(byte.MinValue, byte.MaxValue)
                };
                var bytes = converter.Serialize(test, ByteOrder.LittleEndian);
                var deserialized = converter.Deserialize(bytes, ByteOrder.LittleEndian);
                Assert.AreEqual(test.member1, deserialized.member1);
                Assert.AreEqual(test.member2, deserialized.member2);
                Assert.AreEqual(test.member3, deserialized.member3);
            }
        }
    }
}
