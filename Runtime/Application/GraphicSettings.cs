using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Gamebase.Application
{
    [Serializable]
    public sealed class GraphicSettings
    {
        [SerializeField] private RenderPipelineAsset[] pipelines = {};

        public IEnumerable<RenderPipelineAsset> Pipelines => pipelines;
    }
}