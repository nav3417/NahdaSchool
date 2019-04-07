using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DataBaseCrud
{
    public interface IUnitOfWork : IDisposable
    {
        GenericRepository<LessonsPlanMaster> LessonsPlanMasterRepository { get; }
        GenericRepository<LessonPlanDetail> LessonPlanDetailRepository { get; }
        GenericRepository<LessonPlanCategory> LessonPlanCategoryRepository { get; }

        GenericRepository<Student_Class> Student_ClassRepository { get; }
        GenericRepository<Exam_subjectGroup> Exam_subjectGroupRepository { get; }
        GenericRepository<LessonPlanClass> LessonPlanClassRepository { get; }
        GenericRepository<LessonPlanSubject> LessonPlanSubjectRepository { get; }
        GenericRepository<DigitalResourceMaster> DigitalResourceMasterRepository { get; }
        GenericRepository<DigitalResourceFile> DigitalResourceFileRepository { get; }
        GenericRepository<DigitalResourceClass> DigitalResourceClassRepository { get; }
        GenericRepository<CleaningStaffByLocation> CleaningStaffByLocationRepository { get; }
        GenericRepository<ComplainLog> ComplainLogRepository { get; }
        GenericRepository<Maintenance_IssuesDetail> IssuesDetailRepository { get; }
        GenericRepository<LeaveRequestForm> LeaveRequestFormRepository { get; }
        GenericRepository<Maintenance_Approval> MaintenanceApprovalRepository { get; }
        GenericRepository<MaintenanceRequestFormMaster> MaintenanceRequestFormMasterRepository { get; }
        GenericRepository<NewStaffJoining> NewStaffJoiningRepository { get; }
        GenericRepository<StudentFeedBackReport> StudentFeedBackReportRepository { get; }
        GenericRepository<Maintenance_TaskProcess> TaskProcessRepository { get; }
        GenericRepository<HREmployeeMst> HREmployeeMstRepository { get; }
        GenericRepository<Maintenance_TechnicianRequested> TechnicianRequestedRepository { get; }
        GenericRepository<WeeklyMeetingSchedule> WeeklyMeetingScheduleRepository { get; }
        void Save();
    }
}
