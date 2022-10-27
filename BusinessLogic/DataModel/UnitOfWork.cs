using BusinessLogic.DataModel.Repository;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataModel
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        protected readonly Agencia_8Context _context;
        protected IDbContextTransaction _transaction;


        #region Repository

        public UserRepository UserRepository { get; set; }
        public LogRepository LogRepository { get; set; }
        public DependentRepository DependentRepository { get; set; }
        public CandidateRepository CandidateRepository { get; set; }
        public ShopDataRepository ShopDataRepository { get; set; }
        public ContactPersonRepository ContactPersonRepository { get; set; }
        public DecisionParamRepository DecisionParamRepository { get; set; }
        public DecisionSupportRepository DecisionSupportRepository { get; set; }
        public ProjectionParamRepository ProjectionParamRepository { get; set; }

        #endregion

        public UnitOfWork(IConfiguration configuration, string application)
        {
            this._context = new Agencia_8Context(configuration, application);

            this.UserRepository = new UserRepository(this._context);
            this.LogRepository = new LogRepository(this._context);
            this.DependentRepository = new DependentRepository(this._context);
            this.CandidateRepository = new CandidateRepository(this._context);
            this.ShopDataRepository = new ShopDataRepository(this._context);
            this.ContactPersonRepository = new ContactPersonRepository(this._context);
            this.DecisionParamRepository = new DecisionParamRepository(this._context);
            this.DecisionSupportRepository = new DecisionSupportRepository(this._context);
            this.ProjectionParamRepository = new ProjectionParamRepository(this._context);
        }

        public void BeginTransaction()
        {
            this._transaction = this._context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
        }

        public void Commit()
        {
            if (this._transaction != null)
                this._transaction.Commit();
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        public void Rollback()
        {
            if (this._transaction != null)
                this._transaction.Rollback();
        }

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        public void SaveChangesAsync()
        {
            this._context.SaveChangesAsync();
        }
    }
}
