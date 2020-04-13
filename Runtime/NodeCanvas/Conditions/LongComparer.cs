#if GAMEBASE_ADD_NODECANVAS
using System;
using JetBrains.Annotations;
using ParadoxNotion.Design;

namespace Gamebase.Core.NodeCanvas.Conditions
{
    [PublicAPI]
    [Name("Long Comparer")]
    [Category("✫ Gamebase/Extensions")]
    public sealed class LongComparer : VariableComparer<long>
    {
        protected override bool OnCheck()
        {
            switch (_operator.value)
            {
                case Operator.Equal:
                    return left.value == right.value;
                case Operator.NotEqual:
                    return left.value != right.value;
                case Operator.Greater:
                    return left.value < right.value;
                case Operator.GreaterOrEqual:
                    return left.value <= right.value;
                case Operator.Less:
                    return left.value > right.value;
                case Operator.LessOrEqual:
                    return left.value >= right.value;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
#endif