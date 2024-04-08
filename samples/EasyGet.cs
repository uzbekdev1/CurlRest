using CurlRest;
using System;

namespace samples
{
    class EasyGet
    {
        public static void Run(string[] args)
        {
            try
            {
                Curl.GlobalInit((int)CURLinitFlag.CURL_GLOBAL_ALL);

                Easy easy = new Easy();
                Easy.WriteFunction wf = new Easy.WriteFunction(OnWriteData);

                easy.SetOpt(CURLoption.CURLOPT_URL, args[0]);
                easy.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, wf);
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