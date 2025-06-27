// File Name: ConversionException.cs
// Project: ByteForge
// GitHub: https://github.com/Malcadore/ByteForge
// © 2025 Scott Pavetti.  Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace ByteForge
{
    /// <summary>
    /// An exception that's used for when conversions go wrong.
    /// </summary>
    [Serializable]
    public class ConversionException : Exception
    {

        public ConversionException() { }
        /// <summary>
        /// String message constructor.
        /// </summary>
        /// <param name="message"></param>
        public ConversionException(string message) : base(message) { }
        /// <summary>
        /// String and inner exception constructor.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public ConversionException(string message, Exception inner) : base(message, inner) { }


    }
}
