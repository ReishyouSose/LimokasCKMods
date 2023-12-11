﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Mono.CSharp;
using UnityEngine;
using UnityExplorer.Config;

// Thanks to ManlyMarco for this

namespace UnityExplorer.CSConsole
{
    public class ScriptEvaluator : Evaluator, IDisposable
    {
        internal TextWriter _textWriter;
        internal static StreamReportPrinter _reportPrinter;

        private static readonly HashSet<string> StdLib = new(StringComparer.InvariantCultureIgnoreCase)
        {
            "mscorlib",
            "System.Core",
            "System",
            "System.Xml"
        };

        public ScriptEvaluator(TextWriter tw) : base(BuildContext(tw))
        {
            _textWriter = tw;

            ImportAppdomainAssemblies();
            AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoad;
        }

        public void Dispose()
        {
            AppDomain.CurrentDomain.AssemblyLoad -= OnAssemblyLoad;
            _textWriter.Dispose();
        }

        private void OnAssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            string name = args.LoadedAssembly.GetName().Name;

            if (StdLib.Contains(name))
                return;

            Reference(args.LoadedAssembly);
        }

        private void Reference(Assembly asm)
        {
            string name = asm.GetName().Name;

            if (name == "completions") // ignore assemblies generated by mcs' autocomplete.
                return;

            foreach (string blacklisted in ConfigManager.CSConsole_Assembly_Blacklist.Value.Split(';'))
            {
                string bl = blacklisted;
                if (bl.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                    bl = blacklisted.Substring(0, bl.Length - 4);
                if (string.Equals(bl, name, StringComparison.OrdinalIgnoreCase))
                    return;
            }

            ReferenceAssembly(asm);
        }

        private static CompilerContext BuildContext(TextWriter tw)
        {
            _reportPrinter = new StreamReportPrinter(tw);

            CompilerSettings settings = new()
            {
                Version = LanguageVersion.Experimental,
                GenerateDebugInfo = false,
                StdLib = true,
                Target = Target.Library,
                WarningLevel = 0,
                EnhancedWarnings = false
            };

            return new CompilerContext(settings, _reportPrinter);
        }

        private void ImportAppdomainAssemblies()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                string name = assembly.GetName().Name;
                if (StdLib.Contains(name))
                    continue;

                try
                {
                    Reference(assembly);
                }
                catch // (Exception ex)
                {
                    //ExplorerCore.LogWarning($"Excepting referencing '{name}': {ex.GetType()}.{ex.Message}");
                }
            }
        }
    }
}
