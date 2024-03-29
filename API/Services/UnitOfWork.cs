﻿using API.Dominio.Repositories;

namespace API.Services
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DbSession _session;

        public UnitOfWork(DbSession session)
        {
            _session = session;
        }

        public void BeginTransaction()
        {
            _session.Transaction = _session.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _session.Transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _session.Transaction.Rollback();
            Dispose();
        }

        public bool ExisteTransacao()
        {
            return _session.Transaction != null;
        }

        public void Dispose() => _session.Transaction?.Dispose();

    }
}
