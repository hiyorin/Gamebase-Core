#if GAMEBASE_ADD_NODECANVAS
using System;
using NodeCanvas.Framework;

namespace Gamebase.Core.NodeCanvas.Conditions
{
    public abstract class VariableComparer<T> : ConditionTask
    {
        public BBParameter<T> left;
            
        public BBParameter<Operator> _operator;
        
        public BBParameter<T> right;
        
        protected override string info
        {
            get
            {
                switch (_operator.value)
                {
                    case Operator.Equal:
                        return $"{left} == {right}";
                    case Operator.NotEqual:
                        return $"{left} != {right}";
                    case Operator.Greater:
                        return $"{left} < {right}";
                    case Operator.GreaterOrEqual:
                        return $"{left} <= {right}";
                    case Operator.Less:
                        return $"{left} > {right}";
                    case Operator.LessOrEqual:
                        return $"{left} >= {right}";
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        
        public enum Operator
        {
            Equal,
            NotEqual,
            Greater,
            GreaterOrEqual,
            Less,
            LessOrEqual,
        }
    }
}
#endif