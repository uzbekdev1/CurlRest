using CurlRest;
using System;

namespace samples
{
    class FileUpload
    {
        public static void Run(string[] args)
        {
            try
            {
                Curl.GlobalInit((int)CURLinitFlag.CURL_GLOBAL_ALL);

                // <form action="http://mybox/cgi-bin/myscript.cgi
                //  method="post" enctype="multipart/form-data">
                MultiPartForm mf = new MultiPartForm();

                // <input name="frmUsername">
                mf.AddSection(CURLformoption.CURLFORM_COPYNAME, "frmUsername",
                    CURLformoption.CURLFORM_COPYCONTENTS, "testtcc",
                    CURLformoption.CURLFORM_END);

                // <input name="frmPassword">
                mf.AddSection(CURLformoption.CURLFORM_COPYNAME, "frmPassword",
                    CURLformoption.CURLFORM_COPYCONTENTS, "tcc",
                    CURLformoption.CURLFORM_END);

                // <input name="frmFileOrigPath">
                mf.AddSection(CURLformoption.CURLFORM_COPYNAME, "frmFileOrigPath",
                    CURLformoption.CURLFORM_COPYCONTENTS, args[1],
                    CURLformoption.CURLFORM_END);

                // <input name="frmFileDate">
                mf.AddSection(CURLformoption.CURLFORM_COPYNAME, "frmFileDate",
                    CURLformoption.CURLFORM_COPYCONTENTS, "08/01/2004",
                    CURLformoption.CURLFORM_END);

                // <input type="File" name="f1">
                mf.AddSection(CURLformoption.CURLFORM_COPYNAME, "f1",
                    CURLformoption.CURLFORM_FILE, args[1],
                    CURLformoption.CURLFORM_CONTENTTYPE, "application/binary",
                    CURLformoption.CURLFORM_END);

                Easy easy = new Easy();

                Easy.DebugFunction df = new Easy.DebugFunction(OnDebug);
                easy.SetOpt(CURLoption.CURLOPT_DEBUGFUNCTION, df);
                easy.SetOpt(CURLoption.CURLOPT_VERBOSE, true);

                Easy.ProgressFunction pf = new Easy.ProgressFunction(OnProgress);
                easy.SetOpt(CURLoption.CURLOPT_PROGRESSFUNCTION, pf);

                easy.SetOpt(CURLoption.CURLOPT_URL, args[0]);
                easy.SetOpt(CURLoption.CURLOPT_HTTPPOST, mf);

                easy.Perform();
                easy.Cleanup();
                mf.Free();

                Curl.GlobalCleanup();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void OnDebug(CURLINFOTYPE infoType, string msg,
            object extraData)
        {
            // print out received data only
            if (infoType == CURLINFOTYPE.CURLINFO_DATA_IN)
                Console.WriteLine(msg);
        }


        public static int OnProgress(object extraData, double dlTotal,
            double dlNow, double ulTotal, double ulNow)
        {
            Console.WriteLine("Progress: {0} {1} {2} {3}",
                dlTotal, dlNow, ulTotal, ulNow);
            return 0; // standard return from PROGRESSFUNCTION
        }
    }
}