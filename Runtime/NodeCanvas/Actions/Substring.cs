#if GAMEBASE_ADD_NODECANVAS
using JetBrains.Annotations;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace Gamebase.Core.NodeCanvas.Actions
{
    [PublicAPI]
    [Name("Substring")]
    [Category("✫ Gamebase/String")]
    public sealed class Substring : ActionTask
    {
        public BBParameter<string> source = default;

        public BBParameter<int> startIndex = new BBParameter<int>(0);
        
        public BBParameter<int> length = new BBParameter<int>(1);
        
        [BlackboardOnly] public BBParameter<string> saveAs = default;

        protected override string info => $"{saveAs} = {source}.{base.info}({startIndex}, {length})";

        protected override void OnExecute()
        {
            saveAs.value = source.value.Substring(startIndex.value, length.value);
            EndAction(true);
        }
    }
}
#endif