using System;

namespace BabySitterKataSimple
{
    public class BabySitterFeeCalculator
    {
        private TimeConverter _timeConverter;

        private const double MIDNIGHT = 24.0;
        private const double LASTENDTIME = 4.0;
        private const double EARLIESTSTARTTIME = 17.0;

        private const int BEFOREBEDRATE = 12;
        private const int AFTERBEDRATE = 8;
        private const int AFTERMIDRATE = 16;

        private double _doubleStartTime;
        private double _doubleEndTime;
        private double _doubleBedTime;

        private double doubleStartToBedFee = 0.0;
        private double doubleBedToMidFee = 0.0;
        private double doubleMidToEndFee = 0.0;

        private double _doubleFee;
        private int _fee;

        private enum Type
        {
            StartAndEndAfterMid,
            BedtimeBeforeStartOrEqualStartBothBeforeMidEndBeforeMid,
            BedtimeBeforeOrEqualStartBothBeforeMidEndAfterMid,
            StartBeforeMidBedtimeEqualsOrAfterEndAndEndBeforeMid,
            StartBeforeOrAtMidBedAndEndAfterMid,
            StartBeforeBedBothBeforeMidEndBeforeMid,
            StartBeforeBedBothBeforeMidEndAfterMid,
            defaultType
        }
        public BabySitterFeeCalculator()
        {
            _timeConverter = new TimeConverter();
        }
        private bool CheckStartTime(double doubleStartTime)
        {
            if ((doubleStartTime >= EARLIESTSTARTTIME || doubleStartTime < LASTENDTIME) && doubleStartTime - Math.Floor(doubleStartTime) == 0)
                return true;
            return false;
        }
        private bool CheckEndTime(double doubleEndTime, double doubleStartTime)
        {
            if ((doubleEndTime - Math.Floor(doubleEndTime) == 0) && (doubleEndTime <= LASTENDTIME || doubleEndTime > EARLIESTSTARTTIME) && doubleEndTime != doubleStartTime)
                return true;
            return false;
        }
        private bool CheckIsNoon(string time)
        {
            if (time.Equals("12:00PM"))
            {
                return true;
            }
            return false;
        }
        private double CheckAndAssignStartTime(string startTime)
        {
            double doubleStartTime = 0.0;
            doubleStartTime = _timeConverter.ConvertTimeString(startTime);
            if (!CheckStartTime(doubleStartTime) || CheckIsNoon(startTime))
            {
                throw new System.ArgumentException("Start time entered is incorrect.", startTime);
            }
            else
            return doubleStartTime;
        }
        private double CheckAndAssignEndTime(string endTime, double doubleStartTime)
        {
            double doubleEndTime = 0.0;
            doubleEndTime = _timeConverter.ConvertTimeString(endTime);
            if (!CheckEndTime(doubleEndTime, doubleStartTime) || CheckIsNoon(endTime))
            {
                throw new System.ArgumentException("End time entered is incorrect.", endTime);
            }
            else
                return doubleEndTime;
        }
        private void CalculateFracHoursStartAndEndTime(double doubleStartTime, double doubleEndTime)
        {
            _doubleStartTime = _timeConverter.ConvertRawTimeDoubleToFractionalHours(doubleStartTime);
            _doubleEndTime = _timeConverter.ConvertRawTimeDoubleToFractionalHours(doubleEndTime);
        }
        private void CalculateRateStartAndEndAfterMid(double doubleStartTime, double doubleEndTime)
        {
            CalculateFracHoursStartAndEndTime(doubleStartTime, doubleEndTime);
            if (_doubleStartTime < _doubleEndTime)
            {
                _doubleFee = _doubleEndTime - _doubleStartTime;
                _fee = Convert.ToInt32(_doubleFee) * AFTERMIDRATE;
            }
            else if (_doubleStartTime > _doubleEndTime)
            {
                _doubleFee = _doubleEndTime - (_doubleStartTime - MIDNIGHT);
                _fee = Convert.ToInt32(_doubleFee) * AFTERMIDRATE;
            }
        }
        private void CalculateRateBedtimeBeforeStartOrEqualStartBothBeforeMidEndBeforeMid(double doubleStartTime, double doubleEndTime)
        {
            CalculateFracHoursStartAndEndTime(doubleStartTime, doubleEndTime);
            _doubleFee = _doubleEndTime - _doubleStartTime;
            _fee = Convert.ToInt32(_doubleFee) * AFTERBEDRATE;
        }
        private void CalculateRateBedtimeBeforeOrEqualStartBothBeforeMidEndAfterMid(double doubleStartTime, double doubleEndTime)
        {
            CalculateFracHoursStartAndEndTime(doubleStartTime, doubleEndTime);

                _doubleFee = MIDNIGHT - _doubleStartTime;
                _fee = Convert.ToInt32(_doubleFee) * AFTERBEDRATE;
                _doubleFee = _doubleEndTime;
                _fee = _fee + Convert.ToInt32(_doubleFee) * AFTERMIDRATE;
                return;
        }
        private void CalculateRateStartBeforeMidBedtimeEqualsOrAfterEndAndEndBeforeMid(double doubleStartTime, double doubleEndTime)
        {
            CalculateFracHoursStartAndEndTime(doubleStartTime, doubleEndTime);
            _doubleFee = _doubleEndTime - _doubleStartTime;
            _fee = Convert.ToInt32(_doubleFee) * BEFOREBEDRATE;
        }
        private void CalculateRateStartBeforeOrAtMidBedAndEndAfterMid(double doubleStartTime, double doubleEndTime)
        {
            CalculateFracHoursStartAndEndTime(doubleStartTime, doubleEndTime);

                _doubleFee = MIDNIGHT - _doubleStartTime;
                _fee = Convert.ToInt32(_doubleFee) * BEFOREBEDRATE;
                _doubleFee = _doubleEndTime;
                _fee = _fee + Convert.ToInt32(_doubleFee) * AFTERMIDRATE;
                return;
        }
        private void CalculateRateStartBeforeBedBothBeforeMidEndBeforeMid(double doubleStartTime, double doubleBedTime, double doubleEndTime)
        {
            CalculateFracHoursStartAndEndTime(doubleStartTime, doubleEndTime);
            _doubleBedTime = _timeConverter.ConvertRawTimeDoubleToFractionalHours(doubleBedTime);
            int feeStartToBed = 0;
            int feeBedToEnd = 0;
            double doubleStartToBedFee = 0.0;
            double doubleBedToEndFee = 0.0;
                doubleStartToBedFee = _doubleBedTime - _doubleStartTime;
                feeStartToBed = Convert.ToInt32(doubleStartToBedFee) * BEFOREBEDRATE;
                doubleBedToEndFee = _doubleEndTime - _doubleBedTime;
                feeBedToEnd = Convert.ToInt32(Math.Floor(doubleBedToEndFee)) * AFTERBEDRATE;
                _fee = feeBedToEnd + feeStartToBed;
                return;
        }
        private void CalculateRateStartBeforeBedBothBeforeMidEndAfterMid(double doubleStartTime, double doubleBedTime, double doubleEndTime)
        {
            CalculateFracHoursStartAndEndTime(doubleStartTime, doubleEndTime);
            _doubleBedTime = _timeConverter.ConvertRawTimeDoubleToFractionalHours(doubleBedTime);
            doubleStartToBedFee = (_doubleBedTime - _doubleStartTime) * BEFOREBEDRATE;
            doubleBedToMidFee = (MIDNIGHT - _doubleBedTime) * AFTERBEDRATE;
            doubleMidToEndFee = _doubleEndTime * AFTERMIDRATE;
            _doubleFee = doubleStartToBedFee + doubleBedToMidFee + doubleMidToEndFee;
            _fee = Convert.ToInt32(_doubleFee);
        }
        public int CalculateRate(string startTime, string bedTime, string endTime)
        {
            double doubleStartTime = 0;
            double doubleEndTime = 0;
            double doubleBedTime = 0;
            try
            {
                doubleStartTime = CheckAndAssignStartTime(startTime);
                doubleEndTime = CheckAndAssignEndTime(endTime, doubleStartTime);
                doubleBedTime = _timeConverter.ConvertTimeString(bedTime);
                if(doubleBedTime - Math.Floor(doubleBedTime) != 0)
                {
                    throw new System.ArgumentException("Bed time entered is incorrect.", bedTime);
                }
            }
            catch(ArgumentException ex)
            {
                return 0;
            }
            Type type = DetermineType(doubleStartTime, doubleBedTime, doubleEndTime);
            switch (type)
            {
                case Type.StartAndEndAfterMid:
                    {
                        CalculateRateStartAndEndAfterMid(doubleStartTime, doubleEndTime);
                        break;
                    }
                case Type.BedtimeBeforeStartOrEqualStartBothBeforeMidEndBeforeMid:
                    {
                        CalculateRateBedtimeBeforeStartOrEqualStartBothBeforeMidEndBeforeMid(doubleStartTime, doubleEndTime);
                        break;
                    }
                case Type.BedtimeBeforeOrEqualStartBothBeforeMidEndAfterMid:
                    {
                        CalculateRateBedtimeBeforeOrEqualStartBothBeforeMidEndAfterMid(doubleStartTime, doubleEndTime);
                        break;
                    }
                case Type.StartBeforeMidBedtimeEqualsOrAfterEndAndEndBeforeMid:
                    {
                        CalculateRateStartBeforeMidBedtimeEqualsOrAfterEndAndEndBeforeMid(doubleStartTime, doubleEndTime);
                        break;
                    }
                case Type.StartBeforeOrAtMidBedAndEndAfterMid:
                    {
                        CalculateRateStartBeforeOrAtMidBedAndEndAfterMid(doubleStartTime, doubleEndTime);
                        break;
                    }
                case Type.StartBeforeBedBothBeforeMidEndBeforeMid:
                    {
                        CalculateRateStartBeforeBedBothBeforeMidEndBeforeMid(doubleStartTime, doubleBedTime, doubleEndTime);
                        break;
                    }
                case Type.StartBeforeBedBothBeforeMidEndAfterMid:
                    {
                        CalculateRateStartBeforeBedBothBeforeMidEndAfterMid(doubleStartTime, doubleBedTime, doubleEndTime);
                        break;
                    }
                default:
                    return _fee;
            }
            return _fee;
        }
        private Type DetermineType(double doubleStartTime, double doubleBedTime, double doubleEndTime)
        {
            Type type;
            if (doubleEndTime <= LASTENDTIME && (doubleStartTime < LASTENDTIME || doubleStartTime > MIDNIGHT))
                return type = Type.StartAndEndAfterMid;

            else if (doubleStartTime >= doubleBedTime && doubleStartTime < doubleEndTime && doubleEndTime > EARLIESTSTARTTIME
                && doubleStartTime < MIDNIGHT && doubleBedTime > LASTENDTIME)
                return type = Type.BedtimeBeforeStartOrEqualStartBothBeforeMidEndBeforeMid;

            else if (doubleStartTime >= doubleBedTime && doubleStartTime <= MIDNIGHT && doubleEndTime <= LASTENDTIME && doubleBedTime > LASTENDTIME)
                return type = Type.BedtimeBeforeOrEqualStartBothBeforeMidEndAfterMid;

            else if (doubleStartTime < doubleEndTime && doubleEndTime <= MIDNIGHT && doubleStartTime < MIDNIGHT
                && doubleStartTime > LASTENDTIME && (doubleBedTime >= doubleEndTime || doubleBedTime <= LASTENDTIME))
                return type = Type.StartBeforeMidBedtimeEqualsOrAfterEndAndEndBeforeMid;

            else if (doubleStartTime <= MIDNIGHT && doubleEndTime <= LASTENDTIME && doubleBedTime <= LASTENDTIME)
                return type = Type.StartBeforeOrAtMidBedAndEndAfterMid;

            else if (doubleStartTime < doubleBedTime && doubleBedTime < doubleEndTime && doubleEndTime <= MIDNIGHT)
                return type = Type.StartBeforeBedBothBeforeMidEndBeforeMid;

            else if (doubleStartTime < doubleBedTime && doubleBedTime <= MIDNIGHT && doubleEndTime <= LASTENDTIME && doubleStartTime >= EARLIESTSTARTTIME)
                return type = Type.StartBeforeBedBothBeforeMidEndAfterMid;

            else
                return type = Type.defaultType;
        }
    }
}


