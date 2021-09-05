using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiPricePredict
{
    class Program
    {
        static void Main(string[] args)
        {
            var mlContext = new MLContext();
            var trainDataView = mlContext.Data.LoadFromTextFile<Model.TaxiFare>("data/taxi-fare-train.csv", hasHeader: true, separatorChar: ',');
            var testDataView = mlContext.Data.LoadFromTextFile<Model.TaxiFare>("data/taxi-fare-test.csv", hasHeader: true, separatorChar: ',');
            var pipeLine = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: "FareAmount")
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(inputColumnName: "VendorId", outputColumnName: "VendorIdEncoded"))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(inputColumnName: "RateCode", outputColumnName: "RateCodeEncoded"))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(inputColumnName: "PaymentType", outputColumnName: "PaymentTypeEncoded"))
                .Append(mlContext.Transforms.Concatenate("Features", new string[] {"VendorIdEncoded","RateCodeEncoded","PaymentTypeEncoded",
                        "PassengerCount","TripTime","TripDistance"}))
                .Append(mlContext.Regression.Trainers.Sdca());
           var model =  pipeLine.Fit(trainDataView);


            PredictionEngine<Model.TaxiFare, Model.TaxiFareOutput> predictionEngine = 
                mlContext.Model.CreatePredictionEngine<Model.TaxiFare, Model.TaxiFareOutput>(model, trainDataView.Schema);

            var prediction = model.Transform(testDataView);
            var metrics = mlContext.Regression.Evaluate(prediction, "Label", "Score");

            var predictionResult = predictionEngine.Predict(new Model.TaxiFare() { 
                PassengerCount= 3,
                TripDistance = 100,
                TripTime= 600,
                PaymentType = "CRD",
                RateCode="1",
                VendorId = "VTS"
            });
            Console.WriteLine("Amount:" + predictionResult.FareAmount);
            Console.ReadLine();
        }
    }
}
