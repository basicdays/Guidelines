using System;

namespace Guidelines.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        void Begin();
        void Commit();
        void RollBack();
    }
}
