﻿using System;
using System.Threading;

namespace OpaqueId
{
    /// <summary>
    /// Thread-safe opaque id generator class.
    /// </summary>
    public class OpaqueIdGenerator
    {
        private static readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1); // consumer can only be processed one at a time
        private readonly OpaqueEncoding _opaqueEncoding;

        /// <summary>
        /// Initializes new <see cref="OpaqueIdGenerator"/>.
        /// </summary>
        /// <param name="baseCharacters">The base characters. You may refer to <see cref="CharacterSet"/> or you may pass in your own set of base characters for encoding.</param>.
        public OpaqueIdGenerator(string baseCharacters)
        {
            _opaqueEncoding = new OpaqueEncoding(baseCharacters);
        }

        /// <summary>
        /// Initializes new <see cref="OpaqueIdGenerator"/> with base 36 characters.
        /// </summary>
        public OpaqueIdGenerator() : this(CharacterSet.Base36)
        {
        }

        /// <summary>
        /// Returns an opaque id.
        /// </summary>
        public string GetOpaqueId()
        {
            try
            {
                // acquire lock
                _lock.Wait();

                // A necessary delay by 1 millisecond in case it's racing a consumer that's submillisecond.
                Thread.Sleep(1);

                var timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                return _opaqueEncoding.Convert(timestamp);
            }
            finally
            {
                // release lock
                _lock.Release();
            }
        }

        /// <summary>
        /// Returns an opaque id and the timestamp encoded.
        /// </summary>
        public (DateTimeOffset Timestamp, string OpaqueId) GetOpaqueIdWithTimestamp()
        {
            try
            {
                // acquire lock
                _lock.Wait();

                // A necessary delay by 1 millisecond in case it's racing a consumer that's submillisecond.
                Thread.Sleep(1);

                var timestamp = DateTimeOffset.Now;
                return (timestamp, _opaqueEncoding.Convert(timestamp.ToUnixTimeMilliseconds()));
            }
            finally
            {
                // release lock
                _lock.Release();
            }
        }
    }
}
