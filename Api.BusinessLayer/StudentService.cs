using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Api.BusinessLayer
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudents();
        Task<Student> GetStudent(long id);
        Task<Student> CreateStudent(Student student);
    }

    public class StudentService : IStudentService
    {
        private readonly StudentContext _context;

        public StudentService(StudentContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            var students = await _context.Students.ToListAsync();
            return students;
        }

        public async Task<Student> GetStudent(long id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
            return student;
        }

        public async Task<Student> CreateStudent(Student student)
        {
            await _context.Students.AddAsync(student);
            try
            {
                await _context.SaveChangesAsync();

                return student;
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }
    }
}