using Microsoft.EntityFrameworkCore.Storage;
using MyInfrastructure.Extensions;
using MyInfrastructure.Model;
using Horizon.Data;

namespace Data.Services
{
    public class SaveManager<T> 
    {
        private readonly ApplicationDbContext _db;

        public SaveManager(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<FeedBackWithMessages>
           SaveTransactionAsync(Func<T, Task<T>> AppliedFunc, T data)
        {
            var fb = new FeedBackWithMessages();
            using (IDbContextTransaction
                transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    await AppliedFunc(data);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    fb.Done = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    fb.Done = false;
                    fb.Messages.AddRange(ex.GetExpctionErrors());
                }
            }

            return fb;
        }

   

        public FeedBackWithMessages SaveTransaction(Action<T> AppliedAction,
                                                                    T data)
        {
            var fb = new FeedBackWithMessages();
            using (IDbContextTransaction
                transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    AppliedAction(data);
                    _db.SaveChanges();
                    transaction.Commit();
                    fb.Done = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    fb.Done = false;
                    fb.Messages.AddRange(ex.GetExpctionErrors());
                }
            }

            return fb;
        }


        public FeedBackWithMessages SaveTransaction(Action<T, IFormFile> AppliedAction,
                                                    T data, IFormFile file)
        {
            var fb = new FeedBackWithMessages();
            using (IDbContextTransaction
                transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    AppliedAction(data, file);
                    _db.SaveChanges();
                    transaction.Commit();
                    fb.Done = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    fb.Done = false;
                    fb.Messages.AddRange(ex.GetExpctionErrors());
                }
            }

            return fb;
        }

    }
}
