using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Gamebase.Editor
{
    [Serializable]
    public sealed class AssemblyDefine
    {
        public string name;

        public List<string> references = new List<string>();

        public List<string> includePlatforms = new List<string>();

        public List<string> excludePlatforms = new List<string>();

        public bool allowUnsafeCode;

        public bool overrideReferences;

        public List<string> precompiledReferences = new List<string>();
        
        public bool autoReferenced = true;
    }

    public sealed class AssemblyDefineBuilder
    {
        private readonly AssemblyDefine data = new AssemblyDefine();

        private AssemblyDefineBuilder()
        {
            
        }
        
        public static AssemblyDefineBuilder Create() => new AssemblyDefineBuilder();

        public AssemblyDefineBuilder SetName(string value)
        {
            data.name = value;
            return this;
        }

        public AssemblyDefineBuilder AddReference(params string[] values)
        {
            data.references.AddRange(values);
            return this;
        }

        public AssemblyDefineBuilder AddIncludePlatform(params string[] values)
        {
            data.includePlatforms.AddRange(values);
            return this;
        }

        public AssemblyDefineBuilder AddExcludePlatform(params string[] values)
        {
            data.excludePlatforms.AddRange(values);
            return this;
        }

        public AssemblyDefineBuilder SetAllowUnsafeCode(bool value)
        {
            data.allowUnsafeCode = value;
            return this;
        }

        public void Write(string path)
        {
            var json = JsonUtility.ToJson(data);
            var fullPath = $"{Application.dataPath}/../{path}";
            File.WriteAllText(fullPath, json);
        }
    }
}