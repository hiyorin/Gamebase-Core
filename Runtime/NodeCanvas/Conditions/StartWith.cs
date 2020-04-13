#if GAMEBASE_ADD_NODECANVAS
using JetBrains.Annotations;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace Gamebase.Core.NodeCanvas.Conditions
{
    [PublicAPI]
    [Name("StartWith")]
    [Category("✫ Gamebase/String")]
    public sealed class StartWith : ConditionTask
    {
        public BBParameter<string> source = default;

        public BBParameter<string> value = default;

        protected override string info => $"{source}.{base.info}({value})";
        
        protected override bool OnCheck() => source.value.StartsWith(value.value);
    }
}
#endif