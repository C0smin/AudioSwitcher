﻿/*
  LICENSE
  -------
  Copyright (C) 2007 Ray Molenkamp

  This source code is provided 'as-is', without any express or implied
  warranty.  In no event will the authors be held liable for any damages
  arising from the use of this source code or the software it produces.

  Permission is granted to anyone to use this source code for any purpose,
  including commercial applications, and to alter it and redistribute it
  freely, subject to the following restrictions:

  1. The origin of this source code must not be misrepresented; you must not
     claim that you wrote the original source code.  If you use this source code
     in a product, an acknowledgment in the product documentation would be
     appreciated but is not required.
  2. Altered source versions must be plainly marked as such, and must not be
     misrepresented as being the original source code.
  3. This notice may not be removed or altered from any source distribution.
*/

using System.Runtime.InteropServices;
using AudioSwitcher.AudioApi.CoreAudio.Interfaces;

namespace AudioSwitcher.AudioApi.CoreAudio
{
    /// <summary>
    ///     Audio Meter Information
    /// </summary>
    internal class AudioMeterInformation
    {
        private readonly IAudioMeterInformation _audioMeterInformation;
        private readonly AudioMeterInformationChannels _channels;
        private readonly EndpointHardwareSupport _hardwareSupport;

        internal AudioMeterInformation(IAudioMeterInformation realInterface)
        {
            uint hardwareSupp;

            _audioMeterInformation = realInterface;
            Marshal.ThrowExceptionForHR(_audioMeterInformation.QueryHardwareSupport(out hardwareSupp));
            _hardwareSupport = (EndpointHardwareSupport) hardwareSupp;
            _channels = new AudioMeterInformationChannels(_audioMeterInformation);
        }

        /// <summary>
        ///     Peak Values
        /// </summary>
        public AudioMeterInformationChannels PeakValues
        {
            get { return _channels; }
        }

        /// <summary>
        ///     Hardware Support
        /// </summary>
        public EndpointHardwareSupport HardwareSupport
        {
            get { return _hardwareSupport; }
        }

        /// <summary>
        ///     Master Peak Value
        /// </summary>
        public float MasterPeakValue
        {
            get
            {
                float result;
                Marshal.ThrowExceptionForHR(_audioMeterInformation.GetPeakValue(out result));
                return result;
            }
        }
    }
}