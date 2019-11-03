using Zek.Data.Repository;

namespace Zek.Web
{
    public class BaseUowController<TUnitOfWork> : BaseController
        where TUnitOfWork : IUnitOfWork
    {
        public BaseUowController(TUnitOfWork uow)
        {
            Uow = uow;
        }
        protected virtual TUnitOfWork Uow { get; set; }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Uow?.Dispose();
            }
            base.Dispose(disposing);
        }
    }




    public class BaseUowController<TUnitOfWork1, TUnitOfWork2> : BaseController
        where TUnitOfWork1 : IUnitOfWork
        where TUnitOfWork2 : IUnitOfWork
    {
        public BaseUowController(TUnitOfWork1 uow1, TUnitOfWork2 uow2)
        {
            Uow1 = uow1;
            Uow2 = uow2;
        }
        protected virtual TUnitOfWork1 Uow1 { get; set; }
        protected virtual TUnitOfWork2 Uow2 { get; set; }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Uow1?.Dispose();
                Uow2?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
