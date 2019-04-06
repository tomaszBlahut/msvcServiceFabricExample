using System.Net;
using System.Threading.Tasks;
using Api.BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Api.Controllers
{
    [Route("api/student")]
    public class StudentController : BasicController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudents();
            return Ok(students);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetStudent(long id)
        {
            var student = await _studentService.GetStudent(id);
            if (student == null)
            {
                return NotFound($"Student with specified id: {id} does not exist in DB.");
            }

            return Ok(student);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateStudent([FromBody] Student student)
        {
            var result = await _studentService.CreateStudent(student);

            if (result == null)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }

            return CreatedAtAction(nameof(GetStudent), new {id = student.Id}, student);
        }
    }
}
