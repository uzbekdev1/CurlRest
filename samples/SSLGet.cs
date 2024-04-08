using CurlRest;
using System;

namespace samples
{
    class SSLGet
    {
        public static void Run(string[] args)
        {
            try
            {
                Curl.GlobalInit((int)CURLinitFlag.CURL_GLOBAL_ALL);

                Easy easy = new Easy();

                Easy.WriteFunction wf = new Easy.WriteFunction(OnWriteData);
                easy.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, wf);

                Easy.SSLContextFunction sf = new Easy.SSLContextFunction(OnSSLContext);
                easy.SetOpt(CURLoption.CURLOPT_SSL_CTX_FUNCTION, sf);

                easy.SetOpt(CURLoption.CURLOPT_URL, args[0]);
                easy.SetOpt(CURLoption.CURLOPT_CAINFO, "ca-bundle.crt");

                easy.Perform();
                easy.Cleanup();

                Curl.GlobalCleanup();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static int OnWriteData(byte[] buf, int size, int nmemb,
            object extraData)
        {
            Console.Write(System.Text.Encoding.UTF8.GetString(buf));
            return size * nmemb;
        }

        public static CURLcode OnSSLContext(SSLContext ctx, object extraData)
        {
            // To do anything useful with the SSLContext object, you'll need
            // to call the OpenSSL native methods on your own. So for this
            // demo, we just return what cURL is expecting.
            return CURLcode.CURLE_OK;
        }
    }
}