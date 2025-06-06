// File Name: ISerializer.cs
// Project: ByteForge
// GitHub: https://github.com/Malcadore/ByteForge
// © 2025 Scott Pavetti.  Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace ByteForge {
    /// <summary>
    /// Serializer definition for aligning other custom serializers 
    /// in client code.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISerializer<T> where T: struct {
        /// <summary>
        /// A method definition that specifies Serialize with
        /// this method signature.
        /// </summary>
        /// <param name="objectToSerialize"></param>
        /// <returns></returns>
        byte[] Serialize(T objectToSerialize);
        /// <summary>
        /// A method definition that specifics Deserialize with
        /// this method signature.
        /// </summary>
        /// <param name="binaryRepresentation"></param>
        /// <returns></returns>
        T Deserialize(byte[] binaryRepresentation);
    }
}
