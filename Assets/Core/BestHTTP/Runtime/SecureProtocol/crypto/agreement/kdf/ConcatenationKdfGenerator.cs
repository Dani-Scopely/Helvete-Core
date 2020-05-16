#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)
#pragma warning disable
using System;

using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement.Kdf
{
    /**
     * Generator for Concatenation Key Derivation Function defined in NIST SP 800-56A, Sect 5.8.1
     */
    public class ConcatenationKdfGenerator
        :   IDerivationFunction
    {
        private readonly IDigest mDigest;

        private byte[] mShared;
        private byte[] mOtherInfo;
        private int mHLen;

        /**
         * @param digest the digest to be used as the source of generated bytes
         */
        public ConcatenationKdfGenerator(IDigest digest)
        {
            this.mDigest = digest;
            this.mHLen = digest.GetDigestSize();
        }

        public virtual void Init(IDerivationParameters param)
        {
            if (!(param is KdfParameters))
                throw new ArgumentException("KDF parameters required for ConcatenationKdfGenerator");

            KdfParameters p = (KdfParameters)param;

            mShared = p.GetSharedSecret();
            mOtherInfo = p.GetIV();
        }

        /**
         * return the underlying digest.
         */
        public virtual IDigest Digest
        {
            get { return mDigest; }
        }

        /**
         * fill len bytes of the output buffer with bytes generated from
         * the derivation function.
         *
         * @throws DataLengthException if the out buffer is too small.
         */
        public virtual int GenerateBytes(byte[]	outBytes, int outOff, int len)
        {
            if ((outBytes.Length - len) < outOff)
                throw new DataLengthException("output buffer too small");

            byte[] hashBuf = new byte[mHLen];
            byte[] C = new byte[4];
            uint counter = 1;
            int outputLen = 0;

            mDigest.Reset();

            if (len > mHLen)
            {
                do
                {
                    Pack.UInt32_To_BE(counter, C);

                    mDigest.BlockUpdate(C, 0, C.Length);
                    mDigest.BlockUpdate(mShared, 0, mShared.Length);
                    mDigest.BlockUpdate(mOtherInfo, 0, mOtherInfo.Length);

                    mDigest.DoFinal(hashBuf, 0);

                    Array.Copy(hashBuf, 0, outBytes, outOff + outputLen, mHLen);
                    outputLen += mHLen;
                }
                while ((counter++) < (len / mHLen));
            }

            if (outputLen < len)
            {
                Pack.UInt32_To_BE(counter, C);

                mDigest.BlockUpdate(C, 0, C.Length);
                mDigest.BlockUpdate(mShared, 0, mShared.Length);
                mDigest.BlockUpdate(mOtherInfo, 0, mOtherInfo.Length);

                mDigest.DoFinal(hashBuf, 0);

                Array.Copy(hashBuf, 0, outBytes, outOff + outputLen, len - outputLen);
            }

            return len;
        }
    }
}
#pragma warning restore
#endif
