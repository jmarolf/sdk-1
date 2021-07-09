// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Collections.Generic;
using System.CommandLine.Parsing;

namespace Microsoft.DotNet.Cli
{
    internal class CleanupFormattingCommand : AbstractCleanupCommand
    {
        protected override string ParseFrom => "dotnet cleanup formatting";

        protected override List<string> AddArgs(ParseResult parseResult)
        {
            var dotnetFormatArgs = new List<string>();
            dotnetFormatArgs.AddCommonDotnetFormatArgs(parseResult);
            dotnetFormatArgs.AddFormattingDotnetFormatArgs(parseResult);
            dotnetFormatArgs.AddProjectOrSolutionDotnetFormatArgs(parseResult);
            return dotnetFormatArgs;
        }
    }
}
