// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Parsing;

using Microsoft.DotNet.Tools;

namespace Microsoft.DotNet.Cli
{
    internal static class CleanupCommandCommon
    {
        private static string[] VerbosityLevels => new[] { "q", "quiet", "m", "minimal", "n", "normal", "d", "detailed", "diag", "diagnostic" };
        private static string[] SeverityLevels => new[] { "info", "warn", "error" };

        public static readonly Argument SlnOrProjectArgument = new Argument<string>(CommonLocalizableStrings.SolutionOrProjectArgumentName)
        {
            Description = CommonLocalizableStrings.SolutionOrProjectArgumentDescription,
            Arity = ArgumentArity.ZeroOrOne
        }.DefaultToCurrentDirectory();

        internal static readonly Option<bool> NoRestoreOption = new(new[] { "--no-restore" }, "");
        internal static readonly Option<string[]> DiagnosticsOption = new(new[] { "--diagnostics" }, () => Array.Empty<string>(), "");
        internal static readonly Option<string> SeverityOption = new Option<string>("--severity", "").FromAmong(SeverityLevels);
        internal static readonly Option<string[]> IncludeOption = new(new[] { "--include" }, () => Array.Empty<string>(), "");
        internal static readonly Option<string[]> ExcludeOption = new(new[] { "--exclude" }, () => Array.Empty<string>(), "");
        internal static readonly Option<bool> IncludeGeneratedOption = new(new[] { "--include-generated" }, "");
        internal static readonly Option<string> VerbosityOption = new Option<string>(new[] { "--verbosity", "-v" }, "").FromAmong(VerbosityLevels);
        internal static readonly Option BinarylogOption = new Option(new[] { "--binarylog" }, "", argumentType: typeof(string), arity: ArgumentArity.ZeroOrOne)
        {
            ArgumentHelpName = "binary-log-path"
        }.LegalFilePathsOnly();
        internal static readonly Option ReportOption = new Option(new[] { "--report" }, "", argumentType: typeof(string), arity: ArgumentArity.ZeroOrOne)
        {
            ArgumentHelpName = "report-path"
        }.LegalFilePathsOnly();


        public static void AddCommonDotnetFormatArgs(this List<string> dotnetFormatArgs, ParseResult parseResult)
        {
            if (parseResult.HasOption(CleanupCommandCommon.NoRestoreOption))
            {
                dotnetFormatArgs.Add("--no-restore");
            }

            if (parseResult.HasOption(CleanupCommandCommon.IncludeGeneratedOption))
            {
                dotnetFormatArgs.Add("--include-generated");
            }

            if (parseResult.ValueForOption(CleanupCommandCommon.VerbosityOption) is string verbosity)
            {
                dotnetFormatArgs.Add("--verbosity");
                dotnetFormatArgs.Add(verbosity);
            }

            if (parseResult.ValueForOption(CleanupCommandCommon.IncludeOption) is string[] fileToInclude)
            {
                dotnetFormatArgs.Add("--include");
                dotnetFormatArgs.Add(string.Join(" ", fileToInclude));
            }

            if (parseResult.ValueForOption(CleanupCommandCommon.ExcludeOption) is string[] fileToexclude)
            {
                dotnetFormatArgs.Add("--exclude");
                dotnetFormatArgs.Add(string.Join(" ", fileToexclude));
            }


            if (parseResult.HasOption(CleanupCommandCommon.ReportOption))
            {
                dotnetFormatArgs.Add("--report");
                if (parseResult.ValueForOption(CleanupCommandCommon.ReportOption) is string reportPath)
                {
                    dotnetFormatArgs.Add(reportPath);
                }
            }

            if (parseResult.HasOption(CleanupCommandCommon.BinarylogOption))
            {
                dotnetFormatArgs.Add("--binarylog");
                if (parseResult.ValueForOption(CleanupCommandCommon.BinarylogOption) is string binaryLogPath)
                {
                    dotnetFormatArgs.Add(binaryLogPath);
                }
            }
        }

        public static void AddFormattingDotnetFormatArgs(this List<string> dotnetFormatArgs, ParseResult parseResult)
        {
            dotnetFormatArgs.Add("--fix-whitespace");
        }

        public static void AddStyleDotnetFormatArgs(this List<string> dotnetFormatArgs, ParseResult parseResult)
        {
            dotnetFormatArgs.Add("--fix-style");
            if (parseResult.ValueForOption(CleanupCommandCommon.SeverityOption) is string styleSeverity)
            {
                dotnetFormatArgs.Add(styleSeverity);
            }
        }

        public static void AddAnalyzerDotnetFormatArgs(this List<string> dotnetFormatArgs, ParseResult parseResult)
        {
            dotnetFormatArgs.Add("--fix-analyzers");
            if (parseResult.ValueForOption(CleanupCommandCommon.SeverityOption) is string analyzerSeverity)
            {
                dotnetFormatArgs.Add(analyzerSeverity);
            }

            if (parseResult.ValueForOption(CleanupCommandCommon.DiagnosticsOption) is string[] diagnostics)
            {
                dotnetFormatArgs.Add("--diagnostics");
                dotnetFormatArgs.Add(string.Join(" ", diagnostics));
            }
        }

        public static void AddProjectOrSolutionDotnetFormatArgs(this List<string> dotnetFormatArgs, ParseResult parseResult)
        {
            if (parseResult.ValueForArgument<string>(CleanupCommandCommon.SlnOrProjectArgument) is string slnOrProject)
            {
                dotnetFormatArgs.Add(slnOrProject);
            }
        }
    }
}
