﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;

namespace Microsoft.DotNet.Cli
{
    internal static partial class CleanupCommandParser
    {
        private static readonly CleanupCommandDefaultHandler s_cleanupCommandHandler = new();

        public static Command GetCommand()
        {
            var cleanupCommand = new Command("cleanup")
            {
                CleanupCommandCommon.SlnOrProjectArgument,
                CleanupFormattingCommandParser.GetCommand(),
                CleanupStyleCommandParser.GetCommand(),
                CleanupAnalyzersCommandParser.GetCommand(),
                CleanupCommandCommon.NoRestoreOption,
                CleanupCommandCommon.DiagnosticsOption,
                CleanupCommandCommon.SeverityOption,
                CleanupCommandCommon.IncludeOption,
                CleanupCommandCommon.ExcludeOption,
                CleanupCommandCommon.IncludeGeneratedOption,
                CleanupCommandCommon.VerbosityOption,
                CleanupCommandCommon.BinarylogOption,
                CleanupCommandCommon.ReportOption
            };
            cleanupCommand.Handler = s_cleanupCommandHandler;
            return cleanupCommand;
        }

        class CleanupCommandDefaultHandler : ICommandHandler
        {
            public Task<int> InvokeAsync(InvocationContext context)
                => Task.FromResult(new CleanupCommand().FromArgs(context.ParseResult).Execute());
        }
    }
}
