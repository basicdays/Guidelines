using System;

namespace Guidelines.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void Begin();
        void Commit();
        void RollBack();
    }
}
