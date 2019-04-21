using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Grpc.Core;

using mxProject.Helpers.GrpcCore.Extensions;

namespace Test.mxProject.Helpers.Common
{
    [TestClass]
    public class TestMetadata
    {

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddStringValue()
        {
            Metadata entries = new Metadata();

            string key = "a";

            Assert.AreEqual(true, entries.AddStringValue(key, "1"));
            Assert.AreEqual(false, entries.AddStringValue(key, "2"));
            Assert.AreEqual("1", entries.GetStringValueOrNull(key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddBinaryValue()
        {
            Metadata entries = new Metadata();

            string key = "a" + Metadata.BinaryHeaderSuffix;

            Assert.AreEqual(true, entries.AddBinaryValue(key, new byte[] { 1 }));
            Assert.AreEqual(false, entries.AddBinaryValue(key, new byte[] { 1, 2 }));
            Assert.AreEqual(1, entries.GetBinaryValueOrNull(key).Length);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void SetStringValue()
        {
            Metadata entries = new Metadata();

            string key = "a";

            entries.SetStringValue(key, "1");
            entries.SetStringValue(key, "2");
            Assert.AreEqual("2", entries.GetStringValueOrNull(key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void SetBinaryValue()
        {
            Metadata entries = new Metadata();

            string key = "a" + Metadata.BinaryHeaderSuffix;

            entries.SetBinaryValue(key, new byte[] { 1 });
            entries.SetBinaryValue(key, new byte[] { 1, 2 });
            Assert.AreEqual(2, entries.GetBinaryValueOrNull(key).Length);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ContainsKey()
        {
            Metadata entries = CreateMetadata();

            Assert.AreEqual(true, entries.ContainsKey("a"));
            Assert.AreEqual(true, entries.ContainsKey("b"));
            Assert.AreEqual(true, entries.ContainsKey("c"));
            Assert.AreEqual(true, entries.ContainsKey("a" + Metadata.BinaryHeaderSuffix));
            Assert.AreEqual(true, entries.ContainsKey("A"));
            Assert.AreEqual(false, entries.ContainsKey("d"));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void IndexOf()
        {
            Metadata entries = CreateMetadata();

            Assert.AreEqual(0, entries.IndexOf("a"));
            Assert.AreEqual(1, entries.IndexOf("b"));
            Assert.AreEqual(2, entries.IndexOf("c"));
            Assert.AreEqual(4, entries.IndexOf("a" + Metadata.BinaryHeaderSuffix));
            Assert.AreEqual(0, entries.IndexOf("A"));
            Assert.AreEqual(-1, entries.IndexOf("d"));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetStringValueOrNull()
        {
            Metadata entries = CreateMetadata();

            Assert.AreEqual("1", entries.GetStringValueOrNull("a"));
            Assert.AreEqual("2", entries.GetStringValueOrNull("b"));
            Assert.AreEqual("3", entries.GetStringValueOrNull("c"));
            Assert.AreEqual("1", entries.GetStringValueOrNull("A"));
            Assert.IsNull(entries.GetStringValueOrNull("d"));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetStringValueOrDefault()
        {
            Metadata entries = CreateMetadata();

            Assert.AreEqual("1", entries.GetStringValueOrDefault("a", "0"));
            Assert.AreEqual("2", entries.GetStringValueOrDefault("b", "0"));
            Assert.AreEqual("3", entries.GetStringValueOrDefault("c", "0"));
            Assert.AreEqual("1", entries.GetStringValueOrDefault("A", "0"));
            Assert.AreEqual("0", entries.GetStringValueOrDefault("d", "0"));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetBinaryValueOrNull()
        {
            Metadata entries = CreateMetadata();

            Assert.AreEqual(1, entries.GetBinaryValueOrNull("a" + Metadata.BinaryHeaderSuffix).Length);
            Assert.AreEqual(2, entries.GetBinaryValueOrNull("b" + Metadata.BinaryHeaderSuffix).Length);
            Assert.AreEqual(3, entries.GetBinaryValueOrNull("c" + Metadata.BinaryHeaderSuffix).Length);
            Assert.AreEqual(1, entries.GetBinaryValueOrNull("A" + Metadata.BinaryHeaderSuffix).Length);
            Assert.IsNull(entries.GetBinaryValueOrNull("d" + Metadata.BinaryHeaderSuffix));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetBinaryValueOrDefault()
        {
            Metadata entries = CreateMetadata();

            Assert.AreEqual(1, entries.GetBinaryValueOrDefault("a" + Metadata.BinaryHeaderSuffix, new byte[] { }).Length);
            Assert.AreEqual(2, entries.GetBinaryValueOrDefault("b" + Metadata.BinaryHeaderSuffix, new byte[] { }).Length);
            Assert.AreEqual(3, entries.GetBinaryValueOrDefault("c" + Metadata.BinaryHeaderSuffix, new byte[] { }).Length);
            Assert.AreEqual(1, entries.GetBinaryValueOrDefault("A" + Metadata.BinaryHeaderSuffix, new byte[] { }).Length);
            Assert.AreEqual(0, entries.GetBinaryValueOrDefault("d" + Metadata.BinaryHeaderSuffix, new byte[] { }).Length);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void EnumerateStringValues()
        {
            Metadata entries = CreateMetadata();

            int count = 0;

            foreach (var entry in entries.EnumerateStringValues())
            {
                Console.WriteLine(entry);
                ++count;
            }

            Assert.AreEqual(4, count);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ToStringDictionary()
        {
            Metadata entries = CreateMetadata();

            IDictionary<string, string> dic = entries.ToStringDictionary(true);

            Assert.AreEqual(3, dic.Count);
            Assert.AreEqual("1", dic["a"]);

            dic = entries.ToStringDictionary(false);

            Assert.AreEqual(3, dic.Count);
            Assert.AreEqual("0", dic["a"]);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ToStringListDictionary()
        {
            Metadata entries = CreateMetadata();

            IDictionary<string, IList<string>> dic = entries.ToStringListDictionary();

            Assert.AreEqual(3, dic.Count);
            Assert.AreEqual(2, dic["a"].Count);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void EnumerateBinaryValues()
        {
            Metadata entries = CreateMetadata();

            int count = 0;

            foreach (var entry in entries.EnumerateBinaryValues())
            {
                Console.WriteLine(entry);
                ++count;
            }

            Assert.AreEqual(4, count);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ToBinaryDictionary()
        {
            Metadata entries = CreateMetadata();

            IDictionary<string, byte[]> dic = entries.ToBinaryDictionary(true);

            Assert.AreEqual(3, dic.Count);
            Assert.AreEqual(1, dic["a" + Metadata.BinaryHeaderSuffix].Length);

            dic = entries.ToBinaryDictionary(false);

            Assert.AreEqual(3, dic.Count);
            Assert.AreEqual(0, dic["a" + Metadata.BinaryHeaderSuffix].Length);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ToBinaryListDictionary()
        {
            Metadata entries = CreateMetadata();

            IDictionary<string, IList<byte[]>> dic = entries.ToBinaryListDictionary();

            Assert.AreEqual(3, dic.Count);
            Assert.AreEqual(2, dic["a" + Metadata.BinaryHeaderSuffix].Count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Metadata CreateMetadata()
        {
            Metadata entries = new Metadata();

            entries.Add("a", "1");
            entries.Add("b", "2");
            entries.Add("c", "3");

            entries.Add("a", "0");

            entries.Add("a" + Metadata.BinaryHeaderSuffix, new byte[] { 1 });
            entries.Add("b" + Metadata.BinaryHeaderSuffix, new byte[] { 1, 2 });
            entries.Add("c" + Metadata.BinaryHeaderSuffix, new byte[] { 1, 3, 4 });

            entries.Add("a" + Metadata.BinaryHeaderSuffix, new byte[] { });

            return entries;
        }

    }

}