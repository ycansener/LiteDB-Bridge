using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paspartooo.Master.Libs.Data
{
    public class AbstractEntityModel
    {
        public Guid Id { get; set; }

        public AbstractEntityModel()
        {
            Id = Guid.NewGuid();
        }

    }
}
