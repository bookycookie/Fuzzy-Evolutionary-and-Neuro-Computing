using System;
using NENR6.Extensions;

namespace NENR6.ANFIS
{
    public class Rule
    {
        private const int Scale = 1;
        // Neuron node coefficients.
        public double A { get; private set; }
        public double B { get; private set; }
        public double C { get; private set; }
        public double D { get; private set; }

        private double P { get; set; }
        private double Q { get; set; }
        private double R { get; set; }

        // Corrections.
        private double DeltaA { get; set; }
        private double DeltaB { get; set; }
        private double DeltaC { get; set; }
        private double DeltaD { get; set; }

        private double DeltaP { get; set; }
        private double DeltaQ { get; set; }
        private double DeltaR { get; set; }

        public Rule(Random random)
        {
            A = random.NextDouble(-Scale, Scale);
            B = random.NextDouble(-Scale, Scale);
            C = random.NextDouble(-Scale, Scale);
            D = random.NextDouble(-Scale, Scale);
            
            P = random.NextDouble(-Scale, Scale);
            Q = random.NextDouble(-Scale, Scale);
            R = random.NextDouble(-Scale, Scale);
            
        }

        /// <summary>
        /// Membership for the A node. Ai ≡ μAi(x) = 1 / (1 + e^b(x - a))
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private double MemA(Sample s) => Functions.Functions.Sigmoid(A, B, s.X);

        /// <summary>
        /// Membership for the B node. Bi ≡ μBi(y) = 1 / (1 + e^b(x - a))
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private double MemB(Sample s) => Functions.Functions.Sigmoid(C, D, s.Y);

        /// <summary>
        /// Calculates the T-Norm of the given sample.
        /// Defined as the algebraic product of two memberships.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public double TNorm(Sample s) => MemA(s) * MemB(s);

        /// <summary>
        /// Computed value of the sample.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public double Z(Sample s) => P * s.X + Q * s.Y + R;

        /// <summary>
        /// Resets all the corrections back to 0.0.
        /// </summary>
        private void ResetCorrections()
        {
            DeltaA = 0.0;
            DeltaB = 0.0;
            DeltaC = 0.0;
            DeltaD = 0.0;

            DeltaP = 0.0;
            DeltaQ = 0.0;
            DeltaR = 0.0;
        }

        /// <summary>
        /// Uses gradient descent to update the corrections of coefficients for the given sample.
        /// Sum(αi(Zi - Zj)) / sum(αi)
        /// </summary>
        /// <param name="err">(real - predicted)</param>
        /// <param name="s">Given sample</param>
        /// <param name="differenceSum">Sum of the differences between the function outputs multiplied by the weight (alpha).</param>
        /// <param name="sumOfWeights">Sum of all the weights (alphas).</param>
        /// <param name="eta"></param>
        /// <param name="etaZ"></param>
        public void CalculateCorrections(double err, Sample s, double differenceSum, double sumOfWeights, double eta, double etaZ)
        {
            DeltaA += eta * err * (differenceSum / sumOfWeights * sumOfWeights) * B * MemB(s) * MemA(s) * (1 - MemA(s));
            DeltaB += eta * err * (differenceSum / sumOfWeights * sumOfWeights) * (A - s.X) * MemB(s) * MemA(s) * (1 - MemA(s));
            DeltaC += eta * err * (differenceSum / sumOfWeights * sumOfWeights) * D * MemA(s) * MemB(s) * (1 - MemB(s));
            DeltaD += eta * err * (differenceSum / sumOfWeights * sumOfWeights) * (C - s.Y) * MemA(s) * MemB(s) * (1- MemB(s));

            var alpha = TNorm(s);
            DeltaP += etaZ * err * alpha / sumOfWeights * s.X;
            DeltaQ += etaZ * err * alpha / sumOfWeights * s.Y;
            DeltaR += etaZ * err * alpha / sumOfWeights;
        }

        /// <summary>
        /// Coefficients are updated with their respective corrections.
        /// The corrections also need to be reset after each update.
        /// </summary>
        public void Update()
        {
            A += DeltaA;
            B += DeltaB;
            C += DeltaC;
            D += DeltaD;
            
            P += DeltaP;
            Q += DeltaQ;
            R += DeltaR;
            
            ResetCorrections();
        }
    }
}