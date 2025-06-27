// File Name: Conversion.cs
// Project: ByteForge
// GitHub: https://github.com/Malcadore/ByteForge
// © 2025 Scott Pavetti.  Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace ByteForge
{
    /// <summary>
    /// SUPPORT FOR
    ///     SByte, Byte, Char
    ///     Int32, UInt32
    ///     Int16, UInt16
    ///     Int64, UInt64
    ///     Int128, UInt128
    ///     Single, Double
    /// </summary>
    //[DebuggerStepThrough]
    public static class Conversion
    {
        /// <summary>
        /// Uses BinaryPrimitives .net class to convert the data from the binary
        /// stream in to the declared intrinisc type.  The code is a bit dense right now, and
        /// it should be refactored to be more readable.  
        /// </summary>
        /// <param name="source"></param>
        /// <param name="conversionType"></param>
        /// <param name="offset"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        /// <exception cref="ConversionException"></exception>
        public static Object NConvert(byte[] source, Type conversionType, Int32 offset, ByteOrder order) => conversionType switch
        {
            Type t when t == typeof(Byte) => source[offset],
            Type t when t == typeof(SByte) => (SByte)source[offset],
            Type t when t == typeof(Char) => (Char)source[offset],
            Type t when t == typeof(Int16) => order == ByteOrder.LittleEndian ?
                        BinaryPrimitives.ReadInt16LittleEndian(new ReadOnlySpan<byte>(source, offset, 2)) :
                        BinaryPrimitives.ReadInt16BigEndian(new ReadOnlySpan<byte>(source, offset, 2)),
            Type t when t == typeof(UInt16) => order == ByteOrder.LittleEndian ?
                        BinaryPrimitives.ReadUInt16LittleEndian(new ReadOnlySpan<byte>(source, offset, 2)) :
                        BinaryPrimitives.ReadUInt16BigEndian(new ReadOnlySpan<byte>(source, offset, 2)),
            Type t when t == typeof(Int32) => order == ByteOrder.LittleEndian ?
                        BinaryPrimitives.ReadInt32LittleEndian(new ReadOnlySpan<byte>(source, offset, 4)) :
                        BinaryPrimitives.ReadInt32BigEndian(new ReadOnlySpan<byte>(source, offset, 4)),
            Type t when t == typeof(UInt32) => order == ByteOrder.LittleEndian ?
                        BinaryPrimitives.ReadUInt32LittleEndian(new ReadOnlySpan<byte>(source, offset, 4)) :
                        BinaryPrimitives.ReadUInt32BigEndian(new ReadOnlySpan<byte>(source, offset, 4)),
            Type t when t == typeof(Int64) => order == ByteOrder.LittleEndian ?
                        BinaryPrimitives.ReadInt64LittleEndian(new ReadOnlySpan<byte>(source, offset, 8)) :
                        BinaryPrimitives.ReadInt64BigEndian(new ReadOnlySpan<byte>(source, offset, 8)),
            Type t when t == typeof(UInt64) => order == ByteOrder.LittleEndian ?
                        BinaryPrimitives.ReadUInt64LittleEndian(new ReadOnlySpan<byte>(source, offset, 8)) :
                        BinaryPrimitives.ReadUInt64BigEndian(new ReadOnlySpan<byte>(source, offset, 8)),
            Type t when t == typeof(Single) => order == ByteOrder.LittleEndian ?
                        BinaryPrimitives.ReadSingleLittleEndian(new ReadOnlySpan<byte>(source, offset, 4)) :
                        BinaryPrimitives.ReadSingleBigEndian(new ReadOnlySpan<byte>(source, offset, 4)),
            Type t when t == typeof(Double) => order == ByteOrder.LittleEndian ?
                        BinaryPrimitives.ReadDoubleLittleEndian(new ReadOnlySpan<byte>(source, offset, 8)) :
                        BinaryPrimitives.ReadDoubleBigEndian(new ReadOnlySpan<byte>(source, offset, 8)),
            Type t when t == typeof(Int128) => order == ByteOrder.LittleEndian ?
                        BinaryPrimitives.ReadInt128LittleEndian(new ReadOnlySpan<byte>(source, offset, 16)) :
                        BinaryPrimitives.ReadInt128BigEndian(new ReadOnlySpan<byte>(source, offset, 16)),
            Type t when t == typeof(UInt128) => order == ByteOrder.LittleEndian ?
                        BinaryPrimitives.ReadUInt128LittleEndian(new ReadOnlySpan<byte>(source, offset, 16)) :
                        BinaryPrimitives.ReadUInt128BigEndian(new ReadOnlySpan<byte>(source, offset, 16)),
            _ => throw new ConversionException(String.Format(CultureInfo.InvariantCulture, "unknown type passed for conversion: {0}", conversionType)),
        };

        /// <summary>
        /// Helper method that does what default(T) does.
        /// </summary>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public static object Default(Type fieldType)
        {
            if (fieldType == null) return null;
            return fieldType.IsValueType ? (object)0 : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        /// <param name="conversionOrder"></param>
        public static void AddToByteArray(byte[] destination, int offset, object value, ByteOrder conversionOrder)
        {
            switch (value)
            {
                case Byte: Buffer.SetByte(destination, offset, (byte)value); break;
                case SByte: Buffer.SetByte(destination, offset, (byte)value); break;
                case Char: Buffer.SetByte(destination, offset, (byte)value); break;
                case Int16: InsertBytes(destination, offset, BinaryConverter.GetBytes((short)value, conversionOrder)); break;
                case UInt16: InsertBytes(destination, offset, BinaryConverter.GetBytes((ushort)value, conversionOrder)); break;
                case Int32: InsertBytes(destination, offset, BinaryConverter.GetBytes((int)value, conversionOrder)); break;
                case UInt32: InsertBytes(destination, offset, BinaryConverter.GetBytes((uint)value, conversionOrder)); break;
                case Int64: InsertBytes(destination, offset, BinaryConverter.GetBytes((long)value, conversionOrder)); break;
                case UInt64: InsertBytes(destination, offset, BinaryConverter.GetBytes((ulong)value, conversionOrder)); break;
                case Single: InsertBytes(destination, offset, BitConverter.GetBytes((float)value)); break;
                case Double: InsertBytes(destination, offset, BitConverter.GetBytes((double)value)); break;
                case Int128: InsertBytes(destination, offset, BinaryConverter.GetBytes((Int128)value, conversionOrder)); break;
                case UInt128: InsertBytes(destination, offset, BinaryConverter.GetBytes((UInt128)value, conversionOrder)); break;
                default: break;
            }
        }

        /// <summary>
        /// A helper method that inserts the bytes into the destination array at the specified offset.
        /// This is a little help with calculating the length of the bytes array in the same line as 
        /// getting the array itself.  It is hoped that inlining this method will help, but it is not
        /// tested as of 6/27/2025.
        /// TODO: Test this method to see if it is inlined as expected, and has performance benefits.
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="offset"></param>
        /// <param name="bytes"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InsertBytes(byte[] destination, int offset, byte[] bytes)
        {
            Buffer.BlockCopy(bytes, 0, destination, offset, bytes.Length);
        }
    }
}
