using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Common
{
    public class BaseDto
    {
        public virtual bool Success { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}
