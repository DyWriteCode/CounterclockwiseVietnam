using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoMsg
{
    public class PBConfig: Singleton<PBConfig>
    {
       // S server  C client
        public  Dictionary<int, Type> PBC2SDic = new Dictionary<int, Type>();
        public  Dictionary<int, Type> PBS2CDic = new Dictionary<int, Type>();

        /// <summary>
        /// 初始化 两个字典
        /// </summary>
        public PBConfig()
        {
            //工具自动化生成
            C2SmsgInit();
            S2CmsgInit();
        }

        void C2SmsgInit() {
            //PBC2SDic.Add(10001,typeof(UserCMDC2S)
        }

        void S2CmsgInit() {
            //PBS2CDic.Add(15,typeof(HeartbeatS2C)
        }
    }
}