using System.Collections.Generic;
using Homework.LinguisticVariable;
using Homework.LinguisticVariable.Acceleration;
using Homework.LinguisticVariable.Direction;
using Homework.LinguisticVariable.Distance;
using Homework.LinguisticVariable.HelmRotation;
using Homework.LinguisticVariable.Velocity;
using Homework.Sets;

namespace Homework.Rules
{
    public class Rule : IRule
    {
        public List<IFuzzySet> Antecedent { get; set; }
        public IFuzzySet Consequent { get; set; }

        public List<string> Variables { get; set; }

        public Rule(List<IFuzzySet> antecedent, IFuzzySet consequent, List<string> variables)
        {
            Antecedent = antecedent;
            Consequent = consequent;
            Variables = variables;
        }

        public static Rule WHEN_L_SHORT_AND_LK_SHORT_THEN_HELM_SHARPRIGHT()
        {
            var variables = new List<string> { VariableConstants.L, VariableConstants.LK };
            var antecedent = new List<IFuzzySet>
            {
                new ShortDistance(VariableConstants.L),
                new ShortDistance(VariableConstants.LK),
            };
            var consequent = new SharpRightHelmRotation(VariableConstants.Helm);
            return new Rule(antecedent, consequent, variables);
        }

        public static Rule WHEN_D_SHORT_AND_DK_SHORT_THEN_HELM_SHARPLEFT()
        {
            var variables = new List<string> { VariableConstants.D, VariableConstants.DK };
            var antecedent = new List<IFuzzySet>
                {
                new ShortDistance(VariableConstants.D),
                new ShortDistance(VariableConstants.DK),
            };
            var consequent = new SharpLeftHelmRotation(VariableConstants.Helm);
            return new Rule(antecedent, consequent, variables);
        }

        public static Rule WHEN_V_SLOW_THEN_ACCELERATION_POSITIVE()
        {
            var variables = new List<string> { VariableConstants.V };
            var antecedent = new List<IFuzzySet> { 
                new SlowVelocity(VariableConstants.V),
                // new DirectionMemberSet()
            };
            var consequent = new PositiveAcceleration(VariableConstants.Acceleration);
            return new Rule(antecedent, consequent, variables);
        }
        
        public static Rule WHEN_V_HIGH_THEN_ACCELERATION_NEGATIVE()
        {
            var variables = new List<string> { VariableConstants.V };
            var antecedent = new List<IFuzzySet> { 
                // new DistanceMemberSet(),
                // new DistanceMemberSet(),
                // new DistanceMemberSet(),
                new HighVelocity(VariableConstants.V),
                // new DirectionMemberSet()
            };
            var consequent = new NegativeAcceleration(VariableConstants.Acceleration);
            return new Rule(antecedent, consequent, variables);
        }

        public static Rule WHEN_L_SHORT_AND_LK_SHORT_HELM_RIGHT()
        {
            var variables = new List<string> { VariableConstants.L, VariableConstants.LK };
            var antecedent = new List<IFuzzySet> { new ShortDistance(VariableConstants.L), new LongDistance(VariableConstants.LK) };
            var consequent = new SharpRightHelmRotation(VariableConstants.Helm);
            return new Rule(antecedent, consequent, variables);
        }

        public static Rule WHEN_L_SHORT_AND_D_LONG_HELM_RIGHT()
        {
            var variables = new List<string> { VariableConstants.L, VariableConstants.D };
            var antecedent = new List<IFuzzySet> { new ShortDistance(VariableConstants.L), new LongDistance(VariableConstants.D) };
            var consequent = new SharpRightHelmRotation(VariableConstants.Helm);
            return new Rule(antecedent, consequent, variables);
        }
        
        public static Rule WHEN_LK_SHORT_AND_DK_LONG_HELM_RIGHT()
        {
            var variables = new List<string> { VariableConstants.LK, VariableConstants.DK };
            var antecedent = new List<IFuzzySet> { new ShortDistance(VariableConstants.LK), new LongDistance(VariableConstants.DK) };
            var consequent = new SharpRightHelmRotation(VariableConstants.Helm);
            return new Rule(antecedent, consequent, variables);
        
        }
        
        public static Rule WHEN_L_LONG_AND_D_SHORT_HELM_LEFT()
        {
            var variables = new List<string> { VariableConstants.L, VariableConstants.D };
            var antecedent = new List<IFuzzySet> { new LongDistance(VariableConstants.L), new ShortDistance(VariableConstants.D) };
            var consequent = new SharpLeftHelmRotation(VariableConstants.Helm);
            return new Rule(antecedent, consequent, variables);
        }
        
        public static Rule WHEN_LK_LONG_AND_DK_SHORT_HELM_LEFT()
        {
            var variables = new List<string> { VariableConstants.LK, VariableConstants.DK };
            var antecedent = new List<IFuzzySet> { new LongDistance(VariableConstants.LK), new ShortDistance(VariableConstants.DK) };
            var consequent = new SharpLeftHelmRotation(VariableConstants.Helm);
            return new Rule(antecedent, consequent, variables);
        }
        
        public static Rule WHEN_L_SHORT_AND_D_SHORT_ACCELERATION_POSITIVE()
        {
            var variables = new List<string> { VariableConstants.L, VariableConstants.D };
            var antecedent = new List<IFuzzySet> { new ShortDistance(VariableConstants.L), new ShortDistance(VariableConstants.D)};
            var consequent = new PositiveAcceleration(VariableConstants.Acceleration);
            return new Rule(antecedent, consequent, variables);
        }
        
        public static Rule WHEN_L_MIDDLE_AND_D_MIDDLE_ACCELERATION_POSITIVE()
        {
            var variables = new List<string> { VariableConstants.L, VariableConstants.D };
            var antecedent = new List<IFuzzySet> { new MiddleDistance(VariableConstants.L), new MiddleDistance(VariableConstants.D)};
            var consequent = new PositiveAcceleration(VariableConstants.Acceleration);
            return new Rule(antecedent, consequent, variables);
        }
        
        public static Rule WHEN_L_LONG_AND_D_LONG_ACCELERATION_POSITIVE()
        {
            var variables = new List<string> { VariableConstants.L, VariableConstants.D };
            var antecedent = new List<IFuzzySet> { new LongDistance(VariableConstants.L), new LongDistance(VariableConstants.D)};
            var consequent = new PositiveAcceleration(VariableConstants.Acceleration);
            return new Rule(antecedent, consequent, variables);
        }
        public static Rule WHEN_WRONG_DIRECTION_TURN_AROUND()
        {
            var variables = new List<string> { VariableConstants.S };
            var antecedent = new List<IFuzzySet> { new BadDirection(VariableConstants.S) };
            var consequent = new SharpLeftHelmRotation(VariableConstants.Helm);
            return new Rule(antecedent, consequent, variables);
        }

        public override string ToString()
        {
            return $"Rule: {Variables[0]} -- Antecedent: {Antecedent[0]} -- Consequent: {Consequent}";
        }

        // public override string ToString() => $"Rule({Variables[0]}, {Variables[1]}). Antecedent:\n{Antecedent[0].ToString()}\n{Antecedent[1]}\n Consequent:\n{Consequent}";
    }
}