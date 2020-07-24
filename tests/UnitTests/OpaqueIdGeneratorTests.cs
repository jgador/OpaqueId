using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpaqueId;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class OpaqueIdGeneratorTests
    {
        [TestMethod]
        public void GetGetOpaqueId_NoDuplicate_Test()
        {
            OpaqueIdGenerator producer = new OpaqueIdGenerator();
            Dictionary<string, string> dict = new Dictionary<string, string>();

            for (int i = 0; i < 1000; i++)
            {
                string opaqueId = producer.GetOpaqueId();
                (DateTimeOffset Timestamp, string OpaqueId) opaqueIdWithTimestamp = producer.GetOpaqueIdWithTimestamp();
                var a = opaqueIdWithTimestamp.Timestamp;

                Console.Out.WriteLine(a.ToString("yyyyMMdd-HH:mm:ss.ff"));

                Assert.IsTrue(dict.TryAdd(opaqueId, opaqueId), $"Duplicate trace id {opaqueId}");
            }
        }

        [TestMethod]
        public void Convert_EpochMilliseconds_ToBase63()
        {
            var startOfEpoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, new TimeSpan(0));
            var oneMillisecondFromEpoch = startOfEpoch.ToUnixTimeMilliseconds() + 1;
            var fiveYearsFromEpoch = startOfEpoch.AddYears(5).ToUnixTimeMilliseconds();

            OpaqueEncoding encoding = new OpaqueEncoding(CharacterSet.Base36);

            Assert.AreEqual("1", encoding.Convert(oneMillisecondFromEpoch));
            Assert.AreEqual("20H61HC0", encoding.Convert(fiveYearsFromEpoch));
        }

        [TestMethod]
        public void SampleOctalBaseTarget()
        {
            OpaqueIdGenerator producer = new OpaqueIdGenerator(CharacterSet.Octal);
            for (int i = 0; i < 5; i++)
            {
                Console.Out.WriteLine(producer.GetOpaqueId());
            }

            // Sample Output:

            // 27160176041003
            // 27160176041011
            // 27160176041013
            // 27160176041015
            // 27160176041017
        }

        [TestMethod]
        public void SampleHexadecimalBaseTarget()
        {
            OpaqueIdGenerator producer = new OpaqueIdGenerator(CharacterSet.Hexadecimal);
            for (int i = 0; i < 5; i++)
            {
                Console.Out.WriteLine(producer.GetOpaqueId());
            }

            // Sample Output:

            // 17381F9AF5E
            // 17381F9AF63
            // 17381F9AF66
            // 17381F9AF68
            // 17381F9AF6A
        }

        [TestMethod]
        public void SampleFiveBase36()
        {
            OpaqueIdGenerator producer = new OpaqueIdGenerator(CharacterSet.Base36);
            for (int i = 0; i < 5; i++)
            {
                Console.Out.WriteLine(producer.GetOpaqueId());
            }

            // Sample Output:

            // KD0J3XO5
            // KD0J3XOB
            // KD0J3XOD
            // KD0J3XOF
            // KD0J3XOG
        }

        [TestMethod]
        public void SampleFiveBase64()
        {
            OpaqueIdGenerator producer = new OpaqueIdGenerator(CharacterSet.Base64);
            for (int i = 0; i < 5; i++)
            {
                Console.Out.WriteLine(producer.GetOpaqueId());
            }

            // Sample Output:

            // XOB_iqI
            // XOB_iqP
            // XOB_iqR
            // XOB_iqS
            // XOB_iqU
        }
    }
}
