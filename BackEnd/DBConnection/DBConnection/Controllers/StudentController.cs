using DBConnection.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DBConnection.Controllers
{
   
    [ApiController]
  

    public class StudentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public StudentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        [Route("GetAllStudents")]
        public List<Student> GetAllProduct()
        {
            List<Student> Lst = new List<Student>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("Select * from student", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Student obj = new Student();
                obj.Id = int.Parse(dt.Rows[i]["Id"].ToString());
                obj.Name= dt.Rows[i]["Name"].ToString();
                obj.Description1 = dt.Rows[i]["Description1"].ToString();
                Console.Write(obj.Description1);

                Lst.Add(obj);
            }
            return Lst;
        }

        [HttpPost]
        [Route("AddStudents")]
        public IActionResult AddStudnet([FromBody] Student studnet)
        {
            if (studnet == null)
            {
                return BadRequest();
            }

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var query = "INSERT INTO Student (Id, Name,Description1) " +
                            "VALUES (@Id, @Name,@Description1)";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id",studnet.Id),
                    new SqlParameter("@Name",studnet.Name),
                    new SqlParameter("@Description1",studnet.Description1)
                };

                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);
                    command.ExecuteNonQuery();
                }
            }

            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudnet(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var query = "DELETE FROM Student WHERE Id = @Id";
                var parameter = new SqlParameter("@Id", id);

                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(parameter);
                    var rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound();
                    }
                }
            }

            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest();
            }

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var query = "UPDATE Student SET Name = @Name,Description1=@Description1 WHERE Id = @Id";
                var parameters = new SqlParameter[]
                {
                new SqlParameter("@Id", id),
                new SqlParameter("@Name", student.Name),
                new SqlParameter("@Description1",student.Description1)
               
                };

                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);
                    var rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound();
                    }
                }
            }

            return Ok();
        }





    }

}
