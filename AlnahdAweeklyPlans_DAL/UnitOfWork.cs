using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DataBaseCrud
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private Nahda_AttendanceEntities _context;

        public UnitOfWork()
        {
            _context = new Nahda_AttendanceEntities();

        }
        #region |Properties|

        private GenericRepository<LessonsPlanMaster> _LessonsPlanMasterRepository;
        private GenericRepository<LessonPlanDetail> _LessonPlanDetailRepository;
        private GenericRepository<LessonPlanCategory> _LessonPlanCategoryRepository;
        private GenericRepository<Student_Class> _Student_ClassRepository;
        private GenericRepository<Exam_subjectGroup> _Exam_subjectGroupRepository;
        private GenericRepository<LessonPlanClass> _LessonPlanClassRepository;
        private GenericRepository<LessonPlanSubject> _LessonPlanSubjectRepository;
        private GenericRepository<DigitalResourceMaster> _DigitalResourceMasterRepository;
        private GenericRepository<DigitalResourceFile> _DigitalResourceFileRepository;
        private GenericRepository<DigitalResourceClass> _DigitalResourceClassRepository;
        private GenericRepository<CleaningStaffByLocation> _CleaningStaffByLocationRepository;
        private GenericRepository<Maintenance_IssuesDetail> _IssuesDetailRepository;
        private GenericRepository<LeaveRequestForm> _LeaveRequestFormRepository;
        private GenericRepository<Maintenance_Approval> _MaintenanceApprovalRepository;
        private GenericRepository<MaintenanceRequestFormMaster> _MaintenanceRequestFormMasterRepository;
        private GenericRepository<ComplainLog> _ComplainLogRepository;
        private GenericRepository<NewStaffJoining> _NewStaffJoiningRepository;
        private GenericRepository<StudentFeedBackReport> _StudentFeedBackReportRepository;
        private GenericRepository<Maintenance_TaskProcess> _TaskProcessRepository;
        private GenericRepository<Maintenance_TechnicianRequested> _TechnicianRequestedRepository;
        private GenericRepository<HREmployeeMst> _HREmployeeMstRepository;
        private GenericRepository<WeeklyMeetingSchedule> _WeeklyMeetingScheduleRepository;
        

        #endregion

        #region |Implementation|

        public GenericRepository<LessonsPlanMaster> LessonsPlanMasterRepository
        {
            get
            {
                if (this._LessonsPlanMasterRepository == null)
                {
                    this._LessonsPlanMasterRepository = new GenericRepository<LessonsPlanMaster>(_context);
                }
                return _LessonsPlanMasterRepository;
            }
        }

        public GenericRepository<LessonPlanDetail> LessonPlanDetailRepository
        {
            get
            {
                if (this._LessonPlanDetailRepository == null)
                {
                    this._LessonPlanDetailRepository = new GenericRepository<LessonPlanDetail>(_context);
                }
                return _LessonPlanDetailRepository;
            }
        }
        public GenericRepository<LessonPlanCategory> LessonPlanCategoryRepository
        {
            get
            {
                if (this._LessonPlanCategoryRepository == null)
                {
                    this._LessonPlanCategoryRepository = new GenericRepository<LessonPlanCategory>(_context);
                }
                return _LessonPlanCategoryRepository;
            }
        }
        public GenericRepository<Student_Class> Student_ClassRepository
        {
            get
            {
                if (this._Student_ClassRepository == null)
                {
                    this._Student_ClassRepository = new GenericRepository<Student_Class>(_context);
                }
                return _Student_ClassRepository;
            }
        }
        public GenericRepository<Exam_subjectGroup> Exam_subjectGroupRepository
        {
            get
            {
                if (this._Exam_subjectGroupRepository == null)
                {
                    this._Exam_subjectGroupRepository = new GenericRepository<Exam_subjectGroup>(_context);
                }
                return _Exam_subjectGroupRepository;
            }
        }
        public GenericRepository<LessonPlanSubject> LessonPlanSubjectRepository
        {
            get
            {
                if (this._LessonPlanSubjectRepository == null)
                {
                    this._LessonPlanSubjectRepository = new GenericRepository<LessonPlanSubject>(_context);
                }
                return _LessonPlanSubjectRepository;
            }
        }
        public GenericRepository<LessonPlanClass> LessonPlanClassRepository
        {
            get
            {
                if (this._LessonPlanClassRepository == null)
                {
                    this._LessonPlanClassRepository = new GenericRepository<LessonPlanClass>(_context);
                }
                return _LessonPlanClassRepository;
            }
        }
        public GenericRepository<DigitalResourceMaster> DigitalResourceMasterRepository
        {
            get
            {
                if (this._DigitalResourceMasterRepository == null)
                {
                    this._DigitalResourceMasterRepository = new GenericRepository<DigitalResourceMaster>(_context);
                }
                return _DigitalResourceMasterRepository;
            }
        }
        public GenericRepository<DigitalResourceFile> DigitalResourceFileRepository
        {
            get
            {
                if (this._DigitalResourceFileRepository == null)
                {
                    this._DigitalResourceFileRepository = new GenericRepository<DigitalResourceFile>(_context);
                }
                return _DigitalResourceFileRepository;
            }
        }
        public GenericRepository<DigitalResourceClass> DigitalResourceClassRepository
        {
            get
            {
                if (this._DigitalResourceClassRepository == null)
                {
                    this._DigitalResourceClassRepository = new GenericRepository<DigitalResourceClass>(_context);
                }
                return _DigitalResourceClassRepository;
            }
        }
        public GenericRepository<CleaningStaffByLocation> CleaningStaffByLocationRepository
        {
            get
            {
                if (this._CleaningStaffByLocationRepository == null)
                {
                    this._CleaningStaffByLocationRepository = new GenericRepository<CleaningStaffByLocation>(_context);
                }
                return _CleaningStaffByLocationRepository;
            }
        }
        public GenericRepository<Maintenance_IssuesDetail> IssuesDetailRepository
        {
            get
            {
                if (this._IssuesDetailRepository == null)
                {
                    this._IssuesDetailRepository = new GenericRepository<Maintenance_IssuesDetail>(_context);
                }
                return _IssuesDetailRepository;
            }
        }
        public GenericRepository<LeaveRequestForm> LeaveRequestFormRepository
        {
            get
            {
                if (this._LeaveRequestFormRepository == null)
                {
                    this._LeaveRequestFormRepository = new GenericRepository<LeaveRequestForm>(_context);
                }
                return _LeaveRequestFormRepository;
            }
        }
        public GenericRepository<Maintenance_Approval> MaintenanceApprovalRepository
        {
            get
            {
                if (this._MaintenanceApprovalRepository == null)
                {
                    this._MaintenanceApprovalRepository = new GenericRepository<Maintenance_Approval>(_context);
                }
                return _MaintenanceApprovalRepository;
            }
        }
        public GenericRepository<MaintenanceRequestFormMaster> MaintenanceRequestFormMasterRepository
        {
            get
            {
                if (this._MaintenanceRequestFormMasterRepository == null)
                {
            this._MaintenanceRequestFormMasterRepository = new GenericRepository<MaintenanceRequestFormMaster>(_context);
        }
                return _MaintenanceRequestFormMasterRepository;
        }
    }
        public GenericRepository<ComplainLog> ComplainLogRepository
        {
            get
            {
                if (this._ComplainLogRepository == null)
                {
                    this._ComplainLogRepository = new GenericRepository<ComplainLog>(_context);
                }
                return _ComplainLogRepository;
            }
        }
        public GenericRepository<NewStaffJoining> NewStaffJoiningRepository
        {
            get
            {
                if (this._NewStaffJoiningRepository == null)
                {
                    this._NewStaffJoiningRepository = new GenericRepository<NewStaffJoining>(_context);
                }
                return _NewStaffJoiningRepository;
            }
        }
        public GenericRepository<StudentFeedBackReport> StudentFeedBackReportRepository
        {
            get
            {
                if (this._StudentFeedBackReportRepository == null)
                {
                    this._StudentFeedBackReportRepository = new GenericRepository<StudentFeedBackReport>(_context);
                }
                return _StudentFeedBackReportRepository;
            }
        }
        public GenericRepository<Maintenance_TaskProcess> TaskProcessRepository
        {
            get
            {
                if (this._TaskProcessRepository == null)
                {
                    this._TaskProcessRepository = new GenericRepository<Maintenance_TaskProcess>(_context);
                }
                return _TaskProcessRepository;
            }
        }
        public GenericRepository<Maintenance_TechnicianRequested> TechnicianRequestedRepository
        {
            get
            {
                if (this._TechnicianRequestedRepository == null)
                {
                    this._TechnicianRequestedRepository = new GenericRepository<Maintenance_TechnicianRequested>(_context);
                }
                return _TechnicianRequestedRepository;
            }
        }
        public GenericRepository<HREmployeeMst> HREmployeeMstRepository
        {
            get
            {
                if (this._HREmployeeMstRepository == null)
                {
                    this._HREmployeeMstRepository = new GenericRepository<HREmployeeMst>(_context);
                }
                return _HREmployeeMstRepository;
            }
        }
        public GenericRepository<WeeklyMeetingSchedule> WeeklyMeetingScheduleRepository
        {
            get
            {
                if (this._WeeklyMeetingScheduleRepository == null)
                {
                    this._WeeklyMeetingScheduleRepository = new GenericRepository<WeeklyMeetingSchedule>(_context);
                }
                return _WeeklyMeetingScheduleRepository;
            }
        }
        #endregion

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
   
    }
}