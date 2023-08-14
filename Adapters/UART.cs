using System;
using System.IO.Ports;
using HorusUITest.Helper;
using System.Threading;

namespace HorusUITest.Adapters
{
    public class UART
    {
        //define Object to control Read/Write over serial COM-Port 
        SerialPort _serialPort;


        private string initializePortMessage = "R**\r";
        private string lastRequest = "";

        /// <summary>
        /// Initializes the desired COM Port 'COM1' for communication to hardware switch. This must be called before any read or write operations to COM Port.
        /// </summary>
        public void InitializeUART()
        {
            var COMport = GetPorts();
            if (COMport != null)
            {
                try
                {
                    //setting up and open COM Port for communication with hearing aid switch
                    _serialPort = new SerialPort(COMport, 115200, Parity.None, 8, StopBits.One);
                    OpenComPort(_serialPort);
                    //Sending this Message is needed to Reset the hearing aif switch and make sure, that devices are turned off
                    if (_serialPort.IsOpen)
                        WriteMessage(initializePortMessage);
                    string acknowledge = ReadResponse();
                    //string test = acknowledge;
                    if (acknowledge != initializePortMessage)
                        Output.Immediately($"Hearing aid switch could not be initialized. Switch response differs from expected message {initializePortMessage} and was {acknowledge}.");
                    //Thread.Sleep(3000);
                    //WriteMessage(initializePortMessage1);
                    // acknowledge = ReadResponse();
                    //test = acknowledge;
                    //if (acknowledge != initializePortMessage1)
                    //    Output.Immediately("COM Port couldn't be initialized. Make sure that the right port is available and not used by any other application");

                    //Thread.Sleep(3000);
                    //WriteMessage(initializePortMessage2);
                    //acknowledge = ReadResponse();
                    ////Thread.Sleep(500);
                    //if (acknowledge != initializePortMessage2)
                    //    Output.Immediately("COM Port couldn't be initialized. Make sure that the right port is available and not used by any other application");

                }
                catch (Exception ex)
                {
                    throw new Exception($"Initialization of Port {_serialPort.PortName} failed because of {ex}");
                }
            }
            else
                throw new ArgumentNullException("The request Com Port was not found");
        }


        public string GetPorts()
        {
            // Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();
            Console.WriteLine("The following serial ports were found:");
            // Display each port name to the console.
            foreach (string port in ports)
            {
                Output.Immediately($"Found port: {port}\n");
                //Console.WriteLine(port);
                if (port == "/dev/tty.usbmodem0006838029121")
                    return port;
            }
            return null;
        }
        /// <summary>
        /// Reads out any data of the COM Port input buffer when available. Returns response string of the hardware switch response.
        /// </summary>
        /// <returns></returns>
        public string ReadResponse()
        {
            string responseMessage = "";
            //int numberOfByteRead; //to hold number of bytes read out of input buffer
            int totalBytes = _serialPort.BytesToRead; //check for received bytes in input buffer
            char[] response = new char[totalBytes]; //define input buffer(size)

            //read input buffer to check switch response
            if (totalBytes > 0)
            {
                try
                {
                    //the value numberOfByteRead is just to check how many bytes has been read if some error occurs
                    //numberOfByteRead = _serialPort.Read(response, 0, 4);

                    //if number of read out byte matches totalBytes detected in input buffer -> assume heaving a valid response 
                    // if (numberOfByteRead == totalBytes)
                    //{
                    responseMessage = _serialPort.ReadExisting();

                    ////THIS IS NEEDED IF INPUT BUFFER SHOULB BE READ BY '_serialPort.Read(..,..,..)'
                    //    string responseMessage1 = response[0].ToString();
                    //  string responseMessage2 = response[1].ToString();
                    // string responseMessage3 = response[2].ToString();
                    //string responseMessage4 = response[3].ToString();
                    // responseMessage = responseMessage1 + responseMessage2 + responseMessage3 + responseMessage4;
                    //////
                    ///
                    Output.Immediately($"Data response received on COM Port:{responseMessage}");
                    //       responseMessage = response.ToString();
                    return responseMessage;
                }

                catch (Exception ex)
                {
                    Output.Immediately($"Error while reading input buffer: {ex}");
                }
            }
            else
                Output.Immediately("No Data to read available in input buffer");

            return null;
        }

        /// <summary>
        /// Sends a given Message as an Array of Byte to serial port using specified buffer
        /// </summary>
        /// <param name="message"></param>
        public void WriteMessage(string message)
        {
            lastRequest = message;
            //check if COM1 is already open and waiting 
            if (_serialPort.IsOpen)
                try
                {
                    //Converting Message string to ByteArray and sends Message with expected length to switch 
                    char[] sendMessage = message.ToCharArray();
                    int ByteToSend = Buffer.ByteLength(sendMessage);

                    _serialPort.Write(message);
                    //_serialPort.Write(sendMessage, 0, ByteToSend/2);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Unable to send message {message} via {_serialPort}. Hearing aid switch didn't receive the command.", ex);
                }
        }

        /// <summary>
        /// Opens a defined COM-Port for communication
        /// </summary>
        /// <param name="port"></param>
        public void OpenComPort(SerialPort port)
        {
            try
            {
                port.Open();
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to open requested communication Port {port}.", ex);
            }
        }

        /// <summary>
        /// Releases a given COM-Port. Needs to be called when after communication over COM Port is finished.
        /// </summary>
        public void ReleaseCOMPort()
        {
            CloseComPort(_serialPort);
        }
        /// <summary>
        /// Takes a message and retries to send it to the COM Port
        /// </summary>
        public void ResendMessage(string message)
        {
            //if a request fails the solution automatically tries to resend the request
            Output.Immediately("Retry to send last switch request!");
            WriteMessage(message);
            //check response on COM 1 to be sure that resetting was successful
            string acknowledge = ReadResponse();
            if (acknowledge != message)
                Output.Immediately($"Failure while retry to send request. Expected response is: {lastRequest} but was: {acknowledge}. Make sure that the COM port is available and not used by any other application");
            if (acknowledge == null)
                Output.Immediately("Received empty response on COM port");
            else
                Output.Immediately("Message was send successful to hearing aid switch.");
        }

        /// <summary>
        /// Resets the hearing aid switch hardware in case of some error when attempting to activate any device. Needs to be called in case of switch hardware error state.
        /// </summary>
        public void ResetSwitch()
        {
            //For resetting the switch, the initialization message must be sent again. This forces the switch to release all connection.
            WriteMessage(initializePortMessage);

            //check response on COM1 to be sure that resetting was successful
            string acknowledge = ReadResponse();
            if (acknowledge != initializePortMessage)
                Output.Immediately($"Unable to Reset Switch. Expected response is: {initializePortMessage} but was: {acknowledge}. Make sure that the COM port is available and not used by any other application");
            if (acknowledge == null)
                Output.Immediately("Received empty response on COM port");
            else
                Output.Immediately("Switch reset successful!");
        }

        /// <summary>
        /// Closes the Connection to a specified COM-Port and releases all resources used by System.
        /// </summary>
        /// <param name="port"></param>
        public void CloseComPort(SerialPort port)
        {
            try
            {
                port.Close();
                port.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to close Port: {port.PortName}", ex);
            }

        }

        //Currently unused/not neccessary code, but maybe for later test
        /*public void PortEvents()
        {
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        }
        private static void DataReceivedHandler(
            object sender,
            SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            char[] response = new char[4];
            string indata = sp.Read(response, 0, 4).ToString();
            Output.Immediately($"Data Received:{response}");
            //Console.Write(indata);
           
        }*/
    }
}
