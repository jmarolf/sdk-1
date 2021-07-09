// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace Microsoft.DotNet.Cli
{
    internal static class CleanupAnalyzersCommandParser
    {
        private static readonly CleanupAnalyzersHandler s_analyzerHandler = new();

        public static Command GetCommand()
        {
            var command = new Command("analyzers")
            {
                CleanupCommandCommon.SlnOrProjectArgument,
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
            command.Handler = s_analyzerHandler;
            return command;
        }

        class CleanupAnalyzersHandler : ICommandHandler
        {
            public Task<int> InvokeAsync(InvocationContext context)
                => Task.FromResult(new CleanupAnalyzersCommand().FromArgs(context.ParseResult).Execute());
        }
    }
}
