using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeHire.Models;

namespace CodeHire.BusinessLogic
{
    public abstract class BasicBusinessLogicImplementor<T>: IDisposable
    {
        protected ApplicationDbContext _context;

        public BasicBusinessLogicImplementor(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public abstract IEnumerable<T> GetAll();
    }
}
