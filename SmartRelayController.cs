using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRelayControllerApi
{
    public class SmartRelayController : SerialPortHelper
    {

        public string m_lastError;
        public virtual bool OpenCom(string ComPort, int BaudRate)
        {
            return Open(ComPort, BaudRate);
        }
        public virtual bool RelayOn(int Relay)
        {
            lock (this)
            {
                if (Relay < 0 || Relay > 7)
                    return false;
                //string cmd = string.Format("O{0}=1" + '\r' , Relay);
                 
                string cmd = string.Empty;
                switch (Relay)
                {
                    case 0:
                        cmd = "OA=XXXXXXX1" + '\r';
                        break;
                    case 1:
                        cmd = "OA=XXXXXX1X" + '\r';
                        break;
                    case 2:
                        cmd = "OA=XXXXX1XX" + '\r';
                        break;
                    case 3:
                        cmd = "OA=XXXX1XXX" + '\r';
                        break;
                    case 4:
                        cmd = "OA=XXX1XXXX" + '\r';
                        break;
                    case 5:
                        cmd = "OA=XX1XXXXX" + '\r';
                        break;
                    case 6:
                        cmd = "OA=X1XXXXXX" + '\r';
                        break;
                    case 7:
                        cmd = "OA=1XXXXXXX" + '\r';
                        break;

                }
                       
                Write(cmd);

                int size;
                if ((m_lastError = Read(out size)) == "ok")
                    return true;
                return false;
            }
        }

        public virtual bool Relay(string command)
        {
            lock (this)
            {

                string cmd = string.Empty;
                cmd = "OA=" + command + '\r';

                Write(cmd);

                int size;
                if ((m_lastError = Read(out size)) == "ok")
                    return true;
                return false;
            }
        }


        public virtual bool RelayOff(int Relay)
        {
            lock (this)
            {
                if (Relay < 0 || Relay > 7)
                    return false;
                //string cmd = string.Format("O{0}=0'\r'", Relay);
                 
                string cmd = string.Empty;
                switch (Relay)
                {
                    case 0:
                        cmd = "OA=XXXXXXX0" + '\r';
                        break;
                    case 1:
                        cmd = "OA=XXXXXX0X" + '\r';
                        break;
                    case 2:
                        cmd = "OA=XXXXX0XX" + '\r';
                        break;
                    case 3:
                        cmd = "OA=XXXX0XXX" + '\r';
                        break;
                    case 4:
                        cmd = "OA=XXX0XXXX" + '\r';
                        break;
                    case 5:
                        cmd = "OA=XX0XXXXX" + '\r';
                        break;
                    case 6:
                        cmd = "OA=X0XXXXXX" + '\r';
                        break;
                    case 7:
                        cmd = "OA=0XXXXXXX" + '\r';
                    break;

                }
                

                Write(cmd);

                int size;
                if ((m_lastError = Read(out size)) == "ok")
                    return true;
                return false;
            }
        }

        public virtual void CloseCom()
        {
            lock (this)
            {                             
                Close();
            }
        }

        public virtual bool AllRelayOff()
        {
            lock (this)
            {
                string cmd = "OA=00000000" + '\r';

                Write(cmd);

                int size;
                if ((m_lastError = Read(out size)) == "ok")
                    return true;
                return false;
 
            }
        }
        public virtual bool AllRelayOn()
        {
            lock (this)
            {
                string cmd = "OA=11111111" + '\r';

                Write(cmd);

                int size;
                if ((m_lastError = Read(out size)) == "ok")
                    return true;
                return false;

            }
        }
    }
}
