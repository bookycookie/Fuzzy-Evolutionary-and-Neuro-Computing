using System;
using System.Collections.Generic;
using System.Linq;
using Homework_7.Activations;

namespace Homework_7
{
    public class NeuralNetwork
    {
        private readonly int[] _layers;
        private readonly int _paramsNum;

        /// <summary>
        /// 2 x <paramref name="layers"/> x 3
        /// </summary>
        /// <param name="layers"></param>
        public NeuralNetwork(params int[] layers)
        {
            var tmp = new List<int> { 2, 3 };
            tmp.InsertRange(1, layers);
            _layers = tmp.ToArray();
            _paramsNum = GetNumberOfRequiredParameters();
        }

        private int GetNumberOfRequiredParameters()
        {
            var sum = 2 * _layers[0] * _layers[1];

            var trueLayers = _layers.Skip(1).ToArray();

            for (var i = 1; i < trueLayers.Length; i++)
                sum += (trueLayers[i - 1] + 1) * trueLayers[i];

            return sum;
        }

        public double[] CalculateOutput(double x, double y, double[] parameters)
        {
            var offset = 0;
            var outputs = new double[_layers.Length][];
            const int likenessLayerIdx = 1;
            
            // 1 - likeness neuron layer
            outputs[likenessLayerIdx] = new double[_layers[likenessLayerIdx]];

            // Activate the likeness layer
            for (var likenessNeuronIdx = 0; likenessNeuronIdx < _layers[likenessLayerIdx]; likenessNeuronIdx++)
            {
                var weightX = parameters[offset++];
                var varianceX = parameters[offset++];
                var weightY = parameters[offset++];
                var varianceY = parameters[offset++];

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
                    var output = parameters[offset++] + previousOutput.Sum(o => o * parameters[offset++]);
                    outputs[hiddenLayerIdx][hiddenNeuronIdx] = ActivationFunctions.Sigmoid(output);
                }
            }

            Console.WriteLine($"I've passed {offset} neurons, should be  {_paramsNum}");
            return outputs[_layers.Length - 1];
        }
        

        public double MeanSquareError(Dataset dataset, double[] parameters)
        {
            var mse = 0.0;
            foreach (var sample in dataset)
            {
                var prediction = CalculateOutput(sample.X, sample.Y, parameters);
                var output = new[] { sample.A, sample.B, sample.C };
                
                for (var i = 0; i < 3; i++)
                {
                    var error = output[i] - prediction[i];
                    mse += error * error;
                }
            }
            return mse / dataset.DatasetCount();
        }

        private int ParametersPerNeuron(int layerIndex) =>
            layerIndex == 1 ? _layers[0] * 2 : _layers[layerIndex - 1] + 1;
        
        public int GetLayerIndexOfNeuron(int neuronIndex)
        {
            var neuronsSeen = 0;
            for (var layerIndex = 1; layerIndex < _layers.Length; layerIndex++)
            {
                neuronsSeen += _layers[layerIndex];
                if (neuronIndex < neuronsSeen) return layerIndex;
            }

            return -1;
        }

        public override string ToString() => string.Join(" x ", _layers);
    }
}