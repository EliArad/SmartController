using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRelayControllerApi
{
    public class SmartRelayControllerSim : SmartRelayController
    {

        public override bool OpenCom(string ComPort, int BaudRate)
        {
            return true;
        }
        public override bool RelayOn(int Relay)
        {
            lock (this)
            {
                return true;
            }
        }

        public override bool Relay(string command)
        {
            lock (this)
            {
                return true;
            }
        }
        public override void CloseCom()
        {
            lock (this)
            {
                
            }
        }

        public override bool RelayOff(int Relay)
        {
            lock (this)
            {
                return true;
            }
        }

        public override bool AllRelayOff()
        {
            lock (this)
            {                    
                return true;                
            }
        }
        public override bool AllRelayOn()
        {
            lock (this)
            {
                return true;
            }
        }
    }
}
