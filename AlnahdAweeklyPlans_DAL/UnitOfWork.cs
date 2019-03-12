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
        // private GenericRepository<UserLog> _UserLogRepository;


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