using CurlRest;
using System;

namespace samples
{
    class BookPost
    {
        public static void Run(string[] args)
        {
            try
            {
                Curl.GlobalInit((int)CURLinitFlag.CURL_GLOBAL_ALL);

                Easy easy = new Easy();

                Easy.WriteFunction wf = new Easy.WriteFunction(OnWriteData);
                easy.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, wf);

                // simple post - with a string
                easy.SetOpt(CURLoption.CURLOPT_POSTFIELDS,
                    "url=index%3Dstripbooks&field-keywords=Topology&Go.x=10&Go.y=10");

                easy.SetOpt(CURLoption.CURLOPT_USERAGENT,
                    "Mozilla 4.0 (compatible; MSIE 6.0; Win32");
                easy.SetOpt(CURLoption.CURLOPT_FOLLOWLOCATION, true);
                easy.SetOpt(CURLoption.CURLOPT_URL,
                    "http://www.amazon.com/exec/obidos/search-handle-form/002-5928901-6229641");
                easy.SetOpt(CURLoption.CURLOPT_POST, true);

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

    }
}