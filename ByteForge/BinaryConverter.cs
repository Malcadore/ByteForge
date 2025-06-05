

using System;
using System.Diagnostics;

namespace ByteForge {
    // in Windows, host order is ALWAYS Little Endian.

    /*  README - IMPORTANT!
     * This file has a lot of tricky syntax because of all the overloading that has to take place
     * when dealing with ValueTypes.  Please use extra caution as similar looking syntax abounds
     * and it is very easy to call the incorrect overload in some cases.
     */
    /// <summary>
    /// A compeditor to the BitConverter, but can do specific endian conversions.
    /// </summary>
    [DebuggerStepThrough]
    public static class BinaryConverter {

        #region To_XXX

        #region Signed 64 bit int parsing
        /// <summary>
        /// Converts 8 bytes to Int64 value.
        /// </summary>
        /// <param name="array">The array to use to create the Int64 value.</param>
        /// <param name="startIndex">The array index to start. Inclusive.</param>
        /// <param name="sourceOrder">Byte order of the input array.</param>
        /// <returns></returns>
        public static Int64 ToInt64(byte[] array, Int32 startIndex, ByteOrder sourceOrder) {
            return sourceOrder == ByteOrder.LittleEndian ? ToInt64LE(array, startIndex) : ToInt64BE(array, startIndex);
        }
        private static Int64 ToInt64BE(byte[] array, int startIndex) {
            var result = BitConverter.ToInt64(array, startIndex);
            return Endian.SwapInt64(result);
        }
        private static Int64 ToInt64LE(byte[] array, int startIndex) {
            return BitConverter.ToInt64(array, startIndex);
        }
        #endregion

        #region Unsigned 64 bit int parsing
        /// <summary>
        /// Converts 8 bytes into a UInt64 value;
        /// </summary>
        /// <param name="array">The array to use to create the Int64 value.</param>
        /// <param name="startIndex">The array index to start. Inclusive.</param>
        /// <param name="sourceOrder">Byte order of the input array.</param>
        /// <returns></returns>
        public static UInt64 ToUInt64(byte[] array, Int32 startIndex, ByteOrder sourceOrder) {
            return sourceOrder == ByteOrder.LittleEndian ? ToUInt64LE(array, startIndex) : ToUInt64BE(array, startIndex);
        }
        private static UInt64 ToUInt64BE(byte[] array, int startIndex) {
            var result = BitConverter.ToUInt64(array, startIndex);
            return Endian.SwapUInt64(result);
        }
        private static UInt64 ToUInt64LE(byte[] array, int startIndex) {
            return BitConverter.ToUInt64(array, startIndex);
        }
        #endregion
        
        #region Unsigned 32 bit int parsing
        /// <summary>
        /// Converts 4 bytes into a UInt32 value;
        /// </summary>
        /// <param name="array">The array to use to create the Int64 value.</param>
        /// <param name="startIndex">The array index to start. Inclusive.</param>
        /// <param name="sourceOrder">Byte order of the input array.</param>
        /// <returns></returns>
        public static UInt32 ToUInt32(byte[] array, Int32 startIndex, ByteOrder sourceOrder) {
            return sourceOrder == ByteOrder.LittleEndian ? ToUInt32LE(array, startIndex) : ToUInt32BE(array, startIndex);
        }
        private static UInt32 ToUInt32BE(byte[] array, int startIndex) {
            var result = BitConverter.ToUInt32(array, startIndex);
            return Endian.SwapUInt32(result);
        }
        private static UInt32 ToUInt32LE(byte[] array, int startIndex) {
            return BitConverter.ToUInt32(array, startIndex);
        }
        #endregion

        #region Signed 32 bit int parsing
        /// <summary>
        /// Converts 4 bytes into an Int32 value;
        /// </summary>
        /// <param name="array">The array to use to create the Int64 value.</param>
        /// <param name="startIndex">The array index to start. Inclusive.</param>
        /// <param name="sourceOrder">Byte order of the input array.</param>
        /// <returns></returns>
        public static Int32 ToInt32(byte[] array, Int32 startIndex, ByteOrder sourceOrder) {
            return sourceOrder == ByteOrder.LittleEndian ? ToInt32LE(array, startIndex) : ToInt32BE(array, startIndex);
        }
        private static Int32 ToInt32BE(byte[] array, int startIndex) {
            var result = BitConverter.ToInt32(array, startIndex);
            return Endian.SwapInt32(result);
        }
        private static Int32 ToInt32LE(byte[] array, int startIndex) {
            return BitConverter.ToInt32(array, startIndex);
        }
        #endregion

        #region Unsigned 16 bit int parsing
        /// <summary>
        /// Converts 2 bytes into a UInt16 value;
        /// </summary>
        /// <param name="array">The array to use to create the Int64 value.</param>
        /// <param name="startIndex">The array index to start. Inclusive.</param>
        /// <param name="sourceOrder">Byte order of the input array.</param>
        /// <returns></returns>
        public static UInt16 ToUInt16(byte[] array, Int32 startIndex, ByteOrder sourceOrder) {
            return sourceOrder == ByteOrder.LittleEndian ? ToUInt16LE(array, startIndex) : ToUInt16BE(array, startIndex);
        }
        private static UInt16 ToUInt16BE(byte[] array, int startIndex) {
            var result = BitConverter.ToUInt16(array, startIndex);
            return Endian.SwapUInt16(result);
        }
        private static UInt16 ToUInt16LE(byte[] array, int startIndex) {
            return BitConverter.ToUInt16(array, startIndex);
        }
        #endregion

        #region Signed 16 bit int parsing
        /// <summary>
        /// Converts 2 bytes into an Int16 value;
        /// </summary>
        /// <param name="array">The array to use to create the Int64 value.</param>
        /// <param name="startIndex">The array index to start. Inclusive.</param>
        /// <param name="sourceOrder">Byte order of the input array.</param>
        /// <returns></returns>
        public static Int16 ToInt16(byte[] array, Int32 startIndex, ByteOrder sourceOrder) {
            return sourceOrder == ByteOrder.LittleEndian ? ToInt16LE(array, startIndex) : ToInt16BE(array, startIndex);
        }
        private static Int16 ToInt16BE(byte[] array, int startIndex) {
            var result = BitConverter.ToInt16(array, startIndex);
            return Endian.SwapInt16(result);
        }
        private static Int16 ToInt16LE(byte[] array, int startIndex) {
            return BitConverter.ToInt16(array, startIndex);
        }
        #endregion

        #endregion

        #region GetBytes(XX)

        #region Unsigned 32 bit
        /// <summary>
        /// Gets the bytes for an Int32 value.
        /// </summary>
        /// <param name="valueInLittleEndian">Value to be converted, must be little endian.</param>
        /// <param name="outputOrder">The byte order that the output array should be in.</param>
        /// <returns></returns>
        public static byte[] GetBytes(UInt32 valueInLittleEndian, ByteOrder outputOrder) {
            UInt32 convertedIfBE = outputOrder == ByteOrder.LittleEndian ? valueInLittleEndian : Endian.SwapUInt32(valueInLittleEndian);
            return BitConverter.GetBytes(convertedIfBE);
        }
        #endregion

        #region Signed 32 bit
        /// <summary>
        /// Gets the bytes for an Int32 value.
        /// </summary>
        /// <param name="valueInLittleEndian">Value to be converted, must be little endian.</param>
        /// <param name="outputOrder">The byte order that the output array should be in.</param>
        /// <returns></returns>
        public static byte[] GetBytes(Int32 valueInLittleEndian, ByteOrder outputOrder) {
            Int32 convertedIfBE = outputOrder == ByteOrder.LittleEndian ? valueInLittleEndian : Endian.SwapInt32(valueInLittleEndian);
            return BitConverter.GetBytes(convertedIfBE);
        }
        #endregion

        #region Unsigned 16 bit
        /// <summary>
        /// Gets the bytes for a UInt16 value.
        /// </summary>
        /// <param name="valueInLittleEndian">Value to be converted, must be little endian.</param>
        /// <param name="outputOrder">The byte order that the output array should be in.</param>
        /// <returns></returns>
        public static byte[] GetBytes(UInt16 valueInLittleEndian, ByteOrder outputOrder) {
            UInt16 convertedIfBE = outputOrder == ByteOrder.LittleEndian ? valueInLittleEndian : Endian.SwapUInt16(valueInLittleEndian);
            return BitConverter.GetBytes(convertedIfBE);
        }
        #endregion

        #region Signed 16 bit
        /// <summary>
        /// Gets the bytes for an Int16 value.
        /// </summary>
        /// <param name="valueInLittleEndian">Value to be converted, must be little endian.</param>
        /// <param name="outputOrder">The byte order that the output array should be in.</param>
        /// <returns></returns>
        public static byte[] GetBytes(Int16 valueInLittleEndian, ByteOrder outputOrder) {
            Int16 convertedIfBE = outputOrder == ByteOrder.LittleEndian ? valueInLittleEndian : Endian.SwapInt16(valueInLittleEndian);
            return BitConverter.GetBytes(convertedIfBE);
        }
        #endregion

        #region Unsigned 64 bit
        /// <summary>
        /// Gets the bytes for a UInt64 value.
        /// </summary>
        /// <param name="valueInLittleEndian">Value to be converted, must be little endian.</param>
        /// <param name="outputOrder">The byte order that the output array should be in.</param>
        /// <returns></returns>
        public static byte[] GetBytes(UInt64 valueInLittleEndian, ByteOrder outputOrder) {
            UInt64 convertedIfBE = outputOrder == ByteOrder.LittleEndian ? valueInLittleEndian : Endian.SwapUInt64(valueInLittleEndian);
            return BitConverter.GetBytes(convertedIfBE);
        }
        #endregion

        #region Signed 64 bit
        /// <summary>
        /// Gets the bytes for an Int64 value.
        /// </summary>
        /// <param name="valueInLittleEndian">Value to be converted, must be little endian.</param>
        /// <param name="outputOrder">The byte order that the output array should be in.</param>
        /// <returns></returns>
        public static byte[] GetBytes(Int64 valueInLittleEndian, ByteOrder outputOrder) {
            Int64 convertedIfBE = outputOrder == ByteOrder.LittleEndian ? valueInLittleEndian : Endian.SwapInt64(valueInLittleEndian);
            return BitConverter.GetBytes(convertedIfBE);
        }
        #endregion

        #endregion

    }
}
