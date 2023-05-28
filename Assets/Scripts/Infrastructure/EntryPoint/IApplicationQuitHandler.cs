using System;

namespace Infrastructure.EntryPoint
{
    public interface IApplicationQuitHandler
    {
        public event Action ApplicationQuit;
    }
}