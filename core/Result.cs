using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace splatoon3Tester.core
{
    public class Result
    {
        private int mod;
        private int desc;

        public Result(int mod, int desc)
        {
            this.mod = mod;
            this.desc = desc;
        }

        public int getModule()
        {
            return mod;
        }

        public int getDesc()
        {
            return desc;
        }

        public bool failed()
        {
            return mod != 0 || desc != 0;
        }

        public bool succeeded()
        {
            return mod == 0 && desc == 0;
        }

        public String message()
        {
            return "Module:" + mod + " Code:" + desc;
        }

        public override String ToString()
        {
            return "Result{" +
                    "mod=" + mod +
                    ", desc=" + desc +
                    '}';
        }

        public static Result valueOf(int rc)
        {
            return new Result(module(rc), description(rc));
        }

        public static bool failed(int rc)
        {
            return rc != 0;
        }

        public static bool succeeded(int rc)
        {
            return rc == 0;
        }

        public static int module(int rc)
        {
            return rc & 0x1FF;
        }

        public static int description(int rc)
        {
            return (rc >> 9) & 0x1FFF;
        }
    }
}
