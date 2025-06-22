// File Name: Conversion.cs
// Project: ByteForge
// GitHub: https://github.com/Malcadore/ByteForge
// © 2025 Scott Pavetti.  Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Buffers.Binary;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace ByteForge {
    /// <summary>
    /// SUPPORT FOR
    ///     SByte, Byte
    ///     Int32, UInt32
    ///     Int16, UInt16
    ///     Int64, UInt64
    /// </summary>
    [DebuggerStepThrough]
    public static class Conversion {
        /// <summary>
        /// Uses BinaryPrimitives .net class to convert the data from the binary
        /// stream in to the declared intrinisc type.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="conversionType"></param>
        /// <param name="offset"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        /// <exception cref="ConversionException"></exception>
        public static Object NConvert(byte[] source, Type conversionType, Int32 offset, ByteOrder order)
        {
            if (source == null) throw new ConversionException("source must be a valid array.");
            // Byte, SByte, and Char will be the same no matter what.
            if (conversionType == typeof(Byte))
            {
                return source[offset];
            }
            if (conversionType == typeof(SByte))
            {
                return (SByte)source[offset];
            }
            if (conversionType == typeof(Char))
            {
                return (Char)source[offset];
            }
            if (conversionType == typeof(Int16))
            {
                return order == ByteOrder.LittleEndian ? 
                    BinaryPrimitives.ReadInt16LittleEndian(new ReadOnlySpan<byte>(source, offset, 2)) :
                    BinaryPrimitives.ReadInt16BigEndian(new ReadOnlySpan<byte>(source, offset, 2));
            }
            if (conversionType == typeof(UInt16))
            {
                return order == ByteOrder.LittleEndian ?
                    BinaryPrimitives.ReadUInt16LittleEndian(new ReadOnlySpan<byte>(source, offset, 2)) :
                    BinaryPrimitives.ReadUInt16BigEndian(new ReadOnlySpan<byte>(source, offset, 2));
            }
            if (conversionType == typeof(Int32))
            {
                return order == ByteOrder.LittleEndian ?
                    BinaryPrimitives.ReadInt32LittleEndian(new ReadOnlySpan<byte>(source, offset, 4)) :
                    BinaryPrimitives.ReadInt32BigEndian(new ReadOnlySpan<byte>(source, offset, 4));
            }
            if (conversionType == typeof(UInt32))
            {
                return order == ByteOrder.LittleEndian ?
                    BinaryPrimitives.ReadUInt32LittleEndian(new ReadOnlySpan<byte>(source, offset, 4)) :
                    BinaryPrimitives.ReadUInt32BigEndian(new ReadOnlySpan<byte>(source, offset, 4));
            }
            if (conversionType == typeof(Int64))
            {
                return order == ByteOrder.LittleEndian ?
                    BinaryPrimitives.ReadInt64LittleEndian(new ReadOnlySpan<byte>(source, offset, 8)) :
                    BinaryPrimitives.ReadInt64BigEndian(new ReadOnlySpan<byte>(source, offset, 8));
            }
            if (conversionType == typeof(UInt64))
            {
                return order == ByteOrder.LittleEndian ?
                    BinaryPrimitives.ReadUInt64LittleEndian(new ReadOnlySpan<byte>(source, offset, 8)) :
                    BinaryPrimitives.ReadUInt64BigEndian(new ReadOnlySpan<byte>(source, offset, 8));
            }
            if (conversionType == typeof(Single))
            {
                return order == ByteOrder.LittleEndian ?
                    BinaryPrimitives.ReadSingleLittleEndian(new ReadOnlySpan<byte>(source, offset, 4)) :
                    BinaryPrimitives.ReadSingleBigEndian(new ReadOnlySpan<byte>(source, offset, 4));
            }
            if (conversionType == typeof(Double))
            {
                return order == ByteOrder.LittleEndian ?
                    BinaryPrimitives.ReadDoubleLittleEndian(new ReadOnlySpan<byte>(source, offset, 4)) :
                    BinaryPrimitives.ReadDoubleBigEndian(new ReadOnlySpan<byte>(source, offset, 4));
            }
            throw new ConversionException(String.Format(CultureInfo.InvariantCulture, "unknown type passed for conversion: {0}", conversionType));
        }

        /// <summary>
        /// Converts a byte array representation to the desired type representation.  For example,
        /// a byte array that has to convert to a UInt32 will take four bytes and do 
        /// conversion to the intrinsic type byte order.
        /// FConvert is short for Format Convert. 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="conversionType"></param>
        /// <param name="offset"></param>
        /// <param name="conversionOrder"></param>
        /// <returns></returns>
        public static Object FConvert(byte[] source, Type conversionType, Int32 offset, ByteOrder conversionOrder)
        {
            if (source == null) throw new ConversionException("source must be a valid array.");

            if (conversionType == typeof(Byte))
            {
                return source[offset];
            }
            if (conversionType == typeof(SByte))
            {
                return (SByte)source[offset];
            }
            if (conversionType == typeof(Char))
            {
                return (Char)source[offset];
            }
            if (conversionType == typeof(Int16))
            {
                return BinaryConverter.ToInt16(source, offset, conversionOrder);
            }
            if (conversionType == typeof(UInt16))
            {
                return BinaryConverter.ToUInt16(source, offset, conversionOrder);
            }
            if (conversionType == typeof(Int32))
            {
                return BinaryConverter.ToInt32(source, offset, conversionOrder);
            }
            if (conversionType == typeof(UInt32))
            {
                return BinaryConverter.ToUInt32(source, offset, conversionOrder);
            }
            if (conversionType == typeof(Int64))
            {
                return BinaryConverter.ToInt64(source, offset, conversionOrder);
            }
            if (conversionType == typeof(UInt64))
            {
                return BinaryConverter.ToUInt64(source, offset, conversionOrder);
            }
            if (conversionType == typeof(Single))
            {
                return BinaryConverter.ToFloat(source, offset, conversionOrder);
            }
            if (conversionType == typeof(Double))
            {
                return BinaryConverter.ToDouble(source, offset, conversionOrder);
            }
            throw new ConversionException(String.Format(CultureInfo.InvariantCulture, "unknown type passed for conversion: {0}", conversionType));
        }

        /// <summary>
        /// Helper method that does what default(T) does.
        /// </summary>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public static object Default(Type fieldType) {
            if (fieldType == null) return null;
            return fieldType.IsValueType ? (object)0 : null;
        }

        /// <summary>
        /// Takes a boxed object, unboxes it, converts it to an array representation, then
        /// stuffs it into a destination array.
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        /// <param name="conversionOrder"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        public static void AddToByteArray(byte[] destination, int offset, object value, ByteOrder conversionOrder) {
            if (value is Int32) {
                var temp = (Int32)value;
                AddToByteArray(destination, offset, temp, conversionOrder);
            } else if (value is UInt32) {
                var temp = (UInt32)value;
                AddToByteArray(destination, offset, temp, conversionOrder);
            } else if (value is Byte) {
                var temp = (Byte)value;
                AddToByteArray(destination, offset, temp, conversionOrder);
            } else if (value is Char) {
                var temp = (Char)value;
                AddToByteArray(destination, offset, temp, conversionOrder);
            } else if (value is SByte) {
                var temp = (SByte)value;
                AddToByteArray(destination, offset, temp, conversionOrder);
            } else if (value is Int64) {
                var temp = (Int64)value;
                AddToByteArray(destination, offset, temp, conversionOrder);
            } else if (value is UInt64) {
                var temp = (UInt64)value;
                AddToByteArray(destination, offset, temp, conversionOrder);
            } else if (value is Int16) {
                var temp = (Int16)value;
                AddToByteArray(destination, offset, temp, conversionOrder);
            } else if (value is UInt16) {
                var temp = (UInt16)value;
                AddToByteArray(destination, offset, temp, conversionOrder);
            } else if (value is Single) {
                var temp = (Single)value;
                AddToByteArray(destination, offset, temp, conversionOrder);
            } else if (value is Double) { 
                var temp = (Double)value;
                AddToByteArray(destination, offset, temp, conversionOrder);
            } else if (value is Int128) {
                throw new ConversionException("No support for Int128.");
            } else if (value is UInt128) {
                throw new ConversionException("No support for UInt128.");
            }
        }

        #region Array Stuffing
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "conversionOrder")]
        private static void AddToByteArray(byte[] destination, int offset, char value, ByteOrder conversionOrder) {
            // byte order is irrelevant when converting a single byte.
            Buffer.SetByte(destination, offset, (byte)value);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "conversionOrder")]
        private static void AddToByteArray(byte[] destination, int offset, byte value, ByteOrder conversionOrder) {
            // byte order is irrelevant when converting a single byte.
            Buffer.SetByte(destination, offset, value);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "conversionOrder")]
        private static void AddToByteArray(byte[] destination, int offset, sbyte value, ByteOrder conversionOrder) {
            // byte order is irrelevant when converting a single byte.
            Buffer.SetByte(destination, offset, (byte)value);
        }

        private static void AddToByteArray(byte[] destination, int offset, long value, ByteOrder conversionOrder) {
            InsertBytes(destination, offset, BinaryConverter.GetBytes(value, conversionOrder));
        }
       
        private static void AddToByteArray(byte[] destination, int offset, ulong value, ByteOrder conversionOrder) {
            InsertBytes(destination, offset, BinaryConverter.GetBytes(value, conversionOrder));
        }
      
        private static void AddToByteArray(byte[] destination, int offset, int value, ByteOrder conversionOrder) {
            InsertBytes(destination, offset, BinaryConverter.GetBytes(value, conversionOrder));
        }
      
        private static void AddToByteArray(byte[] destination, int offset, uint value, ByteOrder conversionOrder) {
            InsertBytes(destination, offset, BinaryConverter.GetBytes(value, conversionOrder));
        }
      
        private static void AddToByteArray(byte[] destination, int offset, short value, ByteOrder conversionOrder) {
            InsertBytes(destination, offset, BinaryConverter.GetBytes(value, conversionOrder));
        }
      
        private static void AddToByteArray(byte[] destination, int offset, ushort value, ByteOrder conversionOrder) {
            InsertBytes(destination, offset, BinaryConverter.GetBytes(value, conversionOrder));
        }

        private static void AddToByteArray(byte[] destination, int offset, Int128 value, ByteOrder conversionOrder)
        {
            InsertBytes(destination, offset, BinaryConverter.GetBytes(value, conversionOrder));
        }
        private static void InsertBytes(byte[] destination, int offset, byte[] bytes) {
            Buffer.BlockCopy(bytes, 0, destination, offset, bytes.Length);
        }
        #endregion
    }
}
