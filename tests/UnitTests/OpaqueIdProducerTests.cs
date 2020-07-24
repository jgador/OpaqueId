using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpaqueId;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class OpaqueIdProducerTests
    {
        [TestMethod]
        public void GetGetOpaqueId_NoDuplicate_Test()
        {
            OpaqueIdProducer producer = new OpaqueIdProducer();
            Dictionary<string, string> dict = new Dictionary<string, string>();

            for (int i = 0; i < 1000; i++)
            {
                var opaqueId = producer.GetOpaqueId();

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
            OpaqueIdProducer producer = new OpaqueIdProducer(CharacterSet.Octal);
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
            OpaqueIdProducer producer = new OpaqueIdProducer(CharacterSet.Hexadecimal);
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
            OpaqueIdProducer producer = new OpaqueIdProducer(CharacterSet.Base36);
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
            OpaqueIdProducer producer = new OpaqueIdProducer(CharacterSet.Base64);
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
