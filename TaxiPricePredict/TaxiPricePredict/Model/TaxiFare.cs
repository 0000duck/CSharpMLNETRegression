using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiPricePredict.Model
{
    class TaxiFare
    {
        /*vendor_id,rate_code,passenger_count,trip_time_in_secs,trip_distance,payment_type,fare_amount*/
        [LoadColumn(0)]
        public string VendorId { get; set; }
        [LoadColumn(1)]
        public string RateCode { get; set; }
        [LoadColumn(2)]
        public float PassengerCount { get; set; }
        [LoadColumn(3)]
        public float TripTime { get; set; }
        [LoadColumn(4)]
        public float TripDistance { get; set; }
        [LoadColumn(5)]
        public string PaymentType { get; set; }
        [LoadColumn(6)]
        public float FareAmount { get; set; }
    }
}
