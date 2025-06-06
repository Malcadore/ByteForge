// File Name: PackedStructConverter.cs
// Project: ByteForge
// GitHub: https://github.com/Malcadore/ByteForge
// © 2025 Scott Pavetti.  Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;


namespace ByteForge {
    /// <summary>
    /// A generic class that turns structures into byte arrays, as well as
    /// converts byte arrays into structures.
    /// </summary>
    /// <remarks>
    /// Be warned that the structures are assumed to be parsed in source order
    /// from within the definition.  This means that the compiler is not guaranteed to
    /// generate code that puts the fields into memory in the exact way it's laid out
    /// in the source file.
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    [DebuggerStepThrough]
    public class PackedStructConverter<T> where T : struct {
        // a cached list of FieldInfo members.
        private readonly List<FieldInfo> fieldInfo = [];
        private int packedSize = -1;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PackedStructConverter() {
            fieldInfo = new List<FieldInfo>();
            var fields = typeof(T).GetFields();
            fieldInfo.AddRange(fields);
        }
        
        private void ForEach(Action<FieldInfo> action) {
            foreach (var fi in fieldInfo) {
                action(fi);
            }
        }

        /// <summary>
        /// Gets the packed size of the structure that this marshaler
        /// corresponds to.
        /// </summary>
        public int PackedSize {
            get {
                if (packedSize == -1) {
                    packedSize++; // bump the value back up to zero.
                    ForEach(fi => {
                        Type ft = fi.FieldType;
                        packedSize += Marshal.SizeOf(ft);
                    });
                }
                return packedSize;
            }
        }
     
        /// <summary>
        /// Serializes a struct into a byte array, with the specified byte ordering.
        /// </summary>
        /// <param name="obj">The live instance of the structure to turn into a byte array.</param>
        /// <param name="conversionOrder">The byte order used in the output array.</param>
        /// <returns></returns>
        public byte[] Serialize(T obj, ByteOrder conversionOrder) {
            var structureSize = PackedSize;
            var result = new byte[structureSize];

            int offset = 0;

            ForEach(fi => {
                int size = Marshal.SizeOf(fi.FieldType);
                Conversion.AddToByteArray(result, offset, fi.GetValue(obj), conversionOrder);
                offset += size;
            });
            return result;
        }

        /// <summary>
        /// Overload.  Deserializes the byte array into the struct starting at the beginning
        /// of the array.
        /// </summary>
        /// <param name="nativeData">The array representation of the struct.</param>
        /// <param name="conversionOrder">The byte order used in the array.</param>
        /// <returns></returns>
        public T Deserialize(byte[] nativeData, ByteOrder conversionOrder) {
            var result = new T();
            var obj = result as object; // boxing required
            var offset = 0;
            foreach (var field in fieldInfo) {
                var size = Marshal.SizeOf(field.FieldType);
                var value = Conversion.FConvert(nativeData, field.FieldType, offset, conversionOrder);
                field.SetValue(obj, value);
                offset += size;
            }
            result = (T)obj;
            return result;
        }
        /// <summary>
        /// Deserializes the byte array into the struct starting at the offset
        /// supplied, and using the byte ordering supplied.
        /// </summary>
        /// <param name="nativeData">The array representation of the struct.</param>
        /// <param name="offset">Starts deserializing the array here.</param>
        /// <param name="conversionOrder">The byte order used in the array.</param>
        /// <returns></returns>
        public T Deserialize(byte[] nativeData, Int32 offset, ByteOrder conversionOrder) {
            T result = new T();
            var obj = result as object; // boxing required
            int theOffset = offset;
            foreach (var field in fieldInfo) {
                int size = Marshal.SizeOf(field.FieldType);
                var value = Conversion.FConvert(nativeData, field.FieldType, theOffset, conversionOrder);
                field.SetValue(obj, value);
                theOffset += size;
            }
            result = (T)obj;
            return result;
        }

    }
}
