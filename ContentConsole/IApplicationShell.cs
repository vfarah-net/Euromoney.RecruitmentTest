using System;

namespace ContentConsole
{
    public interface IApplicationShell
    {
        void Run(bool ignoreFiltering);
    }
}
