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
        void Save();
    }
}
