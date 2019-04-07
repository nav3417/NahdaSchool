using Database.DataBaseCrud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AlnahdAweeklyPlans.API
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StudentFeedBackController : ApiController
    {
        IUnitOfWork unitofwork;
        public StudentFeedBackController()
        {
            unitofwork = new UnitOfWork();
        }

        public IHttpActionResult Get()
        {
            return Ok(unitofwork.StudentFeedBackReportRepository.Get().ToList());
        }
        public IHttpActionResult Get(int id)
        {
            return Ok(unitofwork.StudentFeedBackReportRepository.GetByID(id));
        }

    }
}
