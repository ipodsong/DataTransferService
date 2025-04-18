namespace DataTransferService.Utils
{
    /// <summary>
    /// Utility for validation.
    /// </summary>
    public static class ValidationUtil
    {
        /// <summary>
        /// Checks whether the given string array is null or if all elements are null or empty.
        /// </summary>
        /// <param name="strings">A variable number of string arguments to validate.</param>
        /// <returns>
        /// - true: If the array is null or all strings are null or empty.  
        /// - false: If at least one valid (non-null and non-empty) string exists.
        /// </returns>
        public static bool IsNullOrEmptyStrings(params string?[] strings)
        {
            // Return true if the array itself is null
            if (strings == null) return true;

            // Return true if any string is null or empty
            foreach (string? s in strings)
            {
                if (string.IsNullOrEmpty(s)) return true;
            }

            // All strings are not null or not empty
            return false;
        }
    }
}
