using System.Collections.Generic;
using System.Linq;
using Homework_7.Activations;

namespace Homework_7.ANN
{
    public class NeuralNetwork
    {
        private readonly int[] _layers;
        private readonly Dataset _dataset;

        /// <summary>
        /// 2 x <paramref name="layers"/> x 3
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="layers"></param>
        public NeuralNetwork(Dataset dataset, params int[] layers)
        {
            var tmp = new List<int> { 2, 3 };
            tmp.InsertRange(1, layers);
            _layers = tmp.ToArray();
            _dataset = dataset;
        }

        public int GetNumberOfRequiredParameters()
        {
            var sum = 2 * _layers[0] * _layers[1];

            var trueLayers = _layers.Skip(1).ToArray();

            for (var i = 1; i < trueLayers.Length; i++)
                sum += (trueLayers[i - 1] + 1) * trueLayers[i];

            return sum;
        }

        public double[] CalculateOutput(double x, double y, double[] parameters)
        {
            var paramIdx = 0;
            var outputs = new double[_layers.Length][];
            const int likenessLayerIdx = 1;
            
            // 1 - likeness neuron layer
            outputs[likenessLayerIdx] = new double[_layers[likenessLayerIdx]];

            // Activate the likeness layer
            for (var likenessNeuronIdx = 0; likenessNeuronIdx < _layers[likenessLayerIdx]; likenessNeuronIdx++)
            {
                var weightX = parameters[paramIdx++];
                var varianceX = parameters[paramIdx++];
                var weightY = parameters[paramIdx++];
                var varianceY = parameters[paramIdx++];

                var xy = new[] {x, y};
                var w = new[] {weightX, weightY};
                var s = new[] {varianceX, varianceY};

                outputs[likenessLayerIdx][likenessNeuronIdx] = ActivationFunctions.Likeness(xy, w, s);
            }
            
            // Pass through the hidden layer
            for (var hiddenLayerIdx = 2; hiddenLayerIdx < _layers.Length; hiddenLayerIdx++)
            {
                var hidden = _layers[hiddenLayerIdx];
                outputs[hiddenLayerIdx] = new double[hidden];
                var previousOutput = outputs[hiddenLayerIdx - 1];

                // Pass through every neuron
                for (var hiddenNeuronIdx = 0; hiddenNeuronIdx < hidden; hiddenNeuronIdx++)
                {
                    // Collect previous outputs
                    // Bias + previous output
                    var output = parameters[paramIdx++] + previousOutput.Sum(o => o * parameters[paramIdx++]);
                    outputs[hiddenLayerIdx][hiddenNeuronIdx] = ActivationFunctions.Sigmoid(output);
                }
            }

            return outputs[_layers.Length - 1];
        }
        

        public double MeanSquareError(double[] parameters)
        {
            var mse = 0.0;
            foreach (var sample in _dataset)
            {
                var prediction = CalculateOutput(sample.X, sample.Y, parameters);
                var output = new[] { sample.A, sample.B, sample.C };
                
                for (var i = 0; i < 3; i++)
                {
                    var error = output[i] - prediction[i];
                    mse += error * error;
                }
            }
            return mse / _dataset.DatasetCount();
        }

        public override string ToString() => string.Join(" x ", _layers);
    }
}