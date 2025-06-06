// File Name: Conversion.cs
// Project: ByteForge
// GitHub: https://github.com/Malcadore/ByteForge
// © 2025 Scott Pavetti.  Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics;
using System.Globalization;

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
        /// Converts a byte array representation to the desired type representation.  For example,
        /// a byte array that has to convert to a UInt32 will take four bytes and do the desired
        /// conversion based on the desired byte order.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="conversionType"></param>
        /// <param name="offset"></param>
        /// <param name="conversionOrder"></param>
        /// <returns></returns>
        public static Object FConvert(byte[] source, Type conversionType, Int32 offset, ByteOrder conversionOrder) {
            if (source == null) throw new ConversionException("source must be a valid array.");

            if (conversionType == typeof (Byte)) {
                return source[offset];
            }
            if (conversionType == typeof (SByte)) {
                return (SByte) source[offset];
            }
            if (conversionType == typeof (Char)) {
                return (Char) source[offset];
            }
            if (conversionType == typeof (Int16)) {
                return BinaryConverter.ToInt16(source, offset, conversionOrder);
            }
            if (conversionType == typeof (UInt16)) {
                return BinaryConverter.ToUInt16(source, offset, conversionOrder);
            }
            if (conversionType == typeof (Int32)) {
                return BinaryConverter.ToInt32(source, offset, conversionOrder);
            }
            if (conversionType == typeof (UInt32)) {
                return BinaryConverter.ToUInt32(source, offset, conversionOrder);
            }
            if (conversionType == typeof (Int64)) {
                return BinaryConverter.ToInt64(source, offset, conversionOrder);
            }
            if (conversionType == typeof (UInt64)) {
                return BinaryConverter.ToUInt64(source, offset, conversionOrder);
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
        
        private static void InsertBytes(byte[] destination, int offset, byte[] bytes) {
            Buffer.BlockCopy(bytes, 0, destination, offset, bytes.Length);
        }
        #endregion
    }
}
