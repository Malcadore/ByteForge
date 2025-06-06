// File Name: ByteOrder.cs
// Project: ByteForge
// GitHub: https://github.com/Malcadore/ByteForge
// © 2025 Scott Pavetti.  Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace ByteForge {
    /// <summary>
    /// Represents the endian byte order of the platform.  This 
    /// enum is used during serialization and deserialization to 
    /// specify the byte order of multi-byte values.
    /// </summary>
    public enum ByteOrder {
        /// <summary>
        /// LSB first.
        /// </summary>
        LittleEndian,
        /// <summary>
        /// MSB first.
        /// </summary>
        BigEndian
    }
}
