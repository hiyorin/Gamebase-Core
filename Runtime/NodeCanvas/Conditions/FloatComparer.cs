#if GAMEBASE_ADD_NODECANVAS
using System;
using JetBrains.Annotations;
using ParadoxNotion.Design;
using UnityEngine;

namespace Gamebase.Core.NodeCanvas.Conditions
{
    [PublicAPI]
    [Name("Float Comparer")]
    [Category("✫ Gamebase/Extensions")]
    public sealed class FloatComparer : VariableComparer<float>
    {
        protected override bool OnCheck()
        {
            switch (_operator.value)
            {
                case Operator.Equal:
                    return Mathf.Approximately(left.value, right.value);
                case Operator.NotEqual:
                    return !Mathf.Approximately(left.value, right.value);
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