﻿// <auto-generated />
/* ************************************************************
 * 
 * 
 * AUTO GENERERATED - DO NOT ALTER
 * 
 * 
 * ************************************************************/ 
namespace ContosoUniversity.TestKit.Factories
{
	using Moq;
	using ContosoUniversity.Web.Mvc.Features.Course;
	using ContosoUniversity.Web.Mvc.Features.Department;
	using ContosoUniversity.Web.Mvc.Features.Instructor;
	using ContosoUniversity.Web.Mvc.Features.Student;
	using NRepository.Core.Query;

    // Auto generated
	public partial class StudentControllerFactory
	{
		private readonly MockBehavior _MockBehaviour;

	    public StudentControllerFactory()
            : this(MockBehavior.Default)
        {
        }

        public StudentControllerFactory(MockBehavior mockBehaviour)
        {
            _MockBehaviour = mockBehaviour;
			
			QueryRepositoryMock = new Mock<IQueryRepository>(_MockBehaviour);
		
			Initalise();
		}

		partial void Initalise();

		// IQueryRepository
		private IQueryRepository _IQueryRepository;
        public Mock<IQueryRepository> QueryRepositoryMock { get; set; }
        public IQueryRepository _GetQueryRepository() { return _IQueryRepository ?? QueryRepositoryMock.Object; }
	    public IQueryRepository _SetQueryRepository(IQueryRepository obj){ _IQueryRepository = obj; return obj;}

	    public StudentController  Object
        {
            get
            {
                return new StudentController(_GetQueryRepository());
			}
        }

		public void VerifyAll()
		{
			QueryRepositoryMock.VerifyAll();
		}
	}


    // Auto generated
	public partial class InstructorControllerFactory
	{
		private readonly MockBehavior _MockBehaviour;

	    public InstructorControllerFactory()
            : this(MockBehavior.Default)
        {
        }

        public InstructorControllerFactory(MockBehavior mockBehaviour)
        {
            _MockBehaviour = mockBehaviour;
			
			QueryRepositoryMock = new Mock<IQueryRepository>(_MockBehaviour);
		
			Initalise();
		}

		partial void Initalise();

		// IQueryRepository
		private IQueryRepository _IQueryRepository;
        public Mock<IQueryRepository> QueryRepositoryMock { get; set; }
        public IQueryRepository _GetQueryRepository() { return _IQueryRepository ?? QueryRepositoryMock.Object; }
	    public IQueryRepository _SetQueryRepository(IQueryRepository obj){ _IQueryRepository = obj; return obj;}

	    public InstructorController  Object
        {
            get
            {
                return new InstructorController(_GetQueryRepository());
			}
        }

		public void VerifyAll()
		{
			QueryRepositoryMock.VerifyAll();
		}
	}


    // Auto generated
	public partial class DepartmentControllerFactory
	{
		private readonly MockBehavior _MockBehaviour;

	    public DepartmentControllerFactory()
            : this(MockBehavior.Default)
        {
        }

        public DepartmentControllerFactory(MockBehavior mockBehaviour)
        {
            _MockBehaviour = mockBehaviour;
			
			QueryRepositoryMock = new Mock<IQueryRepository>(_MockBehaviour);
		
			Initalise();
		}

		partial void Initalise();

		// IQueryRepository
		private IQueryRepository _IQueryRepository;
        public Mock<IQueryRepository> QueryRepositoryMock { get; set; }
        public IQueryRepository _GetQueryRepository() { return _IQueryRepository ?? QueryRepositoryMock.Object; }
	    public IQueryRepository _SetQueryRepository(IQueryRepository obj){ _IQueryRepository = obj; return obj;}

	    public DepartmentController  Object
        {
            get
            {
                return new DepartmentController(_GetQueryRepository());
			}
        }

		public void VerifyAll()
		{
			QueryRepositoryMock.VerifyAll();
		}
	}


    // Auto generated
	public partial class CourseControllerFactory
	{
		private readonly MockBehavior _MockBehaviour;

	    public CourseControllerFactory()
            : this(MockBehavior.Default)
        {
        }

        public CourseControllerFactory(MockBehavior mockBehaviour)
        {
            _MockBehaviour = mockBehaviour;
			
			QueryRepositoryMock = new Mock<IQueryRepository>(_MockBehaviour);
		
			Initalise();
		}

		partial void Initalise();

		// IQueryRepository
		private IQueryRepository _IQueryRepository;
        public Mock<IQueryRepository> QueryRepositoryMock { get; set; }
        public IQueryRepository _GetQueryRepository() { return _IQueryRepository ?? QueryRepositoryMock.Object; }
	    public IQueryRepository _SetQueryRepository(IQueryRepository obj){ _IQueryRepository = obj; return obj;}

	    public CourseController  Object
        {
            get
            {
                return new CourseController(_GetQueryRepository());
			}
        }

		public void VerifyAll()
		{
			QueryRepositoryMock.VerifyAll();
		}
	}

}
