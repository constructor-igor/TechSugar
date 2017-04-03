using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloworld_infra
{
    public class OsplConfiguration
    {
        public static void SetOsplConfiguration()
        {
            string osplHome = @"D:\@Temp\DDS\OpenSpliceDDSV6.4.140407OSS-HDE-x86_64.win-vs2013-installer\HDE\x86_64.win64";
            string pathVariable = Environment.GetEnvironmentVariable("PATH");
            Environment.SetEnvironmentVariable("SPLICE_ORB", "DDS_OpenFusion_1_6_1");
            Environment.SetEnvironmentVariable("OSPL_HOME", osplHome);
            Environment.SetEnvironmentVariable("PATH", String.Format(@"{0}\bin;{0}\lib;{0}\examples\lib;{1}", osplHome, pathVariable));
            Environment.SetEnvironmentVariable("OSPL_TMPL_PATH", String.Format(@"{0}\etc\idlpp", osplHome));
            Environment.SetEnvironmentVariable("OSPL_URI", String.Format(@"file://{0}\etc\config\ospl.xml", osplHome));
        }
    }
}
