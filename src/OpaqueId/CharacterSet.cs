namespace OpaqueId
{
    public static class CharacterSet
    {
        /// <summary>
        /// 0 and 1
        /// </summary>
        public const string Binary = "01";

        /// <summary>
        /// 0-7
        /// </summary>
        public const string Octal = "01234567";

        /// <summary>
        /// 0-9, A-F
        /// </summary>
        public const string Hexadecimal = "0123456789ABCDEF";

        /// <summary>
        /// 0-9, A-Z
        /// </summary>
        public const string Base36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// A-Z, a-z, 0-9
        /// </summary>
        public const string Base64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";
    }
}
