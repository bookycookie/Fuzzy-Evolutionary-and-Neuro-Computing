using System;
using System.Collections.Generic;
using System.Linq;
using Homework.Defuzzifier;
using Homework.Domain;
using Homework.LinguisticVariable;
using Homework.Rules;
using Homework.Sets;
using Homework.Sets.Operations;

namespace Homework.FuzzyEngine
{
    public class HelmFuzzyEngine : IFuzzyEngine
    {
        private readonly IDefuzzifier _defuzzifier;
        
        public List<IRule> RuleBase { get; set; } = new List<IRule>
        {
            Rule.WHEN_L_SHORT_AND_LK_SHORT_THEN_HELM_SHARPRIGHT(),
            Rule.WHEN_D_SHORT_AND_DK_SHORT_THEN_HELM_SHARPLEFT()
        };
        
        public HelmFuzzyEngine(IDefuzzifier defuzzifier) => _defuzzifier = defuzzifier;

        public int Conclude(int l, int d, int lk, int dk, int v, int s)
        {

            Dictionary<string, int> variables = new Dictionary<string, int>
            {
                {VariableConstants.L, l},
                {VariableConstants.D, d},
                {VariableConstants.LK, lk},
                {VariableConstants.DK, dk},
                {VariableConstants.V, v},
                {VariableConstants.S, s}
            };

            List<IFuzzySet> results = new List<IFuzzySet>();
            foreach (var rule in RuleBase)
            {
                var values = new double[rule.Antecedent.Count];

                for (var i = 0; i < rule.Antecedent.Count; i++)
                {
                    var adjective = rule.Antecedent[i];
                    var variable = rule.Variables[i];
                    values[i] = adjective.GetValueAt(DomainElement.Of(variables[variable]));
                }

                var min = values.Min();

                var result = new MutableFuzzySet(rule.Consequent.GetDomain());

                foreach (var element in result.GetDomain())
                {
                    var mi = rule.Consequent.GetValueAt(element);
                    result.Set(element, Math.Min(mi, min));
                }
                results.Add(result);
            }

            IFuzzySet finalResult = Union(results);

            return _defuzzifier.Defuzzify(finalResult);
        }

        private static IFuzzySet Union(IReadOnlyList<IFuzzySet> list)
        {
            if (list.Count == 1)
                return list.First();

            IFuzzySet result = Operations.BinaryOperation(list[0], list[1], Operations.ZadehOr());
            if (list.Count == 2)
                return result;

            for (var i = 2; i < list.Count; i++)
                result = Operations.BinaryOperation(result, list[i], Operations.ZadehOr());

            return result;
        }
        
        public IFuzzySet ConcludeWithoutDefuzzifying(int l, int d, int lk, int dk, int v, int s)
        {
            Dictionary<string, int> variables = new Dictionary<string, int>
            {
                {VariableConstants.L, l},
                {VariableConstants.D, d},
                {VariableConstants.LK, lk},
                {VariableConstants.DK, dk},
                {VariableConstants.V, v},
                {VariableConstants.S, s}
            };

            List<IFuzzySet> results = new List<IFuzzySet>();
            foreach (var rule in RuleBase)
            {
                var values = new double[rule.Antecedent.Count];

                for (var i = 0; i < rule.Antecedent.Count; i++)
                {
                    var adjective = rule.Antecedent[i];
                    var variable = rule.Variables[i];
                    values[i] = adjective.GetValueAt(DomainElement.Of(variables[variable]));
                }

                var min = values.Min();

                var result = new MutableFuzzySet(rule.Consequent.GetDomain());

                foreach (var element in result.GetDomain())
                {
                    var mi = rule.Consequent.GetValueAt(element);
                    result.Set(element, Math.Min(mi, min));
                }
                results.Add(result);
            }

            return Union(results);
        }
        
        public IFuzzySet ConcludeChosenRuleWithoutDefuzzifying(Rule rule1, int l, int d, int lk, int dk, int v, int s)
        {
            var newRuleBase = new List<IRule> { rule1 };
            Dictionary<string, int> variables = new Dictionary<string, int>
            {
                {VariableConstants.L, l},
                {VariableConstants.D, d},
                {VariableConstants.LK, lk},
                {VariableConstants.DK, dk},
                {VariableConstants.V, v},
                {VariableConstants.S, s}
            };

            List<IFuzzySet> results = new List<IFuzzySet>();
            foreach (var rule in newRuleBase)
            {
                var values = new double[rule.Antecedent.Count];

                for (var i = 0; i < rule.Antecedent.Count; i++)
                {
                    var adjective = rule.Antecedent[i];
                    var variable = rule.Variables[i];
                    values[i] = adjective.GetValueAt(DomainElement.Of(variables[variable]));
                }

                var min = values.Min();

                var result = new MutableFuzzySet(rule.Consequent.GetDomain());

                foreach (var element in result.GetDomain())
                {
                    var mi = rule.Consequent.GetValueAt(element);
                    result.Set(element, Math.Min(mi, min));
                }
                results.Add(result);
            }

            return Union(results);
        }
        
        public int ConcludeWithProduct(int l, int d, int lk, int dk, int v, int s)
        {
            Dictionary<string, int> variables = new Dictionary<string, int>
            {
                {VariableConstants.L, l},
                {VariableConstants.D, d},
                {VariableConstants.LK, lk},
                {VariableConstants.DK, dk},
                {VariableConstants.V, v},
                {VariableConstants.S, s}
            };

            List<IFuzzySet> results = new List<IFuzzySet>();
            foreach (var rule in RuleBase)
            {
                var values = new double[rule.Antecedent.Count];

                for (var i = 0; i < rule.Antecedent.Count; i++)
                {
                    var adjective = rule.Antecedent[i];
                    var variable = rule.Variables[i];
                    values[i] = adjective.GetValueAt(DomainElement.Of(variables[variable]));
                }

                var min = values.Min();

                var result = new MutableFuzzySet(rule.Consequent.GetDomain());

                foreach (var element in result.GetDomain())
                {
                    var mi = rule.Consequent.GetValueAt(element);
                    result.Set(element, mi * min);
                }
                results.Add(result);
            }

            var final =  Union(results);

            return _defuzzifier.Defuzzify(final);
        }

        private void InitialiseRulebase()
        {
            RuleBase.Add(Rule.WHEN_L_SHORT_AND_LK_SHORT_THEN_HELM_SHARPRIGHT());
            RuleBase.Add(Rule.WHEN_D_SHORT_AND_DK_SHORT_THEN_HELM_SHARPLEFT());
            // RuleBase.Add(Rule.WHEN_L_SHORT_AND_LK_SHORT_HELM_RIGHT());
            // RuleBase.Add(Rule.WHEN_L_LONG_AND_D_SHORT_HELM_LEFT());
            // RuleBase.Add(Rule.WHEN_L_SHORT_AND_D_LONG_HELM_RIGHT());
            // RuleBase.Add(Rule.WHEN_LK_LONG_AND_DK_SHORT_HELM_LEFT());
            // RuleBase.Add(Rule.WHEN_LK_SHORT_AND_DK_LONG_HELM_RIGHT());
        }

    }
}