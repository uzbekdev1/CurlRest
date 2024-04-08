using System;

namespace CurlRest
{
	/// <summary>
	/// An instance of this class is passed to the delegate
	/// <see cref="Easy.SSLContextFunction"/>, if it's implemented.
	/// Within that delegate, the code will have to make native calls to
	/// the <c>OpenSSL</c> library with the value returned from the
	/// <see cref="SSLContext.Context"/> property cast to an
	/// <c>SSL_CTX</c> pointer.
	/// </summary>
	public sealed class SSLContext
	{
        private IntPtr m_pvContext;

		internal SSLContext(IntPtr pvContext)
		{
            m_pvContext = pvContext;
		}

        /// <summary>
        /// Get the underlying OpenSSL context.
        /// </summary>
        public IntPtr Context
        {
            get
            {
                return m_pvContext;
            }
        }
	}
}
