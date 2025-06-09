using ByteForge;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ByteForgeTest;

[TestClass]
public class FloatingPointTests

{
    [TestMethod]
    public void Test_Float_Conversion_Bytes_To_Type_LE()
    {
        Single input = 2.25F;  //Arbitrarily chosen.
        var array = BitConverter.GetBytes(input);
        var result = BinaryConverter.ToFloat(array, 0, ByteOrder.LittleEndian);
        Assert.AreEqual(input, result);
    }

    [TestMethod]
    public void Test_Double_Conversion_Bytes_To_Type_LE()
    {
        Double input = 2.25D;  //Arbitrarily chosen.
        var array = BitConverter.GetBytes(input);
        var result = BinaryConverter.ToDouble(array, 0, ByteOrder.LittleEndian);
        Assert.AreEqual(input, result);
    }
}
