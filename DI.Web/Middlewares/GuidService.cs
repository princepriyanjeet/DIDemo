using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI.Web.Middlewares
{
    public class GuidService : IGuidService
    {
        private readonly Guid ServiceGuid;
        public GuidService()
        {
            this.ServiceGuid = Guid.NewGuid();
        }

        public string GetGuid() => ServiceGuid.ToString();
    }
}
