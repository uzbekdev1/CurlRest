using CurlRest;
using System;

namespace samples
{
    class VersionData
    {
        public static void Run(string[] args)
        {
            try
            {
                Curl.GlobalInit((int)CURLinitFlag.CURL_GLOBAL_ALL);

                VersionInfoData vd = Curl.GetVersionInfo(CURLversion.CURLVERSION_NOW);
                Console.WriteLine("           Age: {0}", vd.Age);
                Console.WriteLine("Version String: {0}", vd.Version);
                Console.WriteLine("Version Number: {0}", vd.VersionNum);
                Console.WriteLine("   Host System: {0}", vd.Host);
                Console.WriteLine("Feature Bitmap: {0}", vd.Features);
                Console.WriteLine("   SSL Version: {0}", vd.SSLVersion);
                Console.WriteLine("SSL VersionNum: {0}", vd.SSLVersionNum);
                Console.WriteLine("  LibZ Version: {0}", vd.LibZVersion);
                Console.WriteLine("  ARES Version: {0}", vd.ARes);
                Console.WriteLine("  ARES Ver Num: {0}", vd.AResNum);
                Console.WriteLine("LibIDN Version: {0}", vd.LibIDN);
                Console.WriteLine();
                Console.WriteLine("Protocols:");
                string[] protocols = vd.Protocols;
                foreach (string prot in protocols)
                    Console.WriteLine("  {0}", prot);

                Curl.GlobalCleanup();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}