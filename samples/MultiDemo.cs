using CurlRest;
using System;

namespace samples
{
    class MultiDemo
    {
        public static void Run(string[] args)
        {
            try
            {
                Curl.GlobalInit((int)CURLinitFlag.CURL_GLOBAL_ALL);

                Easy.WriteFunction wf = new Easy.WriteFunction(OnWriteData);

                Easy easy1 = new Easy();
                string s1 = (string)args[0].Clone();
                easy1.SetOpt(CURLoption.CURLOPT_URL, args[0]);
                easy1.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, wf);
                easy1.SetOpt(CURLoption.CURLOPT_WRITEDATA, s1);

                Easy easy2 = new Easy();
                string s2 = (string)args[1].Clone();
                easy2.SetOpt(CURLoption.CURLOPT_URL, args[1]);
                easy2.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, wf);
                easy2.SetOpt(CURLoption.CURLOPT_WRITEDATA, s2);

                Multi multi = new Multi();
                multi.AddHandle(easy1);
                multi.AddHandle(easy2);

                int stillRunning = 1;
                // call Multi.Perform right away (note ref qualifier)
                while (multi.Perform(ref stillRunning) ==
                    CURLMcode.CURLM_CALL_MULTI_PERFORM) ;

                while (stillRunning != 0)
                {
                    multi.FDSet();
                    int rc = multi.Select(1000); // one second
                    switch (rc)
                    {
                        case -1:
                            Console.WriteLine("Multi.Select() returned -1");
                            stillRunning = 0;
                            break;

                        case 0:
                        default:
                            {
                                while (multi.Perform(ref stillRunning) ==
                                    CURLMcode.CURLM_CALL_MULTI_PERFORM) ;
                                break;
                            }
                    }
                }

                // various cleanups
                multi.Cleanup();
                easy1.Cleanup();
                easy2.Cleanup();
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
            int nBytes = size * nmemb;
            Console.WriteLine("Obtained {0} bytes from {1}", nBytes, extraData);
            return nBytes;
        }

    }
}