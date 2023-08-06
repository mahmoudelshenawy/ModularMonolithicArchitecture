using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Employees.Controllers
{
    [ApiController]
    [Route("/api/user/[controller]")]
    [Authorize(Roles = RolesNameConstants.Administrator)]
    public class EmployeeAccessController : ControllerBase
    {

    }
}
