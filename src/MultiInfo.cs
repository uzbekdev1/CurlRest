namespace CurlRest
{
    /// <summary>
    /// Wraps the <c>cURL</c> struct <c>CURLMsg</c>. This class provides
    /// status information following a <see cref="Multi"/> transfer.
    /// </summary>
    public sealed class MultiInfo
	{
        // private members
        CURLMSG  m_msg;
        Easy     m_easy;
        CURLcode m_result;

		internal MultiInfo(CURLMSG msg, Easy easy, CURLcode result)
		{
            m_msg = msg;
            m_easy = easy;
            m_result = result;
		}

        /// <summary>
        /// Get the status code from the <see cref="CURLMSG"/> enumeration.
        /// </summary>
        public CURLMSG Msg
        {
            get
            {
                return m_msg;
            }
        }

        /// <summary>
        /// Get the <see cref="Easy"/> object for this child.
        /// </summary>
        public Easy EasyHandle
        {
            get
            {
                return m_easy;
            }
        }

        /// <summary>
        /// Get the return code for the transfer, as a
        /// <see cref="CURLcode"/>.
        /// </summary>
        public CURLcode Result
        {
            get
            {
                return m_result;
            }
        }
    }
}
