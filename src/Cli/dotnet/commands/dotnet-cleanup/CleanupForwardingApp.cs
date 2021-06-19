// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.DotNet.Cli.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Microsoft.DotNet.Cli
{
    public class CleanupForwardingApp : ForwardingApp
    {
        private const string FormatDllName = @"Format/dotnet-format.dll";

        public CleanupForwardingApp(IEnumerable<string> argsToForward)
            : base(Path.Combine(AppContext.BaseDirectory, FormatDllName), argsToForward)
        {
        }
    }
}
