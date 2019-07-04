﻿namespace BMWStore.Common.Constants
{
    public class EntitiesConstants
    {
        public const double CarMaxAcceleration_0_100Km = 15;
        public const double CarMinAcceleration_0_100Km = 0.1;
        public const int CarMaxCO2Emissions = 95;
        public const int CarMinCO2Emissions = 0;
        public const int CarMaxDisplacement = 5000;
        public const int CarMinDisplacement = 500;
        public const int CarMaxDoorsCount = 5;
        public const int CarMinDoorsCount = 3;
        public const double CarMinFuelConsumation = 0;
        public const double CarMaxFuelConsumation_City_Litres_100Km = 100;
        public const double CarMaxFuelConsumation_Highway_Litres_100Km = 80;
        public const int CarMaxHoursePower = 1500;
        public const int CarMinHoursePower = 80;
        public const int CarNameMaxLength = 50;
        public const int CarNameMinLength = 5;
        public const string CarMaxPrice = "15000000";
        public const double CarMaxTorque = 30;
        public const double CarMinTorque = 5;
        public const int CarVinLength = 17;
        public const int CarMaxWarrantyMonthsLeft = 100;
        public const int CarMinWarrantyMonthsLeft = 0;
        public const int CarMaxWeight = 10000;
        public const int CarMinWeight = 1000;


        public const string ColorMaxPrice = "2000";
        public const int ColorNameMaxLength = 25;
        public const int ColorNameMinLength = 3;

        public const string MinPrice = "0";

        public const int ModelTypeNameMinLength = 3;
        public const int ModelTypeNameMaxLength = 15;

        public const string EngineMaxPrice = "500000";
        public const string EngineMaxWeight_Kg = "400";
        public const string EngineMinWeight_Kg = "40";

        public const int FuelTypeNameMaxLength = 3;
        public const int FuelTypeNameMinLength = 15;

        public const int OptionNameMaxLength = 25;
        public const string OptionNameMaxPrice = "100000";
        public const int OptionNameMinLength = 3;

        public const int SeriesNameMaxLength = 15;
        public const int SeriesNameMinLength = 2;

        public const string TransmissionMaxPrice = "300000";
        public const int TransmissionNameMaxLength = 40;
        public const int TransmissionNameMinLength = 5;

        public const int UsedCarMaxMileage = int.MaxValue;
        public const int UsedCarMinMileage = 100;
    }
}