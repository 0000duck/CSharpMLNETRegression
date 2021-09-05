using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiPricePredict.Model
{
    class TaxiFareOutput
    {
        [ColumnName("Score")]
        public float FareAmount { get; set; }
    }
}
