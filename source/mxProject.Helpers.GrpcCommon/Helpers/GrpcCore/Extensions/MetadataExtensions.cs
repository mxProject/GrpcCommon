using System;
using System.Collections.Generic;
using System.Text;

using Grpc.Core;

namespace mxProject.Helpers.GrpcCore.Extensions
{

    /// <summary>
    /// Extention methods of <see cref="Metadata"/>.
    /// </summary>
    public static class MetadataExtensions
    {

        #region ContainsKey

        /// <summary>
        /// Gets a value indicating whether the specified key is included. This method uses the brute force algorithm.
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="key">The key.</param>
        /// <returns>true if the key is contained; otherwise false.</returns>
        public static bool ContainsKey(this Metadata metadata, string key)
        {

            for (int i = 0; i < metadata.Count; ++i)
            {
                if (IsSameKey(metadata[i].Key, key)) { return true; }
            }

            return false;

        }

        #endregion

        #region IndexOf

        /// <summary>
        /// Gets the index associated with the specified key. This method uses the brute force algorithm.
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="key">The key.</param>
        /// <returns>Index if the key is contained; otherwise -1.</returns>
        public static int IndexOf(this Metadata metadata, string key)
        {

            for (int i = 0; i < metadata.Count; ++i)
            {
                if (IsSameKey(metadata[i].Key, key)) { return i; }
            }

            return -1;

        }

        #endregion

        #region gets value

        /// <summary>
        /// Gets the value associated with the specified key. This method uses the brute force algorithm.
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>true if the key is found; otherwise false.</returns>
        public static bool TryGetStringValue(this Metadata metadata, string key, out string value)
        {

            for (int i = 0; i < metadata.Count; ++i)
            {
                if (IsSameKey(metadata[i].Key, key))
                {
                    value = metadata[i].Value;
                    return true;
                }
            }

            value = null;
            return false;

        }

        /// <summary>
        /// Gets the value associated with the specified key. This method uses the brute force algorithm.
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>true if the key is found; otherwise false.</returns>
        public static bool TryGetBinaryValue(this Metadata metadata, string key, out byte[] value)
        {

            for (int i = 0; i < metadata.Count; ++i)
            {
                if (IsSameKey(metadata[i].Key, key))
                {
                    value = metadata[i].ValueBytes;
                    return true;
                }
            }

            value = null;
            return false;

        }

        /// <summary>
        /// Gets the value associated with the specified key. This method uses the brute force algorithm.
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="key">The key.</param>
        /// <returns>The value. If the key is not found, returns null.</returns>
        public static string GetStringValueOrNull(this Metadata metadata, string key)
        {

            if (!TryGetStringValue(metadata, key, out string value))
            {
                return null;
            }
            else
            {
                return value;
            }

        }

        /// <summary>
        /// Gets the value associated with the specified key. This method uses the brute force algorithm.
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="key">The key.</param>
        /// <returns>The value. If the key is not found, returns null.</returns>
        public static byte[] GetBinaryValueOrNull(this Metadata metadata, string key)
        {

            if (!TryGetBinaryValue(metadata, key, out byte[] value))
            {
                return null;
            }
            else
            {
                return value;
            }

        }

        /// <summary>
        /// Gets the value associated with the specified key. This method uses the brute force algorithm.
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value when the key is not found.</param>
        /// <returns>The value. If the key is not found, returns <paramref name="defaultValue"/>.</returns>
        public static string GetStringValueOrDefault(this Metadata metadata, string key, string defaultValue)
        {

            if (!TryGetStringValue(metadata, key, out string value))
            {
                return defaultValue;
            }
            else
            {
                return value;
            }

        }

        /// <summary>
        /// Gets the value associated with the specified key. This method uses the brute force algorithm.
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value when the key is not found.</param>
        /// <returns>The value. If the key is not found, returns <paramref name="defaultValue"/>.</returns>
        public static byte[] GetBinaryValueOrDefault(this Metadata metadata, string key, byte[] defaultValue)
        {

            if (!TryGetBinaryValue(metadata, key, out byte[] value))
            {
                return defaultValue;
            }
            else
            {
                return value;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static bool IsSameKey(string a, string b)
        {
            return string.Compare(a, b, true) == 0;
        }

        #endregion

        #region sets value

        /// <summary>
        /// Sets the specified key and value.
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="key">The key.</param>
        /// <param name="value">The string value.</param>
        public static void SetStringValue(this Metadata metadata, string key, string value)
        {

            int index = metadata.IndexOf(key);

            if (index >= 0) { metadata.RemoveAt(index); }

            metadata.Add(key, value ?? "");

        }

        /// <summary>
        /// Sets the specified key and value.
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="key">The key.</param>
        /// <param name="value">The binary value.</param>
        public static void SetBinaryValue(this Metadata metadata, string key, byte[] value)
        {

            int index = metadata.IndexOf(key);

            if (index >= 0) { metadata.RemoveAt(index); }

            metadata.Add(key, value ?? new byte[] { });

        }

        #endregion

        #region adds value

        /// <summary>
        /// Sets the specified key and value. Nothing is done if it has already been set.
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="key">The key.</param>
        /// <param name="value">The string value.</param>
        /// <returns>true if it added; otherwise false.</returns>
        public static bool AddStringValue(this Metadata metadata, string key, string value)
        {

            int index = metadata.IndexOf(key);

            if (index >= 0) { return false; }

            metadata.Add(key, value ?? "");

            return true;

        }

        /// <summary>
        /// Sets the specified key and value. Nothing is done if it has already been set.
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="key">The key.</param>
        /// <param name="value">The binary value.</param>
        /// <returns>true if it added; otherwise false.</returns>
        public static bool AddBinaryValue(this Metadata metadata, string key, byte[] value)
        {

            int index = metadata.IndexOf(key);

            if (index >= 0) { return false; }

            metadata.Add(key, value ?? new byte[] { });

            return true;

        }

        #endregion

        #region enumerate

        /// <summary>
        /// Enumerates string values.
        /// </summary>
        /// <returns>The keys and the values.</returns>
        public static IEnumerable<Metadata.Entry> EnumerateStringValues(this Metadata metadata)
        {
            for (int i = 0; i < metadata.Count; ++i)
            {
                if (metadata[i].IsBinary) { continue; }
                yield return metadata[i];
            }
        }

        /// <summary>
        /// Enumerates binary values.
        /// </summary>
        /// <returns>The keys and the values.</returns>
        public static IEnumerable<Metadata.Entry> EnumerateBinaryValues(this Metadata metadata)
        {
            for (int i = 0; i < metadata.Count; ++i)
            {
                if (!metadata[i].IsBinary) { continue; }
                yield return metadata[i];
            }
        }

        #endregion

        #region ToDictionary

        /// <summary>
        /// Creates a dictionary containing string values.
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="firstValueWhenKeyDuplicate">A value whether the first found value is considered valid if the key is duplicated.</param>
        /// <returns>The keys and the values.</returns>
        public static IDictionary<string, string> ToStringDictionary(this Metadata metadata, bool firstValueWhenKeyDuplicate = true)
        {

            Dictionary<string, string> dic = new Dictionary<string, string>(metadata.Count);

            for (int i = 0; i < metadata.Count; ++i)
            {
                if (metadata[i].IsBinary) { continue; }

                string key = metadata[i].Key;

                if (firstValueWhenKeyDuplicate)
                {
                    if (!dic.ContainsKey(key)) { dic.Add(key, metadata[i].Value); }
                }
                else
                {
                    dic[key] = metadata[i].Value;
                }
            }

            return dic;

        }

        /// <summary>
        /// Creates a dictionary containing string values.
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns>The keys and the values.</returns>
        public static IDictionary<string, IList<string>> ToStringListDictionary(this Metadata metadata)
        {

            Dictionary<string, IList<string>> dic = new Dictionary<string, IList<string>>(metadata.Count);

            for (int i = 0; i < metadata.Count; ++i)
            {
                if (metadata[i].IsBinary) { continue; }

                string key = metadata[i].Key;

                if (!dic.TryGetValue(key, out IList<string> list))
                {
                    list = new List<string>(1);
                    dic.Add(key, list);
                }

                list.Add(metadata[i].Value);
            }

            return dic;

        }

        /// <summary>
        /// Creates a dictionary containing bytes values.
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="firstValueWhenKeyDuplicate">A value whether the first found value is considered valid if the key is duplicated.</param>
        /// <returns>The keys and the values.</returns>
        public static IDictionary<string, byte[]> ToBinaryDictionary(this Metadata metadata, bool firstValueWhenKeyDuplicate = true)
        {

            Dictionary<string, byte[]> dic = new Dictionary<string, byte[]>(metadata.Count);

            for (int i = 0; i < metadata.Count; ++i)
            {
                if (!metadata[i].IsBinary) { continue; }

                string key = metadata[i].Key;

                if (firstValueWhenKeyDuplicate)
                {
                    if (!dic.ContainsKey(key)) { dic.Add(key, metadata[i].ValueBytes); }
                }
                else
                {
                    dic[key] = metadata[i].ValueBytes;
                }
            }

            return dic;

        }

        /// <summary>
        /// Creates a dictionary containing bytes values.
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns>The keys and the values.</returns>
        public static IDictionary<string, IList<byte[]>> ToBinaryListDictionary(this Metadata metadata)
        {

            Dictionary<string, IList<byte[]>> dic = new Dictionary<string, IList<byte[]>>(metadata.Count);

            for (int i = 0; i < metadata.Count; ++i)
            {
                if (!metadata[i].IsBinary) { continue; }

                string key = metadata[i].Key;

                if (!dic.TryGetValue(key, out IList<byte[]> list))
                {
                    list = new List<byte[]>(1);
                    dic.Add(key, list);
                }

                list.Add(metadata[i].ValueBytes);
            }

            return dic;

        }

        #endregion

    }

}
