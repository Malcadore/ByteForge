
using System;
using System.Runtime.InteropServices;

namespace Msa.Serialization {
#warning The UnsafeMarshaler and UnsafeMarshaler2 need to be reviewed.  The variants are slightly different and require further research to see which is preferred.
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
