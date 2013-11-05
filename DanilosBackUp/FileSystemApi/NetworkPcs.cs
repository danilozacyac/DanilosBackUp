using System;
using System.Linq;

namespace DanilosBackUp.FileSystemApi
{
    public class NetworkPcs
    {
        private String networkPcName;

        public NetworkPcs(String networkPcName)
        {
            this.networkPcName = networkPcName;
        }

        public String NetworkPcName
        {
            get
            {
                return this.networkPcName;
            }
            set
            {
                this.networkPcName = value;
            }
        }
    }
}
