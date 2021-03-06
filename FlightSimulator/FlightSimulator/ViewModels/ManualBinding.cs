﻿using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator
{
    class ManualBinding : INotifyPropertyChanged
    {
        double elevator;
        double aileron;
        double rudder;
        double throttle;

        double prev_rudder;
        double prev_throttle;
        double prev_elevator;
        double prev_aileron;

        CmdConnection sc;

        const double MIN_RUDDER_CHANGE = 0.01;
        const double MIN_THROTTLE_CHANGE = 0.01;
        const double MIN_ELEVATOR_CHANGE = 0.01;
        const double MIN_AILERON_CHANGE = 0.01;

        public ManualBinding(CmdConnection sc)
        {
            this.sc = sc;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public double Elevator
        {
            get
            {
                return elevator;
            }
            set
            {
                elevator = value;
                if (Math.Abs(elevator - prev_rudder) < MIN_ELEVATOR_CHANGE)
                {
                    return;
                }
                prev_elevator = value;
                sc.SendString("/controls/flight/elevator", elevator);
                OnPropertyChanged("ElevatorString");
                OnPropertyChanged("Elevator");
            }
        }
        
        public double Aileron
        {
            get
            {
                return aileron;
            }
            set
            {
                aileron = value;
                if (Math.Abs(aileron - prev_rudder) < MIN_AILERON_CHANGE)
                {
                    return;
                }
                prev_aileron = value;
                sc.SendString("/controls/flight/aileron", aileron);
                OnPropertyChanged("AileronString");
                OnPropertyChanged("Aileron");
            }
        }

        public double Rudder
        {
            get
            {
                return rudder;
            }
            set
            {
                rudder = value;
                if (Math.Abs(rudder - prev_rudder) < MIN_RUDDER_CHANGE)
                {
                    return;
                }
                prev_rudder = value;
                sc.SendString("/controls/flight/rudder", rudder);
                OnPropertyChanged("RudderString");
                OnPropertyChanged("Rudder");
            }
        }

        public double Throttle
        {
            get
            {
                return throttle;
            }

            set
            {
                throttle = value;
                if (Math.Abs(throttle - prev_throttle) < MIN_THROTTLE_CHANGE)
                {
                    return;
                }
                prev_throttle = value;
                sc.SendString("/controls/engines/current-engine/throttle", throttle);
                OnPropertyChanged("ThrottleString");
                OnPropertyChanged("Throttle");
            }
        }

        public string ElevatorString
        {
            get
            {
                return VarStringFactoring(elevator);
            }
        }
        
        public string AileronString
        {
            get
            {
                return VarStringFactoring(aileron);
            }
        }

        public string RudderString
        {
            get
            {
                return VarStringFactoring(rudder);
            }
        }

        public string ThrottleString
        {
            get
            {
                return VarStringFactoring(throttle);
            }
        }

        public string VarStringFactoring(double var)
        {
            double intermediate = Math.Truncate(var * 100) / 100;
            return String.Format("{0:N2}", intermediate);
        }

        public static double MIN_THROTTLE_CHANGE1 => MIN_THROTTLE_CHANGE;

    }
}
