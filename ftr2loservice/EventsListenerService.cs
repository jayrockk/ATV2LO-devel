/*
 *	Copyright (C) 2007-2010 ForTheRecord
 *	http://www.4therecord.eu
 *
 *  This Program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2, or (at your option)
 *  any later version.
 *
 *  This Program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with GNU Make; see the file COPYING.  If not, write to
 *  the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA.
 *  http://www.gnu.org/copyleft/gpl.html
 *
 */
using System;
using System.ServiceModel;
using ForTheRecord.Client.Common;
using ForTheRecord.ServiceContracts.Events;
using FTR2LO_Log;

namespace ftr2loservice
{
    [ServiceBehavior(
#if DEBUG
        IncludeExceptionDetailInFaults = true,
#endif
ConcurrencyMode = ConcurrencyMode.Single,
        InstanceContextMode = InstanceContextMode.Single)]
    public class EventsListenerService : EventsListenerServiceBase
    {
        private string _modulename = "Events";

        private static FTR2LO _ftr2lo;

        public static FTR2LO Ftr2lo
        {
            set { _ftr2lo = value; }
        }

        public static ServiceHost CreateServiceHost(string eventsServiceBaseUrl)
        {
            return CreateServiceHost(typeof(EventsListenerService), eventsServiceBaseUrl, typeof(IRecordingEventsListener));
        }

        public override void NewGuideData()
        {
            if (_ftr2lo != null)
            {
                FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "GuideEvents: " + String.Format("NewGuideData() called !"));
            }
        }

        public override void UpcomingRecordingsChanged()
        {
            if (_ftr2lo != null)
            {
                FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "RecordingEvents: UpcomingRecordingsChanged() called !");
                _ftr2lo.ftr2lo_main();
            }
        }

        public override void ActiveRecordingsChanged()
        {
            if (_ftr2lo != null)
            {
                FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "RecordingEvents: ActiveRecordingsChanged() called !");
                _ftr2lo.ftr2lo_main();
            }
        }

        public override void RecordingStarted(ForTheRecord.Entities.Recording recording)
        {
            if (_ftr2lo != null)
            {
                FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "RecordingEvents: RecordingStarted() called !");
            }
        }

        public override void RecordingEnded(ForTheRecord.Entities.Recording recording)
        {
            if (_ftr2lo != null)
            {
                FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "RecordingEvents: RecordingEnded() called !");
            }
        }

        public override void UpcomingAlertsChanged()
        {
            if (_ftr2lo != null)
            {
                FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "SystemEvents: UpcomingAlertsChanged() called !");
            }
        }

        public override void UpcomingSuggestionsChanged()
        {
            if (_ftr2lo != null)
            {
                FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "SystemEvents: UpcomingSuggestionsChanged() called !");
            }
        }

        public override void ScheduleChanged(Guid scheduleId)
        {
            if (_ftr2lo != null)
            {
                FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "ScheduleChanged: " + String.Format("Schedule with id {0} changed !", scheduleId));
            }
        }

        public override void EnteringStandby()
        {
            if (_ftr2lo != null)
            {
                FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "SystemEvents: EnteringStandby() called !");
            }
        }

        public override void SystemResumed()
        {
            if (_ftr2lo != null)
            {
                FTR2LO_Log.FTR2LO_log.do_log(_modulename, (int)FTR2LO_log.LogLevel.DEBUG, "SystemEvents: SystemResumed() called !");
            }
        }
    }
}
