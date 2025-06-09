// File Name: Endian.cs
// Project: ByteForge
// GitHub: https://github.com/Malcadore/ByteForge
// © 2025 Scott Pavetti.  Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics;

namespace ByteForge {
    /// <summary>
    /// A class that helps with checking the endianness as well as converting the endian
    /// posture of multi-byte value types.
    /// TODO: This class's methods should be performance tested against regular 
    /// calls to the platform's byte swapping techniques.
    /// </summary>
    [DebuggerStepThrough]
    public static class Endian {
        private static readonly bool isLittleEndian = BitConverter.IsLittleEndian;
        
        /// <summary>
        /// Swaps the bytes in an Int16 value using bit operators.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int16 SwapInt16(Int16 value) {
            return (Int16)(((value & 0xff) << 8) | ((value >> 8) & 0xff));
        }
        /// <summary>
        /// Swaps the bytes in a UInt16 value using bit operators.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static UInt16 SwapUInt16(UInt16 value) {
            return (UInt16)(((value & 0xff) << 8) | ((value >> 8) & 0xff));
        }
        /// <summary>
        /// Swaps the bytes in an Int32 value using bit operators.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int32 SwapInt32(Int32 value) {
            return ((SwapInt16((Int16)value) & 0xffff) << 0x10) | (SwapInt16((Int16)(value >> 0x10)) & 0xffff);
        }
        /// <summary>
        /// Swaps the bytes in a UInt32 value using bit operators.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static UInt32 SwapUInt32(UInt32 value) {
            return (UInt32)(((SwapUInt16((UInt16)value) & 0xffff) << 0x10) | (SwapUInt16((UInt16)(value >> 0x10)) & 0xffff));
        }

        /// <summary>
        /// Swaps the bytes in an Int64 value using bit operators.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int64 SwapInt64(Int64 value) {
            return ((SwapInt32((Int32)value) & 0xffffffffL) << 0x20) | (SwapInt32((Int32)(value >> 0x20)) & 0xffffffffL);
        }
        /// <summary>
        /// Swaps the bytes in a UInt64 value using bit operators.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static UInt64 SwapUInt64(UInt64 value) {
            return (UInt64)(((SwapUInt32((UInt32)value) & 0xffffffffL) << 0x20) | (SwapUInt32((UInt32)(value >> 0x20)) & 0xffffffffL));
        } 
        /// <summary>
        /// Checks the underlying endian of the platform using the BitConverter class.
        /// <seealso cref="BitConverter"/>
        /// </summary>
        public static bool IsBigEndian {
            get { return !isLittleEndian; }
        }
        /// <summary>
        /// Checks the underlying endian of the platform using the BitConverter class.
        /// <seealso cref="BitConverter"/>
        /// </summary>
        public static bool IsLittleEndian {
            get { return isLittleEndian; }
        }
    }
}
