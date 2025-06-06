// File Name: UnsafeMarshaler.cs
// Project: ByteForge
// GitHub: https://github.com/Malcadore/ByteForge
// © 2025 Scott Pavetti.  Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.InteropServices;

namespace ByteForge {

    /// <summary>
    /// Tclass provides unsafe marshaling methods for converting structures to byte arrays and vice versa.
    /// It is not fully tested and should be used with caution.  When testing is done, this class will be
    /// in a position to made public.
    /// CurrentlThe UnsafeMarshaler and UnsafeMarshaler2 need to be reviewed.  The variants are slightly 
    /// different and require further research to see which is preferred.
    /// </summary>
    internal class UnsafeMarshaler {
        public static byte[] StructureToByteArray<T>(T obj) where T : struct {
            int len = Marshal.SizeOf(obj);

            byte[] arr = new byte[len];

            IntPtr ptr = Marshal.AllocHGlobal(len);

            Marshal.StructureToPtr(obj, ptr, true);

            Marshal.Copy(ptr, arr, 0, len);

            Marshal.FreeHGlobal(ptr);

            return arr;
        }
        public static T ByteArrayToStructure<T>(byte[] bytearray) where T : struct {
            T result = default(T);
            int len = Marshal.SizeOf(result);

            IntPtr i = Marshal.AllocHGlobal(len);

            Marshal.Copy(bytearray, 0, i, len);

            result = (T)Marshal.PtrToStructure(i, typeof(T));

            Marshal.FreeHGlobal(i);
            return result;
        }
    }

    internal static class UnsafeMarshaler2 {
        public static T BytesToStruct<T>(byte[] rawData) where T : struct {
            T result;

            var handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);

            try {
                var rawDataPtr = handle.AddrOfPinnedObject();
                result = (T)Marshal.PtrToStructure(rawDataPtr, typeof(T));
            } finally {
                handle.Free();
            }

            return result;
        }

        public static byte[] StructToBytes<T>(T data) where T : struct {
            var rawData = new byte[Marshal.SizeOf(data)];
            var handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);
            try {
                var rawDataPtr = handle.AddrOfPinnedObject();
                Marshal.StructureToPtr(data, rawDataPtr, false);
            } finally {
                handle.Free();
            }
            return rawData;
        }
    }
}
