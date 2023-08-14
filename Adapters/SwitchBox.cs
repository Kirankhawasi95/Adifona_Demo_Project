using System;
using System.Threading.Tasks;
using System.ComponentModel;
using HorusUITest.Data;
using HorusUITest.Helper;
using NUnit.Framework.Constraints;


namespace HorusUITest.Adapters
{
    //public static class SwitchBox -->alte Deklaration, seit sendcounter verwendet wird ->Verzicht auf "static" Schlüsselwort
    public class SwitchBox
    {
        const char enable = 'U';
        const char disable = 'D';
        const char query = 'Q';
        const char wildcard = '*';
        const char terminator = '\r';
        int sendcounter = 0;
        public static UART getUART = new UART();
        /// <summary>
        /// Sends the given <paramref name="command"/>.
        /// </summary>
        /// <param name="command">A 4 byte long string consisting of the desired status character, followed by a 2 byte long selector and a designated terminating character.</param>
        /// <returns>The acknowledgement message of the switch box.</returns>
        private static void SendCommand(string command)
        {
            getUART.WriteMessage(command);
        }

        /// <summary>
        /// Sets the enabled / disabled status of a hearing aid.
        /// </summary>
        /// <param name="command">A 4 byte long string consisting of the desired status character, followed by a 2 byte long selector and a designated terminating character.</param>
        private static void SetHearingAidStatus(string command)
        {
            try
            {
                SendCommand(command);
                string acknowledgement = getUART.ReadResponse();
                if (acknowledgement != command)
                    throw new Exception($"The switch box didn't acknowledge the command. Command was '{command}', but acknowledgement was '{acknowledgement}'.");

                else if (acknowledgement == null)
                    Output.Immediately("Received empty response on COM port");

                else
                    Output.Immediately("Required switch chanel setting successful!");
            }
            catch
            {
                throw new Exception("Expected state could not be set.");
            }

        }

        /// <summary>
        /// Returns whether or not the hearing aid given by <paramref name="selector"/> is turned on.
        /// </summary>
        /// <param name="selector">A 2 byte long description of where the hearing aid is physically connected to, e.g. "A3".</param>
        /// <returns><see cref="true"/> if the hearing aid is enabled, <see cref="false"/> otherwise.</returns>
        public static bool GetIsHearingAidEnabled(string selector)
        {
            string statusRequest = query + selector + terminator;

            SendCommand(statusRequest);
            string acknowledgement = getUART.ReadResponse();
            if (acknowledgement == statusRequest)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Enables the hearing aid given by <paramref name="selector"/>,
        /// </summary>
        /// <param name="selector">A 2 byte long description of where the hearing aid is physically connected to, e.g. "A3".</param>
        public static void EnableHearingAid(string selector)
        {
            //selector needs to be defined in "Configuration/SelectHearingAid" for each device
            SetHearingAidStatus(enable + selector + terminator);
        }

        /// <summary>
        /// Disables the hearing aid given by <paramref name="selector"/>,
        /// </summary>
        /// <param name="selector">A 2 byte long description of where the hearing aid is physically connected to, e.g. "A3".</param>
        public static void DisableHearingAid(string selector)
        {
            //selector needs to be defined in "Configuration/SelectHearingAid" for each device
            SetHearingAidStatus(disable + selector + terminator);
        }

        /// <summary>
        /// Returns whether or not the given hearing aid is turned on.
        /// </summary>
        /// <returns><see cref="true"/> if the hearing aid is turned on, <see cref="false"/> otherwise.</returns>
        public static bool GetIsHearingAidEnabled(HearingAid hearingAid)
        {
            try
            {
                return GetIsHearingAidEnabled(hearingAid.Selector);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get the hearing aid status of {hearingAid}", ex);
            }
        }

        /// <summary>
        /// Turns on the given hearing aid.
        /// </summary>
        public static void EnableHearingAid(HearingAid hearingAid)
        {
            try
            {
                EnableHearingAid(hearingAid.Selector);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to enable the hearing aid {hearingAid}", ex);
            }
        }

        /// <summary>
        /// Turns off the given hearing aid.
        /// </summary>
        public static void DisableHearingAid(HearingAid hearingAid)
        {
            try
            {
                DisableHearingAid(hearingAid.Selector);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to disable the hearing aid {hearingAid}", ex);
            }
        }

        /// <summary>
        /// Turns off all hearing aids.
        /// </summary>
        public void DisableAll()
        {
            string disableAll = "R**" + "\r";
            try
            {
                //Send Command to disable all Devices
                SendCommand(disableAll);

                //Get acknowledgement as 'ReadResponse' feedback. Check if switch respond with message that has been send
                string acknowledgement = getUART.ReadResponse();
                if (acknowledgement != disableAll)
                {
                    Output.Immediately("Could not disable all hearing aids.");
                    if (sendcounter < 2)
                    {
                        Output.Immediately("Try to send message again.");
                        DisableAll();
                    }
                }
                else
                {
                    Output.Immediately("All hearing aids has been successfully turned off.");
                    sendcounter = 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to disable all hearing aids. Please check state of COM port", ex);

            }
        }
    }
}
