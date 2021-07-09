// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;

namespace Microsoft.DotNet.Cli
{
    internal static class CleanupFormattingCommandParser
    {

        private static readonly CleanupFormattingHandler s_formattingHandler = new();
        public static Command GetCommand()
        {
            var command = new Command("formatting")
            {
                CleanupCommandCommon.SlnOrProjectArgument,
                CleanupCommandCommon.NoRestoreOption,
                CleanupCommandCommon.IncludeOption,
                CleanupCommandCommon.ExcludeOption,
                CleanupCommandCommon.IncludeGeneratedOption,
                CleanupCommandCommon.VerbosityOption,
                CleanupCommandCommon.BinarylogOption,
                CleanupCommandCommon.ReportOption
            };
            command.Handler = s_formattingHandler;
            return command;
        }

        class CleanupFormattingHandler : ICommandHandler
        {
            public Task<int> InvokeAsync(InvocationContext context)
                => Task.FromResult(new CleanupFormattingCommand().FromArgs(context.ParseResult).Execute());
        }
    }
}
