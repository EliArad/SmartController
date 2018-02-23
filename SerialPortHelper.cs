using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartRelayControllerApi
{
    public class SerialPortHelper
    {
        SerialPort m_serialPort = new SerialPort();
        byte[] m_buffer = new byte[50];
        public bool Open(string ComPort, int BaudRate)
        {
            lock (this)
            {
                try
                {
                    if (m_serialPort.IsOpen == true)
                    {
                        m_serialPort.Close();
                    }
                    m_serialPort.PortName = ComPort;
                    m_serialPort.BaudRate = BaudRate;
                    m_serialPort.Parity = Parity.None;
                    m_serialPort.DataBits = 8;
                    m_serialPort.StopBits = StopBits.One;
                    m_serialPort.Handshake = Handshake.None;


                    m_serialPort.Open();
                    return m_serialPort.IsOpen;
                }
                catch (Exception err)
                {
                    return false;
                }
            }
        }
        public void Close()
        {
            lock (this)
            {
                if (m_serialPort != null)
                    m_serialPort.Close();
            }
        }
        public void Write(byte data)
        {
            lock (this)
            {
                byte[] b = { data };
                m_serialPort.Write(b, 0, 1);
            }
        }
        public void Write(byte [] data)
        {
            lock (this)
            {
                m_serialPort.Write(data, 0, data.Length);
            }
        }
        public void Write(byte[] data, int offset, int size)
        {
            lock (this)
            {
                m_serialPort.Write(data, offset, size);
            }
        }
        public void Write(char data)
        {
            lock (this)
            {
                char[] b = { data };
                m_serialPort.Write(b, 0, 1);
            }
        }
        public void Write(char [] data)
        {
            lock (this)
            {
                m_serialPort.Write(data, 0, data.Length);
            }
        }

        public void Write(string data)
        {
            lock (this)
            {
                m_serialPort.Write(data);
            }
        }

        public string Read(out int size)
        {

            lock (this)
            {
                int timeOut = 20;
                size = m_serialPort.BytesToRead;
                while (size == 0)
                {
                    size = m_serialPort.BytesToRead;
                    Thread.Sleep(10);
                    timeOut--;
                    if (timeOut == 0)
                        return "Failed to read - time out";
                }

                if (size > 0)
                {
                    Array.Clear(m_buffer, 0, m_buffer.Length);
                    m_serialPort.Read(m_buffer, 0, size);
                    var str = System.Text.Encoding.Default.GetString(m_buffer);
                    if (str.Contains("OK"))
                    {
                        return "ok";
                    }
                    if (str.Contains("INVALID COMMAND"))
                    {
                        return "failed - invalid command";
                    }
                }


                return "failed";
            }
        }
    }
}
